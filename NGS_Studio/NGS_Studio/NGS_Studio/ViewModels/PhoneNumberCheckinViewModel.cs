﻿using NGS_Studio.Views;
using NGS_Studio.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using NGS_Studio.Services;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace NGS_Studio.ViewModels
{
    public class PhoneNumberCheckinViewModel : BaseViewModel
    {
        private IList<User> _barbers;
        private User _barber;
        private MaskedBehavior _masked;

        public Command PhoneNumberCheckinCommand { get; }
        public Command BarberSelectSubmitCommand { get; }

        public ICommand LoadCommand { get; protected set; }
        public IList<User> Barbers
        {
            get => _barbers;
            set => SetProperty(ref _barbers, value);

        }
        public User SelectedBarber
        {
            get => _barber;
            set => SetProperty(ref _barber, value);
        }
        string phoneNumberEntry = string.Empty;
        string selectedBarberName = string.Empty;
        public string PhoneNumberEntry
        {
            get { return phoneNumberEntry; }
            set { SetProperty(ref phoneNumberEntry, value); }
        }
        public string SelectedItemBarberName
        {
            get => selectedBarberName;
            set => SetProperty(ref selectedBarberName, value);
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
            _masked = new MaskedBehavior();
            IsCheckinContentVisible = true;
            IsBarberSelectionContentVisible = false;
            LoadCommand = new AsyncCommand(async () =>
            {
                Barbers = await UserTableService.GetAllBarbers();
            });
        }
        private async void OnPhoneNumberCheckinClicked()
        {
            // check to see if client is in database
            User user = await UserTableService.GetUser(_masked.reformatPhoneNumber(phoneNumberEntry));
            if (user == null)
            {
                await Application.Current.MainPage.DisplayAlert("Not a registered client", "Please register with NGS", "OK");
                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                await Shell.Current.GoToAsync($"{nameof(CheckinPage)}");
            }
            else
            {
                user.checkin=System.DateTime.Now.ToString();
                await UserTableService.UpdateUser(user);
                // hide phone number checkin content
                IsCheckinContentVisible = false;
                // show barber select checkin content
                IsBarberSelectionContentVisible = true;

            }
        }

        private async void OnBarberSelectSubmitClicked()
        {
            User user = await UserTableService.GetUser(_masked.reformatPhoneNumber(phoneNumberEntry));
            if (user != null && _barber != null)
            {
                user.Barber = _barber.Name;
                await UserTableService.UpdateUser(user);
                await Application.Current.MainPage.DisplayAlert("Thanks!", "Your _barber will be with you shortly", "OK");
                await Shell.Current.GoToAsync($"{nameof(CheckinPage)}");
            }
            else 
            {
                await Application.Current.MainPage.DisplayAlert("No _barber selected", "Please select a _barber", "OK");
            }

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
