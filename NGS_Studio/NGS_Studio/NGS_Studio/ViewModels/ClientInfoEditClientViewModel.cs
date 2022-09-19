using NGS_Studio.Views;
using NGS_Studio.Models;
using Xamarin.Forms;
using NGS_Studio.Services;

namespace NGS_Studio.ViewModels
{
    public class ClientInfoEditClientViewModel : BaseViewModel
    {
        private User _client;

        public Command EditClientCommand { get; }
        public User Client
        {
            get => _client;
            set => SetProperty(ref _client, value);
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

                if (usertemp == null)
                {
                    await Application.Current.MainPage.DisplayAlert(Client.Name + " Does not exist in NGS", "Try adding them", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    User usr = new User
                    {
                        Name = Client.Name,
                        Email = Client.Email,
                        PhoneNumber = masked.reformatPhoneNumber(Client.PhoneNumber),
                        IsClient = true,

                    };

                    var user = await UserTableService.UpdateUser(usr);
  
                    if (user)
                    {
                        await Application.Current.MainPage.DisplayAlert("Success", Client.Name + " updated", "Ok");
                        Client.Name = null;
                        Client.Email = null;
                        Client.PhoneNumber = null;

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

