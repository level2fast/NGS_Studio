using Xamarin.Forms;
using NGS_Studio.Views;

namespace NGS_Studio.ViewModels
{
    public class OwnerDetailsViewModel : BaseViewModel
    {

        //The commanding interface provides an alternative approach 
        //to implementing commands that is much better suited to
        //the MVVM architecture.The ViewModel itself can contain
        //commands, which are methods that are executed in reaction
        //to a specific activity in the View such as a Button click
        //.Data bindings are defined between these commands and the Button.
        public Command OwnerInfoCommand { get; }
        public Command BarberInfoCommand { get; }
        public Command ClientInfoCommand { get; }

        public OwnerDetailsViewModel()
        {
            Title = "Owner Details";
            OwnerInfoCommand = new Command(OnOwnerInfoClicked);
            BarberInfoCommand = new Command(OnBarberInfoClicked);
            ClientInfoCommand = new Command(OnClientInfoClicked);
        }

        private async void OnOwnerInfoClicked(object sender)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"/{nameof(OwnerInfoPage)}");
        }

        private async void OnBarberInfoClicked(object sender)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"/{nameof(BarberInfoPage)}");

        }
        private async void OnClientInfoClicked(object sender)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"/{nameof(ClientInfoPage)}");

        }
    }
}

