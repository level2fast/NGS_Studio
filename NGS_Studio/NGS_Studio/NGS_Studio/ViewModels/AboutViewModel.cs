using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NGS_Studio.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private const string _phoneNumber = "6195825889";

        public ICommand OpenWebCommandDirections { get; }
        public ICommand CallUsCommand { get; }

        public ICommand LearnMoreCommand { get; }
        public string PhoneNumberNGS
        {
            get { return _phoneNumber; }
        }


        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommandDirections = new Command(OnClientDirectionsClicked);
            CallUsCommand = new Command(OnCallUsBtnClicked);
            LearnMoreCommand = new Command(OnLearnMoreBtnClicked);
        }


        async void OnLearnMoreBtnClicked(object sender)
        {
            try
            {
                await Launcher.OpenAsync("https://www.facebook.com/PRIMECUTZSD/");
                return;
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
                Console.WriteLine("{0} Exception caught.", ex);
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                Console.WriteLine("{0} Exception caught.", ex);
            }
        }
        void OnCallUsBtnClicked(object sender)
        {
            try
            {
                PhoneDialer.Open(PhoneNumberNGS);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
                Console.WriteLine("{0} Exception caught.", ex);
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                Console.WriteLine("{0} Exception caught.", ex);
            }
        }
        async void OnClientDirectionsClicked(object sender)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                await Launcher.OpenAsync("http://maps.apple.com/?address=4456+Vander+Ave+Suite+8+San+Diego+CA+92120&t=m");
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                // open the maps app directly
                await Launcher.OpenAsync("geo:0,0?q=4456+Vander+Ave+Suite+8+San+Diego+CA+92120");
            }
        }
    }
}