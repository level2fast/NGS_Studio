using NGS_Studio.Views;
using NGS_Studio.Models;
using Xamarin.Forms;
using NGS_Studio.Services;

namespace NGS_Studio.ViewModels
{
    public class BarberInfoAddBarberViewModel : BaseViewModel
    {

        private string _name;
        private string _email;
        private string _phoneNumber;

        public Command AddBarberCommand { get; }

        public BarberInfoAddBarberViewModel()
        {
            Title = "Barber Info";
            AddBarberCommand = new Command(OnAddBarberCommand);

        }

        public string NameEntry
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string EmailEntry
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        public string PhoneNumberEntry
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        async void OnAddBarberCommand(object sender)
        {
            MaskedBehavior masked = new MaskedBehavior();
            if (!string.IsNullOrWhiteSpace(NameEntry) && !string.IsNullOrWhiteSpace(EmailEntry) &&
                !string.IsNullOrWhiteSpace(PhoneNumberEntry))
            {
                var usertemp = await UserTableService.GetUser(PhoneNumberEntry);
                // Check to see if user is already in database
                if (usertemp == null)
                {
                    // call AddUser function which we define in Firebase helper class    
                    User usr = new User { Name = NameEntry, 
                                          Email = EmailEntry, 
                                          PhoneNumber = masked.reformatPhoneNumber(PhoneNumberEntry), 
                                          IsBarber = true, 
                                          Barber = NameEntry };

                    var user = await UserTableService.AddUser(usr); 

                    if (user)
                    {
                        await App.Current.MainPage.DisplayAlert("Success", NameEntry + " Added to NGS ", "Ok");
                        NameEntry = null;
                        EmailEntry = null;
                        PhoneNumberEntry = null;

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Failed to Add " , "OK");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(NameEntry + " Already added to NGS", "Try again", "OK");
                    await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
                }
            }
        }

    }
}

