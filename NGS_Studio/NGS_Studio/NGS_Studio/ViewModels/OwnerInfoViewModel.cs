using NGS_Studio.Views;
using Xamarin.Forms;

namespace NGS_Studio.ViewModels
{
    public class OwnerInfoViewModel : BaseViewModel
    {

        public Command EmailPromotionsCommand { get; }
        public Command EmailClientListCommand { get; }

        public OwnerInfoViewModel()
        {
            Title = "Owner";
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
            await Shell.Current.GoToAsync($"/{nameof(NewEmailPage)}?{nameof(NewEmailViewModel.EmailPromotions)}={false}&{nameof(NewEmailViewModel.EmailClientList)}={true}");
        }
    }
}

