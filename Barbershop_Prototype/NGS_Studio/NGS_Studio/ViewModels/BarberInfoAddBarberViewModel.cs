﻿using NGS_Studio.Views;
using NGS_Studio.Models;
using Xamarin.Forms;
using System.Collections.Generic;

namespace NGS_Studio.ViewModels
{
    public class BarberInfoAddBarberViewModel : BaseViewModel
    {

        private string nameEntry;
        private string emailEntry;
        private string phoneNumberEntry;

        //The commanding interface provides an alternative approach 
        //to implementing commands that is much better suited to
        //the MVVM architecture.The ViewModel itself can contain
        //commands, which are methods that are executed in reaction
        //to a specific activity in the View such as a Button click
        //.Data bindings are defined between these commands and the Button.
        public Command ClientSubmitCommand { get; }

        public BarberInfoAddBarberViewModel()
        {
            Title = "Barber Info";
            ClientSubmitCommand = new Command(OnClientSumbitClicked);

        }

        public string NameEntry
        {
            get => nameEntry;
            set => SetProperty(ref nameEntry, value);
        }
        public string EmailEntry
        {
            get => emailEntry;
            set => SetProperty(ref emailEntry, value);
        }
        public string PhoneNumberEntry
        {
            get => phoneNumberEntry;
            set => SetProperty(ref phoneNumberEntry, value);
        }

        async void OnClientSumbitClicked(object sender)
        {
            MaskedBehavior masked = new MaskedBehavior();
            if (!string.IsNullOrWhiteSpace(NameEntry) && !string.IsNullOrWhiteSpace(EmailEntry) &&
                !string.IsNullOrWhiteSpace(PhoneNumberEntry))
            {
                await App.Database.SaveUserAsync(new User
                {
                    Name = NameEntry,
                    Email = EmailEntry,
                    PhoneNumber = masked.reformatPhoneNumber(PhoneNumberEntry),
                    IsBarber = true
                });

                 await Application.Current.MainPage.DisplayAlert("Complete", NameEntry + " has been added to NGS", "OK");
                NameEntry = EmailEntry = PhoneNumberEntry = string.Empty;

            }
            else 
            {
                await Application.Current.MainPage.DisplayAlert("Cannot add to database", "Information missing", "OK");
            }
        }
    }
}

