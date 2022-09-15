using NGS_Studio.Views;
using Xamarin.Forms;
using System;
using NGS_Studio.Services;
using System.Windows.Input;
using NGS_Studio.Data;
using NGS_Studio.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NGS_Studio.ViewModels
{
    public class LoginViewModel_orig : BaseViewModel
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
        public ICommand LoadCommand { get; protected set; }

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

        /// <summary>
        /// 
        /// </summary>
        public LoginViewModel_orig()
        {
            OwnerLoginCommand = new Command(OnOwnerLoginClicked);
            ForgotPasswordCommand = new Command(OnForgotPasswordClicked);
            LoadCommand = new Command(OnPageAppearing);
            emailEntry = string.Empty;
            PasswordEntry = string.Empty;

        }

        /// <summary>
        /// 
        /// </summary>
        private async void OnOwnerLoginClicked()
        {

            if (string.IsNullOrEmpty(EmailEntry) || string.IsNullOrEmpty(PasswordEntry))
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            else
            {
                try
                {
                    var authService = DependencyService.Resolve<IFireBaseAuthentication>();
                    Globals.Instance.AuthToken = await authService.SignIn(EmailEntry.Trim(), PasswordEntry);
                    await Shell.Current.GoToAsync($"/{nameof(OwnerDetailsPage)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    await Shell.Current
                        .DisplayAlert("SignIn", "Error: Can not complete login", "OK");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private async void OnForgotPasswordClicked()
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"/{nameof(ForgotPasswordPage)}");
        }


        public static List<Task> TaskList = new List<Task>();

        /// <summary>
        /// 
        /// </summary>
        private async void OnPageAppearing()
        {
            var dp = DependencyService.Resolve<IFireBaseAuthentication>();
            if (dp.IsSignIn())
            {
                await Shell.Current.GoToAsync($"/{nameof(OwnerDetailsPage)}");
                Globals.Instance.AuthToken = await dp.GetToken();
            }
        }
    }
}

