using NGS_Studio.Services;
using NGS_Studio.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace NGS_Studio.ViewModels
{
    /// <summary>
    /// ViewModel for forgot password page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ForgotPasswordViewModel : LoginViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ForgotPasswordViewModel" /> class.
        /// </summary>
        public ForgotPasswordViewModel()
        {
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.SendCommand = new Command(this.SendClickedAsync);
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Send button is clicked.
        /// </summary>
        public Command SendCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Send button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SendClickedAsync(object obj)
        {
            if (this.IsEmailFieldValid())
            {
                try
                {

                    var authService = DependencyService.Resolve<IFireBaseAuthentication>();
                    await authService.ResetPassword(Email.Value);

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
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            // Do something
        }

        #endregion
    }
}