using NGS_Studio.Models;
using NGS_Studio.Services;
using NGS_Studio.Views;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace NGS_Studio.ViewModels
{
    public class BarberInfoDetailsViewModel : BaseViewModel
    {
        private IList<User> _barbers;

        public ICommand LoadCommand { get; protected set; }
        public ICommand BarberSelectionChangedCommand { get; set; }
        public IList<User> Barbers
        {
            get => _barbers;
            set => SetProperty(ref _barbers, value);

        }
        public BarberInfoDetailsViewModel()
        {
            Title = " Select Barber";
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
            BarberInfoEditBarberViewModel EditBarberVM = new BarberInfoEditBarberViewModel();
            EditBarberVM.Barber = usr;
            BarberInfoEditBarberPage editBarberPage = new BarberInfoEditBarberPage();
            editBarberPage.BindingContext = EditBarberVM;
            await Shell.Current.Navigation.PushAsync(editBarberPage);

        }

    }
}

