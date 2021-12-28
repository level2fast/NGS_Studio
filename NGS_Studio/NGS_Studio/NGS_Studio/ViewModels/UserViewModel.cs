using NGS_Studio.Models;
using Xamarin.Forms;


namespace NGS_Studio.ViewModels
{
    public class UserViewModel : BaseViewModel
    {

        private string nameEntry;
        private string emailEntry;
        private string phoneNumberEntry;

        public UserViewModel()
        {
        }

        public string NameEntry
        {
            get => nameEntry;
            set => SetProperty(ref nameEntry, value);
        }
        public string EmailEntry
        {
            get => emailEntry;
            set => SetProperty(ref emailEntry, value);
        }
        public string PhoneNumberEntry
        {
            get => phoneNumberEntry;
            set => SetProperty(ref phoneNumberEntry, value);
        }

        async void OnClientSumbitClicked(object sender, User usr)
        {
            MaskedBehavior masked = new MaskedBehavior();
            if (!string.IsNullOrWhiteSpace(NameEntry) && !string.IsNullOrWhiteSpace(EmailEntry) &&
                !string.IsNullOrWhiteSpace(PhoneNumberEntry))
            {
                //ToDo SD: check to see if phone number is in database already
                //if so display alert saying you are already registered with NGS
                await App.Database.SaveUserAsync(usr);

                 await Application.Current.MainPage.DisplayAlert("Complete", NameEntry + " has been added to NGS", "OK");
                 NameEntry = EmailEntry = PhoneNumberEntry = string.Empty;
            }
            else 
            {
                await Application.Current.MainPage.DisplayAlert("Cannot add to database", "Information missing", "OK");
            }
        }
    }
}

