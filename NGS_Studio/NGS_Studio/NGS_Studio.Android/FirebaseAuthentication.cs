using Firebase.Auth;
using System.Threading.Tasks;
using NGS_Studio.Services;
using NGS_Studio.Droid;
using Android.Gms.Extensions;

[assembly: Xamarin.Forms.Dependency(typeof(FirebaseAuthentication))]
namespace NGS_Studio.Droid
{
    public class FirebaseAuthentication : IFireBaseAuthentication
    {

        public async Task<bool> CreateUser(string username, string email, string password)
        {
            var authResult = await FirebaseAuth.Instance
                    .CreateUserWithEmailAndPasswordAsync(email, password);

            var userProfileChangeRequestBuilder = new UserProfileChangeRequest.Builder();
            userProfileChangeRequestBuilder.SetDisplayName(username);

            var userProfileChangeRequest = userProfileChangeRequestBuilder.Build();
            await authResult.User.UpdateProfileAsync(userProfileChangeRequest);
            return await Task.FromResult(true);
        }

        public bool IsSignIn()
            => FirebaseAuth.Instance.CurrentUser != null;

        public async Task ResetPassword(string email)
            => await FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);

        public async Task<string> SignIn(string email, string password)
        {
            var authResult = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
            var token = await (FirebaseAuth.Instance.CurrentUser.GetIdToken(false).AsAsync<GetTokenResult>());
            return token.Token;
        }

        public void SignOut()
            => FirebaseAuth.Instance.SignOut();

        public async Task<string> GetToken()
        {
            var token = await FirebaseAuth.Instance.CurrentUser.GetIdToken(false).AsAsync<GetTokenResult>();
            return token.Token;
        }
    }
}