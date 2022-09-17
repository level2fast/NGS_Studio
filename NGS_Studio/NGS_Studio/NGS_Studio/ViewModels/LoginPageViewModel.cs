using NGS_Studio.Data;
using NGS_Studio.Services;
using NGS_Studio.Validators;
using NGS_Studio.Validators.Rules;
using NGS_Studio.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace NGS_Studio.ViewModels
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginPageViewModel : LoginViewModel
    {
        #region Fields

        private ValidatableObject<string> password;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public LoginPageViewModel()
        {
            this.InitializeProperties();
            this.AddValidationRules();
            LoginCommand = new Command(this.LoginClickedAsync);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClickedAsync);
            this.SocialMediaLoginCommand = new Command(this.SocialLoggedIn);
        }

        #endregion

        #region property

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
        /// </summary>
        public ValidatableObject<string> Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.SetProperty(ref this.password, value);
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public Command ForgotPasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the social media login button is clicked.
        /// </summary>
        public Command SocialMediaLoginCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Check the password is null or empty
        /// </summary>
        /// <returns>Returns the fields are valid or not</returns>
        public bool AreFieldsValid()
        {
            bool isEmailValid = this.Email.Validate();
            bool isPasswordValid = this.Password.Validate();
            return isEmailValid && isPasswordValid;
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.Password = new ValidatableObject<string>();
        }

        /// <summary>
        /// Validation rule for password
        /// </summary>
        private void AddValidationRules()
        {
            this.Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password Required" });
        }

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void LoginClickedAsync(object obj)
        {
            if (this.AreFieldsValid())
            {
                try
                {
                    var authService = DependencyService.Resolve<IFireBaseAuthentication>();
                    Globals.Instance.AuthToken = await authService.SignIn(Email.Value, Password.Value);
                    var usertemp = await UserTableService.GetUserByEmail(Email.Value);
                    if (usertemp.IsOwner)
                    {
                        await Shell.Current.GoToAsync($"/{nameof(OwnerDetailsPage)}");
                    }
                    else
                    { 
                        await Shell.Current.GoToAsync($"/{nameof(ClientCheckinPage)}");
                        MessagingCenter.Send<object, (bool, string)>(this, "ChangeCheckinSection",
                            (false, Constants.CheckinContent));
                    }
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
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {

        }

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void ForgotPasswordClickedAsync(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"/{nameof(SimpleForgotPasswordPage)}");
        }

        /// <summary>
        /// Invoked when social media login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SocialLoggedIn(object obj)
        {
            // Do something
        }

        #endregion
    }
}