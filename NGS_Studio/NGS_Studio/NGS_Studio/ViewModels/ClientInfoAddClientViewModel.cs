using NGS_Studio.Models;
using Xamarin.Forms;
using NGS_Studio.Services;
using NGS_Studio.Views;

namespace NGS_Studio.ViewModels
{
    public class ClientInfoAddClientViewModel : BaseViewModel
    {

        private string _name;
        private string _email;
        private string _phoneNumber;

        //The commanding interface provides an alternative approach 
        //to implementing commands that is much better suited to
        //the MVVM architecture.The ViewModel itself can contain
        //commands, which are methods that are executed in reaction
        //to a specific activity in the View such as a Button click
        //.Data bindings are defined between these commands and the Button.
        public Command AddClientCommand { get; }

        public ClientInfoAddClientViewModel()
        {
            Title = "Add Client";
            AddClientCommand = new Command(OnAddClientClicked);

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

        async void OnAddClientClicked(object sender)
        {
            MaskedBehavior masked = new MaskedBehavior();
            if (!string.IsNullOrWhiteSpace(NameEntry) && !string.IsNullOrWhiteSpace(EmailEntry) &&
                !string.IsNullOrWhiteSpace(PhoneNumberEntry))
            {
                var usertemp = await UserTableService.GetUser(masked.reformatPhoneNumber(PhoneNumberEntry));

                if (usertemp == null)
                {
                    User usr = new User
                    {
                        Name = NameEntry,
                        Email = EmailEntry,
                        PhoneNumber = masked.reformatPhoneNumber(PhoneNumberEntry),
                        IsClient = true,
                        Barber = NameEntry
                    };

                    var user = await UserTableService.AddUser(usr);
 
                    if (user)
                    {
                        await Application.Current.MainPage.DisplayAlert("Success", NameEntry + " Added to NGS ", "Ok");
                        NameEntry = null;
                        EmailEntry = null;
                        PhoneNumberEntry = null;

                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Failed to Add ", "OK");
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

