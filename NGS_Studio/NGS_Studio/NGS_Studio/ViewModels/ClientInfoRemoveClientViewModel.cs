using NGS_Studio.Models;
using Xamarin.Forms;
using NGS_Studio.Services;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace NGS_Studio.ViewModels
{
    public class ClientInfoRemoveClientViewModel : BaseViewModel
    {
        private IList<User> clients;
        private User item = null;
        public ICommand LoadCommand { get; protected set; }
        public ICommand ClientSelectionChangedCommand { get; set; }
        public IList<User> Clients
        {
            get => clients;
            set => SetProperty(ref clients, value);

        }
        public User CurrentItem
        {
            get => item;
            set => SetProperty(ref item, value);

        }
        public ClientInfoRemoveClientViewModel()
        {
            Title = "Remove Client";
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
            bool answer = await App.Current.MainPage.DisplayAlert("Remove", "Are you sure you want to remove " + usr.Name + "?", "Yes", "No");
            if (answer == true)
            {
                bool result = await UserTableService.DeleteUser(usr);
                if (result)
                {
                    await App.Current.MainPage.DisplayAlert("Removed", usr.Name + "from NGS", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else 
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Could not remove from NGS", "OK");
                    await Shell.Current.GoToAsync("..");
                }
            }
        }

    }
}

