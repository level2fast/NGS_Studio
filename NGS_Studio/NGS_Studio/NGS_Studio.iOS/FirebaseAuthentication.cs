using System.Threading.Tasks;
using NGS_Studio.Services;
using NGS_Studio.iOS;
using Firebase.Auth;
using Foundation;
using System;
using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(FirebaseAuthentication))]
namespace NGS_Studio.iOS
{
    public class FirebaseAuthentication : IFireBaseAuthentication
    {

        public async Task<bool> CreateUser(string username, string email, string password)
        {
            Firebase.Auth.Auth.DefaultInstance.CreateUser(email, password, CreateUserOnCompletion);

            //var authResult = await Firebase.Auth.Auth.DefaultInstance.CreateUser(email, password);
            //      .CreateUserWithEmailAndPasswordAsync(email, password);

            //var userProfileChangeRequestBuilder = new UserProfileChangeRequest();
            //userProfileChangeRequestBuilder.SetDisplayName(username);

            //var userProfileChangeRequest = userProfileChangeRequestBuilder.Build();
            //await authResult.User.UpdateProfileAsync(userProfileChangeRequest);
            return await Task.FromResult(true);
        }
        void CreateUserOnCompletion(AuthDataResult authResult, NSError error)
        {

            if (error != null)
            {
                AuthErrorCode errorCode;
                if (IntPtr.Size == 8) // 64 bits devices
                    errorCode = (AuthErrorCode)((long)error.Code);
                else // 32 bits devices
                    errorCode = (AuthErrorCode)((int)error.Code);

                // Posible error codes that CreateUser method could throw
                // Visit https://firebase.google.com/docs/auth/ios/errors for more information
                switch (errorCode)
                {
                    case AuthErrorCode.InvalidEmail:
                    case AuthErrorCode.EmailAlreadyInUse:
                    case AuthErrorCode.OperationNotAllowed:
                    case AuthErrorCode.WeakPassword:
                    default:
                        //AppDelegate.ShowMessage("Could not login!", error.LocalizedDescription, NavigationController);
                        break;
                }

                return;
            }
        }

        public bool IsSignIn()
            => Firebase.Auth.Auth.DefaultInstance.CurrentUser != null;

        public async Task ResetPassword(string email)
            => await Firebase.Auth.Auth.DefaultInstance.SendPasswordResetAsync(email);

        public async Task<string> SignIn(string email, string password)
        {
            var authResult = await Firebase.Auth.Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
            var token = await Firebase.Auth.Auth.DefaultInstance.CurrentUser.GetIdTokenResultAsync(false);
            return token.Token;
        }

        public void SignOut()
        {
            //NSString s("SignOut");
            //nint code = 0;
            //string signout = "signout";
            NSError e;
            Firebase.Auth.Auth.DefaultInstance.SignOut(out e);
        }
        public async Task<string> GetToken()
        {
            var token = await Firebase.Auth.Auth.DefaultInstance.CurrentUser.GetIdTokenResultAsync(false);
            return token.Token;
        }
    }
}