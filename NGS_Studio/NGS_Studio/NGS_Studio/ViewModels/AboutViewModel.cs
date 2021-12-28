using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NGS_Studio.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private const string phoneNumber = "6195825889";
        public string PhoneNumberNGS
        {
            get { return phoneNumber; }
        }

        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommandDirections = new Command(OnClientDirectionsClicked);
            CallUsCommand = new Command(OnCallUsBtnClicked);
        }

        public ICommand OpenWebCommandDirections { get; }
        public ICommand CallUsCommand { get; }

         void OnCallUsBtnClicked(object sender)
        {
            try
            {
                PhoneDialer.Open(PhoneNumberNGS);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
        async void OnClientDirectionsClicked(object sender)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                await Launcher.OpenAsync("http://maps.apple.com/?q=6461+University+Ave+San+Diego+CA");
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                // open the maps app directly
                await Launcher.OpenAsync("geo:0,0?q=6461+University+Ave+San+Diego+CA");
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                await Launcher.OpenAsync("bingmaps:?where=6461 University Ave San Diego CA");
            }
        }
    }
}