using NGS_Studio.Views;
using NGS_Studio.Models;
using NGS_Studio.Data;
using Xamarin.Forms;
using System;
using NGS_Studio.Services;

namespace NGS_Studio.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string emailEntry;
        private string passwordEntry;
        //The commanding interface provides an alternative approach 
        //to implementing commands that is much better suited to
        //the MVVM architecture.The ViewModel itself can contain
        //commands, which are methods that are executed in reaction
        //to a specific activity in the View such as a Button click
        //.Data bindings are defined between these commands and the Button.
        public Command OwnerLoginCommand { get; }
        public Command ForgotPasswordCommand { get; }


        /// <summary>
        /// 
        /// </summary>
        public string EmailEntry
        {
            get => emailEntry;
            set => SetProperty(ref emailEntry, value);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string PasswordEntry
        {
            get => passwordEntry;
            set => SetProperty(ref passwordEntry, value);
        }
        public LoginViewModel()
        {
            Title = "Owner Login";
            OwnerLoginCommand = new Command(OnOwnerLoginClicked);
            ForgotPasswordCommand = new Command(OnForgotPasswordClicked);

        }

        private async void OnOwnerLoginClicked()
        {

            //null or empty field validation, check weather email and password is null or empty    

            if (string.IsNullOrEmpty(EmailEntry) || string.IsNullOrEmpty(PasswordEntry))
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            else
            {
                try
                {
                    var authService = DependencyService.Resolve<IFireBaseAuthentication>();
                    var token = await authService.SignIn(EmailEntry, PasswordEntry);

                    await Shell.Current.GoToAsync($"/{nameof(OwnerDetailsPage)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    await Xamarin.Forms.Shell.Current
                        .DisplayAlert("SignIn", "Error: Can not complete login", "OK");
                }


            //call GetUser function which we define in Firebase helper class    
            //var user = null;//await FirebaseHelper.GetUser(EmailEntry);
            ////firebase return null valuse if user data not found in database    
            //if (user != null)
            //    if (EmailEntry == user.Email && PasswordEntry == user.Password)
            //    {
            //        await App.Current.MainPage.DisplayAlert("Login Success", "", "Ok");
            //        //Navigate to Wellcom page after successfuly login    
            //        //pass user email to welcom page    

            //        // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            //        await Shell.Current.GoToAsync($"/{nameof(OwnerDetailsPage)}");
            //    }
            //    else
            //        await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
            //else
            //    await App.Current.MainPage.DisplayAlert("Login Fail", "User not found", "OK");
            }
        }

        private async void OnForgotPasswordClicked()
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"/{nameof(ForgotPasswordPage)}");
        }

        bool AreCredentialsCorrect(User user)
        {
            //var user = new User {
            //	Username = usernameEntry.Text,
            //	Password = passwordEntry.Text
            //};

            //var isValid = AreCredentialsCorrect (user);
            //if (isValid) {
            //	App.IsUserLoggedIn = true;
            //	Navigation.InsertPageBefore (new MainPage (), this);
            //	await Navigation.PopAsync ();
            //} else {
            //	messageLabel.Text = "Login failed";
            //	passwordEntry.Text = string.Empty;
            //}
            return user.Username == Constants.Username && user.Password == Constants.Password;
        }

    }
}

