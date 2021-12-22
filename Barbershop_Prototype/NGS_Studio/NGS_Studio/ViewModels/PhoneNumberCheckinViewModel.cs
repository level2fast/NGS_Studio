using NGS_Studio.Views;
using NGS_Studio.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace NGS_Studio.ViewModels
{
    public class PhoneNumberCheckinViewModel : BaseViewModel
    {
        private MaskedBehavior masked;

        public Command PhoneNumberCheckinCommand { get; }
        public Command BarberSelectSubmitCommand { get; }

        string phoneNumberEntry = string.Empty;
        string selectedBarberName = string.Empty;
        public string PhoneNumberEntry
        {
            get { return phoneNumberEntry; }
            set { SetProperty(ref phoneNumberEntry, value); }
        }
        public string SelectedItemBarberName
        {
            get { return selectedBarberName; }
            set
            {
                if (selectedBarberName != value)
                {
                    selectedBarberName = value;
                }
            }
        }


        private bool _isCheckinContentVisible;
        private bool _isBarberSelectionContentVisible;

        public bool IsCheckinContentVisible
        {
            get{ return _isCheckinContentVisible; }
            set { SetProperty(ref _isCheckinContentVisible, value); }
        }
        public bool IsBarberSelectionContentVisible
        {
            get { return _isBarberSelectionContentVisible; }
            set { SetProperty(ref _isBarberSelectionContentVisible, value); }
        }



        public PhoneNumberCheckinViewModel()
        {
            PhoneNumberCheckinCommand = new Command(OnPhoneNumberCheckinClicked);
            BarberSelectSubmitCommand = new Command(OnBarberSelectSubmitClicked);
            masked = new MaskedBehavior();
            //display checkin content
            IsCheckinContentVisible = true;
            IsBarberSelectionContentVisible = false;
        }
        private async void OnPhoneNumberCheckinClicked()
        {

            //check to see if phone number is in database
            var user = await App.Database.GetUserAsync(masked.reformatPhoneNumber(phoneNumberEntry));
            if (user.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Not a registered client", "Please register with NGS", "OK");
                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                await Shell.Current.GoToAsync($"/{nameof(CheckinPage)}");
            }
            else
            {
                //hide phone number checkin content
                IsCheckinContentVisible = false;
                //show barber select checkin content
                IsBarberSelectionContentVisible = true;

            }
        }

        private async void OnBarberSelectSubmitClicked()
        {
            var user = await App.Database.GetUserAsync(masked.reformatPhoneNumber(phoneNumberEntry));
            User u = user.ElementAt(0);
            u.Barber = SelectedItemBarberName;
            await App.Database.UpdateUserAsync(u);
            //display "client preferred barber question label and buttons
            await Application.Current.MainPage.DisplayAlert("Thanks!", "Your barber will be with you shortly", "OK");
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"/{nameof(CheckinPage)}");
        }

    }
    public class MaskedBehavior : Behavior<Entry>
    {
        private string _mask = "";
        public string Mask
        {
            get => _mask;
            set
            {
                _mask = value;
                SetPositions();
            }
        }
        public string reformatPhoneNumber(string phoneNumber)
        {
            char[] charsToTrim = { ')', '(', '-' };
            string[] words = phoneNumber.Split();
            string temp = "";
            foreach (string word in words)
                 temp+= word.TrimEnd(charsToTrim);

            return temp;
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        IDictionary<int, char> _positions;

        void SetPositions()
        {
            if (string.IsNullOrEmpty(Mask))
            {
                _positions = null;
                return;
            }

            var list = new Dictionary<int, char>();
            for (var i = 0; i < Mask.Length; i++)
                if (Mask[i] != 'X')
                    list.Add(i, Mask[i]);

            _positions = list;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = sender as Entry;

            var text = entry.Text;

            if (string.IsNullOrWhiteSpace(text) || _positions == null)
                return;

            if (text.Length > _mask.Length)
            {
                entry.Text = text.Remove(text.Length - 1);
                return;
            }

            foreach (var position in _positions)
                if (text.Length >= position.Key + 1)
                {
                    var value = position.Value.ToString();
                    if (text.Substring(position.Key, 1) != value)
                        text = text.Insert(position.Key, value);
                }

            if (entry.Text != text)
                entry.Text = text;
        }
    }
}
