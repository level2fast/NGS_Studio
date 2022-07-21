using NGS_Studio.Models;
using Xamarin.Forms;
using NGS_Studio.Services;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Collections.ObjectModel;

namespace NGS_Studio.ViewModels
{
    public class BarberInfoRemoveBarberViewModel : BaseViewModel
    {
        private ObservableCollection<User> barbers;
        private User item = null;
        public ICommand LoadCommand { get; protected set; }
        public ICommand BarberSelectionChangedCommand { get; set; }
        public ObservableCollection<User> Barbers
        {
            get => barbers;
            set => SetProperty(ref barbers, value);

        }
        public User CurrentItem
        {
            get => item;
            set => SetProperty(ref item, value);
        }

        public BarberInfoRemoveBarberViewModel()
        {
            Title = "Remove Barber";
            BarberSelectionChangedCommand = new Command(OnBarberSelectionChanged);
            LoadCommand = new AsyncCommand(async () =>
            {
                // load data async
                var temp = await UserTableService.GetAllBarbers();
                Barbers = new ObservableCollection<User>(temp);
            });
        }

        private async void OnBarberSelectionChanged(object userObject)
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
                        Barbers.Remove(usr);
                        await App.Current.MainPage.DisplayAlert("Removed", usr.Name + "from PrimeCutz", "OK");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Could not remove from PrimeCutz", "OK");
                    }
                }
                CurrentItem = null;
            }

        }

    }
}

