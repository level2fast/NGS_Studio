using NGS_Studio.Views;
using NGS_Studio.Models;
using Xamarin.Forms;
using NGS_Studio.Services;

namespace NGS_Studio.ViewModels
{
    public class BarberInfoAddBarberViewModel : BaseViewModel
    {

        private string name;
        private string email;
        private string phoneNumber;

        //The commanding interface provides an alternative approach 
        //to implementing commands that is much better suited to
        //the MVVM architecture.The ViewModel itself can contain
        //commands, which are methods that are executed in reaction
        //to a specific activity in the View such as a Button click
        //.Data bindings are defined between these commands and the Button.
        public Command AddBarberCommand { get; }

        public BarberInfoAddBarberViewModel()
        {
            Title = "Barber Info";
            AddBarberCommand = new Command(OnAddBarberCommand);

        }

        public string NameEntry
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string EmailEntry
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        public string PhoneNumberEntry
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value);
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
                    //AddUser return true if data insert successfuly     
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
                    // Notify user that they are already signed up
                    await App.Current.MainPage.DisplayAlert(NameEntry + " Already added to NGS", "Try again", "OK");
                    await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
                }
            }
        }

    }
}

