using NGS_Studio.Models;
using Xamarin.Forms;
using NGS_Studio.Services;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Collections.ObjectModel;

namespace NGS_Studio.ViewModels
{
    public class ClientInfoRemoveClientViewModel : BaseViewModel
    {
        private ObservableCollection<User> _clients;
        private User _item = null;
        public ICommand LoadCommand { get; protected set; }
        public ICommand ClientSelectionChangedCommand { get; set; }
        public ObservableCollection<User> Clients
        {
            get => _clients;
            set => SetProperty(ref _clients, value);

        }
        public User CurrentItem
        {
            get => _item;
            set => SetProperty(ref _item, value);

        }
        public ClientInfoRemoveClientViewModel()
        {
            Title = "Remove Client";
            ClientSelectionChangedCommand = new Command(OnClientSelectionChanged);
            LoadCommand = new AsyncCommand(async () =>
            {
                var temp = await UserTableService.GetAllClients();
                Clients = new ObservableCollection<User>(temp);
            });
        }

        private async void OnClientSelectionChanged(object userObject)
        {
            if (userObject != null)
            {
                User usr = (User)userObject;
                bool answer = await App.Current.MainPage.DisplayAlert("Remove", "Are you sure you want to remove " + usr.Name + "?", "Yes", "No");
                if (answer == true)
                {
                    bool result = await UserTableService.DeleteUser(usr);
                    if (result)
                    {
                        Clients.Remove(usr);
                        await App.Current.MainPage.DisplayAlert("Removed", usr.Name + "from NGS", "OK");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Could not remove from NGS", "OK");
                    }
                }
                CurrentItem = null;
            }
        }

    }
}

