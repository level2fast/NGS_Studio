using NGS_Studio.Views;
using NGS_Studio.Models;
using Xamarin.Forms;
using NGS_Studio.Services;

namespace NGS_Studio.ViewModels
{
    public class BarberInfoEditBarberViewModel : BaseViewModel
    {
        private User barber;

        //The commanding interface provides an alternative approach 
        //to implementing commands that is much better suited to
        //the MVVM architecture.The ViewModel itself can contain
        //commands, which are methods that are executed in reaction
        //to a specific activity in the View such as a Button click
        //.Data bindings are defined between these commands and the Button.
        public Command EditBarberCommand { get; }
        public User Barber
        {
            get => barber;
            set => SetProperty(ref barber, value);
        }
        public BarberInfoEditBarberViewModel()
        {
            Title = "Edit Barber";
            EditBarberCommand = new Command(OnEditBarberCommand);

        }

        async void OnEditBarberCommand(object sender)
        {
            MaskedBehavior masked = new MaskedBehavior();
            if (!string.IsNullOrWhiteSpace(Barber.Name) && !string.IsNullOrWhiteSpace(Barber.Email) &&
                !string.IsNullOrWhiteSpace(Barber.PhoneNumber))
            {
                var usertemp = await UserTableService.GetUser(masked.reformatPhoneNumber(Barber.PhoneNumber));
                // Check to see if user is not in database
                if (usertemp == null)
                {
                    // Notify owner that this User does not exist in the database
                    await App.Current.MainPage.DisplayAlert(Barber.Name + " Does not exist in PrimeCutz", "Try adding them", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    // call AddUser function which we define in Firebase helper class    
                    User usr = new User
                    {
                        Name = Barber.Name,
                        Email = Barber.Email,
                        PhoneNumber = masked.reformatPhoneNumber(Barber.PhoneNumber),
                        IsBarber = true,
                        Barber = Barber.Name
                    };
                    var user = await UserTableService.UpdateUser(usr);
                    //AddUser return true if data insert successfuly     
                    if (user)
                    {
                        await App.Current.MainPage.DisplayAlert("Success", Barber.Name + " updated", "Ok");
                        Barber.Name = null;
                        Barber.Email = null;
                        Barber.PhoneNumber = null;

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

