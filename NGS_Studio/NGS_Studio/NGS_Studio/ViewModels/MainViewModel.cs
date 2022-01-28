using NGS_Studio.Views;
using Xamarin.Forms;
using NGS_Studio.Models;
using System.Collections.ObjectModel;
using NGS_Studio.Services;

namespace NGS_Studio.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<User> users = new ObservableCollection<User>();
        //The commanding interface provides an alternative approach 
        //to implementing commands that is much better suited to
        //the MVVM architecture.The ViewModel itself can contain
        //commands, which are methods that are executed in reaction
        //to a specific activity in the View such as a Button click
        //.Data bindings are defined between these commands and the Button.
        public Command ClientCheckinCommand { get; }
        public Command OwnerLoginCommand { get; }

        public ObservableCollection<User> ListUsers
        {
            get => users;
            set => SetProperty(ref users, value);
        }

        public MainViewModel()
        {
            Title = "NGS";
            LogoName = "NgsLogo";
            OwnerLoginCommand = new Command(OnOwnerLoginClicked);
            ClientCheckinCommand = new Command(OnClientCheckinClicked);
        }

        private async void OnClientCheckinClicked()
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"/{nameof(CheckinPage)}");
        }
        private async void OnOwnerLoginClicked()
        {
            // Prefixing with `/`
            // The route hierarchy will be searched from the specified route,
            // downwards from the current position. The matching page will be
            // pushed to the navigation stack
            await Shell.Current.GoToAsync($"/{nameof(OwnerDetailsPage)}");
        }

        public async void OnAppearing()
        {
            if (!DependencyService.Resolve<IFireBaseAuthentication>().IsSignIn())
            {
                // go to login page
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }
    }
}

