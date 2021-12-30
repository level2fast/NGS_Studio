using NGS_Studio.Views;
using Xamarin.Forms;

namespace NGS_Studio.ViewModels
{
    public class BarberInfoViewModel : BaseViewModel
    {


        //The commanding interface provides an alternative approach 
        //to implementing commands that is much better suited to
        //the MVVM architecture.The ViewModel itself can contain
        //commands, which are methods that are executed in reaction
        //to a specific activity in the View such as a Button click
        //.Data bindings are defined between these commands and the Button.
        public Command AddBarberCommand { get; }
        public Command RemoveBarberCommand { get; }
        public Command EditBarbersCommand { get; }


        public BarberInfoViewModel()
        {
            Title = "Barber Info";
            AddBarberCommand = new Command(OnAddBarberClicked);
            RemoveBarberCommand = new Command(OnRemoveBarberClicked);
            EditBarbersCommand = new Command(OnEditBarbersClicked);
        }


        private async void OnAddBarberClicked()
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"{nameof(BarberInfoAddBarberPage)}");
        }
        private async void OnRemoveBarberClicked()
        {
            await Shell.Current.GoToAsync($"{nameof(BarberInfoRemoveBarberPage)}");
        }
        private async void OnEditBarbersClicked()
        {
            await Shell.Current.GoToAsync($"{nameof(BarberInfoDetailsPage)}");
        }

    }
}

