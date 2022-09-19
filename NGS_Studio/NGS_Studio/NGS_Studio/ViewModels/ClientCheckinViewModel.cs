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
        private string _nameEntry;
        private string _emailEntry;
        private string _phoneNumberEntry;
        private User _barber;
        private IList<User> _barbers;

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
            get => _barbers;
            set => SetProperty(ref _barbers, value);
        }
        public User SelectedBarber
        {
            get => _barber;
            set => SetProperty(ref _barber, value);
        }
        public string NameEntry
        {
            get => _nameEntry;
            set => SetProperty(ref _nameEntry, value);
        }
        public string EmailEntry
        {
            get => _emailEntry;
            set => SetProperty(ref _emailEntry, value);
        }
        public string PhoneNumberEntry
        {
            get => _phoneNumberEntry;
            set => SetProperty(ref _phoneNumberEntry, value);
        }

        private async void OnClientCheckinClicked()
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"/{nameof(OwnerDetailsPage)}");
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
                    User usr = new User { Name = NameEntry, Email = _emailEntry, PhoneNumber = masked.reformatPhoneNumber(PhoneNumberEntry), IsClient = true, Barber = SelectedBarber.Name };
                    var user = await UserTableService.AddUser(usr);

                    if (user)
                    {
                        await App.Current.MainPage.DisplayAlert("Thanks!", " Your _barber will be with shortly", "Ok");

                        // Navigate to Wellcom page after successfuly SignUp    
                        // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                        await Shell.Current.GoToAsync($"{nameof(CheckinPage)}");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Check in failed, can not add user", "OK");
                    }
                }
                else
                {
                    // Notify user that they are already signed up
                    await App.Current.MainPage.DisplayAlert("You are already signed up", "Please check in", "OK");
                    await Shell.Current.GoToAsync($"{nameof(CheckinPage)}");
                }
            }
            else 
            {
                await Application.Current.MainPage.DisplayAlert("Cannot register", "Information missing", "OK");
            }
        }
    }
}

