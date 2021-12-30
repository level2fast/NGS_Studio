using NGS_Studio.Views;
using NGS_Studio.Models;
using Xamarin.Forms;
using NGS_Studio.Services;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Windows.Input;

namespace NGS_Studio.ViewModels
{
    public class ClientCheckinViewModel : BaseViewModel
    {
        private string nameEntry;
        private string emailEntry;
        private string phoneNumberEntry;
        private User barber;
        private IList<User> barbers;


        // The commanding interface provides an alternative approach 
        // to implementing commands that is much better suited to
        // the MVVM architecture.The ViewModel itself can contain
        // commands, which are methods that are executed in reaction
        // to a specific activity in the View such as a Button click
        // Data bindings are defined between these commands and the Button.
        public Command ClientSubmitCommand { get; }
        public ICommand LoadCommand { get; protected set; }
        public ClientCheckinViewModel()
        {
            Title = "Client Checkin";
            ClientSubmitCommand = new Command(OnClientSumbitClicked);

            LoadCommand = new AsyncCommand(async () =>
            {
                // load data async
                Barbers = await UserTableService.GetAllBarbers();
            });
        }
        public IList<User> Barbers
        {
            get => barbers;
            set => SetProperty(ref barbers, value);
        }
        public User SelectedBarber
        {
            get => barber;
            set => SetProperty(ref barber, value);
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
                var usertemp = await UserTableService.GetUser(PhoneNumberEntry);
                // Check to see if user is already signed up
                if (usertemp == null)
                {
                    // call AddUser function which we define in Firebase helper class    
                    User usr = new User { Name = NameEntry, Email = emailEntry, PhoneNumber = masked.reformatPhoneNumber(PhoneNumberEntry), IsClient = true, Barber = SelectedBarber.Name };
                    var user = await UserTableService.AddUser(usr);
                    //AddUser return true if data insert successfuly     
                    if (user)
                    {
                        await App.Current.MainPage.DisplayAlert("Thanks!", " Your barber will be with shortly", "Ok");
                        // Navigate to Wellcom page after successfuly SignUp    
                        // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                        await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "SignUp Fail", "OK");
                    }
                }
                else
                {
                    // Notify user that they are already signed up
                    await App.Current.MainPage.DisplayAlert("You are already signed up", "Please login", "OK");
                    await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
                }
            }
            else 
            {
                await Application.Current.MainPage.DisplayAlert("Cannot register", "Information missing", "OK");
            }
        }
    }
}

