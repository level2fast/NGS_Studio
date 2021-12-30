using NGS_Studio.Models;
using NGS_Studio.Services;
using NGS_Studio.Views;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace NGS_Studio.ViewModels
{
    public class ClientInfoDetailsViewModel : BaseViewModel
    {
        private IList<User> clients;

        public ICommand LoadCommand { get; protected set; }
        public ICommand ClientSelectionChangedCommand { get; set; }
        public IList<User> Clients
        {
            get => clients;
            set => SetProperty(ref clients, value);

        }
        public ClientInfoDetailsViewModel()
        {
            Title = "Client Details";
            ClientSelectionChangedCommand = new Command(OnClientSelectionChanged);
            LoadCommand = new AsyncCommand(async () =>
            {
                // load data async
                Clients = await UserTableService.GetAllClients();
            });
        }
        private async void OnClientSelectionChanged(object userObject)
        {
            User usr = (User)userObject;
            ClientInfoEditClientViewModel EditClientVM = new ClientInfoEditClientViewModel();
            EditClientVM.Client = usr;
            ClientInfoEditClientPage editClientPage = new ClientInfoEditClientPage();
            editClientPage.BindingContext = EditClientVM;
            await Shell.Current.Navigation.PushAsync(editClientPage);
        }

    }
}

