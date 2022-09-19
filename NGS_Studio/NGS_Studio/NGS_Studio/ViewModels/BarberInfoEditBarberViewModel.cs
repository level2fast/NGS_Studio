using NGS_Studio.Models;
using Xamarin.Forms;
using NGS_Studio.Services;

namespace NGS_Studio.ViewModels
{
    public class BarberInfoEditBarberViewModel : BaseViewModel
    {
        private User _barbers;
        public Command EditBarberCommand { get; }
        public User Barber
        {
            get => _barbers;
            set => SetProperty(ref _barbers, value);
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
                    await App.Current.MainPage.DisplayAlert(Barber.Name + " Does not exist in NGS", "Try adding them", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    // Add user to database using Firebase RealTime Database service     
                    User usr = new User
                    {
                        Name = Barber.Name,
                        Email = Barber.Email,
                        PhoneNumber = masked.reformatPhoneNumber(Barber.PhoneNumber),
                        IsBarber = true,
                        Barber = Barber.Name
                    };

                    var user = await UserTableService.UpdateUser(usr);  

                    if (user)
                    {
                        await Application.Current.MainPage.DisplayAlert("Success", Barber.Name + " updated", "Ok");
                        Barber.Name = null;
                        Barber.Email = null;
                        Barber.PhoneNumber = null;

                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Failed to Add ", "OK");
                    }
                }
            }
        }

    }
}

