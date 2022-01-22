using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using NGS_Studio.Data;
using NGS_Studio.Services;
using NGS_Studio.Models;

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

        /// <summary>
        /// 
        /// </summary>
        public NewEmailViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
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
                emailS = value;
            }
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
            List<string> emailAddress = new List<string>();
 
            if (EmailClientList)
            {
                User recepients = await UserTableService.GetOwner();
                emailAddress.Add(recepients.Email);
                await email.SendEmail(Subject, Body, emailAddress);
                await App.Current.MainPage.DisplayAlert("Email Sent","","OK");
            }
            else if (EmailPromotions)
            {
                List<User> user = await UserTableService.GetAllClients();
                foreach (User usr in user)
                {
                    emailAddress.Add(usr.Email);
                }
                await email.SendEmail(Subject, Body, emailAddress);
                await App.Current.MainPage.DisplayAlert("Email Sent", "", "OK");
            }
            await Shell.Current.GoToAsync("..");
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
