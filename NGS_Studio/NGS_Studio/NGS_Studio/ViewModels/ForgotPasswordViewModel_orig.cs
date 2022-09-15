using NGS_Studio.Services;
using System;
using Xamarin.Forms;
using System.Windows.Input;
using NGS_Studio.Views;

namespace NGS_Studio.ViewModels
{
    public class ForgotPasswordViewModel_orig : BaseViewModel
    {
        private string email;

        public ForgotPasswordViewModel_orig()
        {
            Title = "Forgot Password";
            ResetPasswordCommand = new Command(OnResetPassword);
        }

        private async void OnResetPassword(object obj)
        {
            try
            {

                var authService = DependencyService.Resolve<IFireBaseAuthentication>();
                await authService.ResetPassword(Email);

                await Xamarin.Forms.Shell.Current
                    .DisplayAlert("Password Reset", "Password recovery sent, check your email", "OK");

                await Xamarin.Forms.Shell.Current
                    .GoToAsync($"/{nameof(LoginPage)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Xamarin.Forms.Shell.Current
                    .DisplayAlert("Password Reset", "An error occurs", "OK");
            }
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public ICommand ResetPasswordCommand { get; }
    }
}

