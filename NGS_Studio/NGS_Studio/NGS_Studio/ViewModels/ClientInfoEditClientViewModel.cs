using NGS_Studio.Views;
using NGS_Studio.Models;
using Xamarin.Forms;
using NGS_Studio.Services;

namespace NGS_Studio.ViewModels
{
    public class ClientInfoEditClientViewModel : BaseViewModel
    {
        private User client;

        //The commanding interface provides an alternative approach 
        //to implementing commands that is much better suited to
        //the MVVM architecture.The ViewModel itself can contain
        //commands, which are methods that are executed in reaction
        //to a specific activity in the View such as a Button click
        //.Data bindings are defined between these commands and the Button.
        public Command EditClientCommand { get; }
        public User Client
        {
            get => client;
            set => SetProperty(ref client, value);
        }
        public ClientInfoEditClientViewModel()
        {
            Title = "Edit Client";
            EditClientCommand = new Command(OnEditClientCommand);

        }

        async void OnEditClientCommand(object sender)
        {
            MaskedBehavior masked = new MaskedBehavior();
            if (!string.IsNullOrWhiteSpace(Client.Name) && !string.IsNullOrWhiteSpace(Client.Email) &&
                !string.IsNullOrWhiteSpace(Client.PhoneNumber))
            {
                var usertemp = await UserTableService.GetUser(masked.reformatPhoneNumber(Client.PhoneNumber));
                // Check to see if user is not in database
                if (usertemp == null)
                {
                    // Notify owner that this User does not exist in the database
                    await App.Current.MainPage.DisplayAlert(Client.Name + " Does not exist in PrimeCutz", "Try adding them", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    // call AddUser function which we define in Firebase helper class    
                    User usr = new User
                    {
                        Name = Client.Name,
                        Email = Client.Email,
                        PhoneNumber = masked.reformatPhoneNumber(Client.PhoneNumber),
                        IsClient = true,

                    };
                    var user = await UserTableService.UpdateUser(usr);
                    //AddUser return true if data insert successfuly     
                    if (user)
                    {
                        await App.Current.MainPage.DisplayAlert("Success", Client.Name + " updated", "Ok");
                        Client.Name = null;
                        Client.Email = null;
                        Client.PhoneNumber = null;

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Failed to Add ", "OK");
                    }
                }
            }
        }

    }
}

