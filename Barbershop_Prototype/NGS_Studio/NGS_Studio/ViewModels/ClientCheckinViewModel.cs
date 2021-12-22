using NGS_Studio.Views;
using NGS_Studio.Models;
using Xamarin.Forms;

namespace NGS_Studio.ViewModels
{
    public class ClientCheckinViewModel : BaseViewModel
    {
        private string nameEntry;
        private string emailEntry;
        private string phoneNumberEntry;

        // The commanding interface provides an alternative approach 
        // to implementing commands that is much better suited to
        // the MVVM architecture.The ViewModel itself can contain
        // commands, which are methods that are executed in reaction
        // to a specific activity in the View such as a Button click
        // Data bindings are defined between these commands and the Button.
        public Command ClientSubmitCommand { get; }

        public string SelectedBarber { get; set; }
       // public IList<string> Barbers { get; set; }

        public ClientCheckinViewModel()
        {
            Title = "Client Checkin";
            ClientSubmitCommand = new Command(OnClientSumbitClicked);
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


        private async void OnClientCheckinClicked()
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"/{nameof(OwnerDetailsPage)}");
        }
        bool IsAlreadyRegistered(string phoneNumber)
        {
            return phoneNumber == Constants.Username;
        }

        async void OnClientSumbitClicked(object sender)
        {
            MaskedBehavior masked = new MaskedBehavior();
            if (!string.IsNullOrWhiteSpace(NameEntry) && !string.IsNullOrWhiteSpace(EmailEntry) &&
                !string.IsNullOrWhiteSpace(PhoneNumberEntry))
            {
                //ToDo SD: check to see if phone number is in database already
                //if so display alert saying you are already registered with NGS
                await App.Database.SaveUserAsync(new User
                {
                    Name = NameEntry,
                    Email = EmailEntry,
                    PhoneNumber = masked.reformatPhoneNumber(PhoneNumberEntry),
                    Barber = SelectedBarber
                });

                NameEntry = EmailEntry = PhoneNumberEntry = string.Empty;
                SelectedBarber = " ";
                await Application.Current.MainPage.DisplayAlert("Thanks!", "Your barber will be with you shortly", "OK");
                await Shell.Current.GoToAsync($"/{nameof(CheckinPage)}");
            }
            else 
            {
                await Application.Current.MainPage.DisplayAlert("Cannot register", "Information missing", "OK");
            }
        }
    }
}

