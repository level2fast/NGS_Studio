using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using NGS_Studio.Data;
using NGS_Studio.Services;
using NGS_Studio.Models;
using System.Timers;
using System.Threading;

namespace NGS_Studio.ViewModels
{
    [QueryProperty(nameof(EmailPromotions), nameof(EmailPromotions))]
    [QueryProperty(nameof(EmailClientList), nameof(EmailClientList))]
    public class NewEmailViewModel : BaseViewModel
    {
        private string text;
        private string description;
        private bool emailC = false;
        private bool emailS = false;
        private bool progressBarContentVisible = false;
        private bool emailContentVisible = false;
        private List<string> emailAddress = new List<string>();
        System.Timers.Timer aTimer;
        public static List<User> users;
        private float progressAmount;

        /// <summary>
        /// 
        /// </summary>
        public NewEmailViewModel()
        {
            
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            getClientList();
            aTimer= new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(makeProgressEvent);
            aTimer.Interval = 100; // milliseconds 1000 = 1 sec

        }

        private void getClientList()
        {
            // display controls for sending email

            if (users == null)
                return;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }

        /// <summary>
        /// 
        /// </summary>
        public string Subject
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public string Body
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool EmailPromotions
        {
            get
            {
                return emailC;
            }
            set
            {
                if (value)
                {
                    Subject = "Promotions";
                }
                emailC = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool EmailClientList
        {
            get
            {
                return emailS;
            }
            set
            {
                if (value)
                {
                    Subject = "Client List";
                }
                emailS = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool EmailContentVisibility
        {
            get => emailContentVisible;
            set => SetProperty(ref emailContentVisible, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ProgressBarVisibilty
        {
            get => progressBarContentVisible;
            set => SetProperty(ref progressBarContentVisible, value);
        }

        
        /// <summary>
        /// 
        /// </summary>
        public float ProgressAmount
        {
            get => progressAmount;
            set => SetProperty(ref progressAmount, value);
        }
        /// <summary>
        /// 
        /// </summary>
        public Command SaveCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        public Command CancelCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        /// <summary>
        /// 
        /// </summary>
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
                await email.SendEmail(Subject, Body, emailAddress);
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
                emailAddress.Add(usr.Email);
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
                await App.Current.MainPage.DisplayAlert(Constants.ERROR, fbsEx.ToString(), "OK");
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred
                Console.WriteLine("{0} Exception caught.", ex);
            }
        }
    }
}
