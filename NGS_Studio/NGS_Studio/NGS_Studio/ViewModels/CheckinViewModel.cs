﻿using NGS_Studio.Views;
using Xamarin.Forms;

namespace NGS_Studio.ViewModels
{
    public class CheckinViewModel : BaseViewModel
    {
        public Command CheckinCommand { get; }
        public Command YesCheckinCommand { get; }
        public Command NoCheckinCommand { get; }

        public CheckinViewModel()
        {
            Title = "Checkin";
            YesCheckinCommand = new Command(OnYesCheckinClicked);
            NoCheckinCommand = new Command(OnNoCheckinClicked);
        }
        private async void OnYesCheckinClicked(object sender)
        {
            // Prefixing with `/`
            // The route hierarchy will be searched from the specified route,
            // downwards from the current position. The matching page will be
            // pushed to the navigation stack
            await Shell.Current.GoToAsync($"/{nameof(ClientCheckinPage)}");
        }

        private async void OnNoCheckinClicked()
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"/{nameof(PhoneNumberCheckinPage)}");
        }
    }
}

