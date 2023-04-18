using NGS_Studio.Views;
using NGS_Studio.Models;
using Xamarin.Forms;

namespace NGS_Studio.ViewModels
{
    public class OwnerLoginViewModel : BaseViewModel
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
        public OwnerLoginViewModel()
        {
            Title = "Owner Login";
            OwnerLoginCommand = new Command(OnOwnerLoginClicked);
        }

        private async void OnOwnerLoginClicked()
        {

            //null or empty field validation, check weather email and password is null or empty    

            if (string.IsNullOrEmpty(EmailEntry) || string.IsNullOrEmpty(PasswordEntry))
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
                
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"/{nameof(OwnerDetailsPage)}");


        }
        bool AreCredentialsCorrect(User user)
        {
            return user.Username == Constants.Username && user.Password == Constants.Password;
        }

    }
}

