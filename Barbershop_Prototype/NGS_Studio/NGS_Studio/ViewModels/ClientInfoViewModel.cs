using NGS_Studio.Views;
using Xamarin.Forms;

namespace NGS_Studio.ViewModels
{
    public class ClientInfoViewModel : BaseViewModel
    {


        //The commanding interface provides an alternative approach 
        //to implementing commands that is much better suited to
        //the MVVM architecture.The ViewModel itself can contain
        //commands, which are methods that are executed in reaction
        //to a specific activity in the View such as a Button click
        //.Data bindings are defined between these commands and the Button.
        public Command AddClientCommand { get; }
        public Command RemoveClientCommand { get; }
        public Command ViewClientsCommand { get; }


        public ClientInfoViewModel()
        {
            Title = "Client Info";
            AddClientCommand = new Command(OnAddClientClicked);
            //RemoveClientCommand = new Command(OnRemoveClientClicked);
            ViewClientsCommand = new Command(OnViewClientsClicked);
        }


        private async void OnAddClientClicked()
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"{nameof(ClientInfoAddClientPage)}");
        }

        private async void OnViewClientsClicked()
        {
            await Shell.Current.GoToAsync($"{nameof(ClientInfoDetailsPage)}");
        }

    }
}

