using NGS_Studio.Views;
using Xamarin.Forms;

namespace NGS_Studio.ViewModels
{
    public class OwnerInfoViewModel : BaseViewModel
    {

        //The commanding interface provides an alternative approach 
        //to implementing commands that is much better suited to
        //the MVVM architecture.The ViewModel itself can contain
        //commands, which are methods that are executed in reaction
        //to a specific activity in the View such as a Button click
        //.Data bindings are defined between these commands and the Button.
        public Command EmailPromotionsCommand { get; }
        public Command EmailClientListCommand { get; }

        public OwnerInfoViewModel()
        {
            Title = "Barber Info";
            EmailPromotionsCommand = new Command(OnEmailPromotionsClicked);
            EmailClientListCommand = new Command(OnEmailClientListClicked);
        }

        private async void OnEmailPromotionsClicked()
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"{nameof(NewEmailPage)}?{nameof(NewEmailViewModel.EmailPromotions)}={true}?{nameof(NewEmailViewModel.EmailClientList)}={false}");
        }
        private async void OnEmailClientListClicked()
        {
            await Shell.Current.GoToAsync($"{nameof(NewEmailPage)}?{nameof(NewEmailViewModel.EmailPromotions)}={false}&{nameof(NewEmailViewModel.EmailClientList)}={true}");
        }
    }
}

