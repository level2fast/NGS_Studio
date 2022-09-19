using Xamarin.Forms;
using NGS_Studio.Views;
using NGS_Studio.Data;

namespace NGS_Studio.ViewModels
{
    public class OwnerDetailsViewModel : BaseViewModel
    {
        public Command OwnerInfoCommand { get; }
        public Command BarberInfoCommand { get; }
        public Command ClientInfoCommand { get; }

        bool toggledState = false;
        public bool ClientCheckSwitchState
        {
            get 
            { 
                return toggledState; 
            }
            set 
            { 
                SetProperty(ref toggledState, value);
                SetClientCheckinState(value);
            }
        }
        public OwnerDetailsViewModel()
        {
            Title = "Owner";
            OwnerInfoCommand = new Command(OnOwnerInfoClicked);
            BarberInfoCommand = new Command(OnBarberInfoClicked);
            ClientInfoCommand = new Command(OnClientInfoClicked);
            MessagingCenter.Send<object, (bool, string)>(this, "ChangeCheckinSection",
                (true, Constants.CheckinContent));
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

        private void SetClientCheckinState(bool state)
        {
            MessagingCenter.Send<object, (bool, string)>(this, "ChangeCheckinSection", 
                (state, Constants.CheckinContent));

        }
    }
}

