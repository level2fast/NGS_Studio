using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using NGS_Studio.Data;
using NGS_Studio.Services;
using NGS_Studio.Models;
using System.Timers;

namespace NGS_Studio.ViewModels
{
    [QueryProperty(nameof(EmailPromotions), nameof(EmailPromotions))]
    [QueryProperty(nameof(EmailClientList), nameof(EmailClientList))]
    public class NewEmailViewModel : BaseViewModel
    {
        private string _text;
        private string _description;
        private bool _emailC = false;
        private bool _emailS = false;
        private bool _progressBarContentVisible = false;
        private bool _emailContentVisible = false;
        private List<string> _emailAddress = new List<string>();
        System.Timers.Timer aTimer;
        public static List<User> users;
        private float _progressAmount;

        public NewEmailViewModel()
        {
            
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            aTimer= new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(makeProgressEvent);
            aTimer.Interval = 100; // milliseconds 1000 = 1 sec

        }


        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_text)
                && !String.IsNullOrWhiteSpace(_description);
        }

        public string Subject
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public string Body
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public bool EmailPromotions
        {
            get
            {
                return _emailC;
            }
            set
            {
                if (value)
                {
                    Subject = "Promotions";
                }
                _emailC = value;
            }
        }

        public bool EmailClientList
        {
            get
            {
                return _emailS;
            }
            set
            {
                if (value)
                {
                    Subject = "Client List";
                }
                _emailS = value;
            }
        }

        public bool EmailContentVisibility
        {
            get => _emailContentVisible;
            set => SetProperty(ref _emailContentVisible, value);
        }

        public bool ProgressBarVisibilty
        {
            get => _progressBarContentVisible;
            set => SetProperty(ref _progressBarContentVisible, value);
        }

        
        public float ProgressAmount
        {
            get => _progressAmount;
            set => SetProperty(ref _progressAmount, value);
        }

        public Command SaveCommand { get; }


        public Command CancelCommand { get; }


        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }


        private async void OnSave()
        {
            EmailSender email = new EmailSender();
            if (EmailClientList)
            {
                List<string> ownerEmailAddress = new List<string>();
                var owner= await UserTableService.GetOwner();
                ownerEmailAddress.Add(owner.Email);
                await email.SendEmail(Subject, Body,ownerEmailAddress);
            }
            else if (EmailPromotions)
            {
                await email.SendEmail(Subject, Body, _emailAddress);
            }
            await Shell.Current.GoToAsync("..");
        }
        public async Task OnAppearing()
        {
            // show progress bar on screen
            ProgressBarVisibilty = true;
            EmailContentVisibility = false;
            aTimer.Enabled = true;
            // load clients
            users = await UserTableService.GetAllClients();
            if (users == null)
            {
                Console.WriteLine("user is null");
                await App.Current.MainPage.DisplayAlert(Constants.ERROR, "could not get clients from database", "OK");
                return;
            }

            // initialize email content
            foreach (User usr in users)
            {
                _emailAddress.Add(usr.Email);
            }

            if (EmailClientList)
            {
                foreach (User usr in users)
                {
                    Body += usr.Email + "\n";
                }
            }


        }
        private void makeProgressEvent(object source, ElapsedEventArgs e)
        {
            ProgressAmount += .2f;
            if (ProgressAmount >= 1)
            {
                aTimer.Enabled = false;
                // make email controls visible
                ProgressBarVisibilty = false;
                EmailContentVisibility = true;
            }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class EmailSender
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="recipients"></param>
        /// <returns></returns>
        public async Task SendEmail(string subject, string body, List<string> recipients)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
                await App.Current.MainPage.DisplayAlert(Constants.ERROR, fbsEx.ToString(), "OK");
            }
            catch (Exception ex)
            {
                // Some other exception occurred
                Console.WriteLine("{0} Exception caught.", ex);
            }
        }
    }
}
