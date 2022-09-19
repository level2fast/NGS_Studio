using NGS_Studio.Views;
using Xamarin.Forms;

namespace NGS_Studio.ViewModels
{
    public class ClientInfoViewModel : BaseViewModel
    {

        public Command AddClientCommand { get; }
        public Command RemoveClientCommand { get; }
        public Command EditClientsCommand { get; }


        public ClientInfoViewModel()
        {
            Title = "Client Info";
            AddClientCommand = new Command(OnAddClientClicked);
            RemoveClientCommand = new Command(OnRemoveClientClicked);
            EditClientsCommand = new Command(OnViewClientsClicked);
        }


        private async void OnAddClientClicked()
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"{nameof(ClientInfoAddClientPage)}");
        }
        private async void OnRemoveClientClicked()
        {
            await Shell.Current.GoToAsync($"{nameof(ClientInfoRemoveClientPage)}");
        }
        private async void OnViewClientsClicked()
        {
            await Shell.Current.GoToAsync($"{nameof(ClientInfoDetailsPage)}");
        }

    }
}

