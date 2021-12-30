using NGS_Studio.Models;
using Xamarin.Forms;
using NGS_Studio.Services;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace NGS_Studio.ViewModels
{
    public class BarberInfoRemoveBarberViewModel : BaseViewModel
    {
        private IList<User> barbers;
        private User item = null;
        public ICommand LoadCommand { get; protected set; }
        public ICommand BarberSelectionChangedCommand { get; set; }
        public IList<User> Barbers
        {
            get => barbers;
            set => SetProperty(ref barbers, value);

        }

        public BarberInfoRemoveBarberViewModel()
        {
            Title = "Remove Barber";
            BarberSelectionChangedCommand = new Command(OnBarberSelectionChanged);
            LoadCommand = new AsyncCommand(async () =>
            {
                // load data async
                Barbers = await UserTableService.GetAllBarbers();
            });
        }

        private async void OnBarberSelectionChanged(object userObject)
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

