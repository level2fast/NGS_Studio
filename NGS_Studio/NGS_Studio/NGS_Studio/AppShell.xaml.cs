using NGS_Studio.Services;
using NGS_Studio.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NGS_Studio
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();

        private bool _isUserAuth;
        public bool IsUserAuthenticated
        {
            get => _isUserAuth;
            set => SetProperty(ref _isUserAuth, value);
        }
        private bool _showOwnerFlyoutItem;
        public bool ShowOwnerFlyoutItem
        {
            get => _showOwnerFlyoutItem;
            set => SetProperty(ref _showOwnerFlyoutItem, value);
        }
        string _iconCheckin = "icon_checkin";
        public string IconCheckin
        {
            get { return _iconCheckin; }
            set { SetProperty(ref _iconCheckin, value); }
        }
        string _iconOwner = "icon_owner";
        public string IconOwner
        {
            get { return _iconOwner; }
            set { SetProperty(ref _iconOwner, value); }
        }
        string _iconLogin = "icon_login";
        public string IconLogin
        {
            get { return _iconLogin; }
            set { SetProperty(ref _iconLogin, value); }
        }
        string _iconAbout = "icon_about";
        public string IconAbout
        {
            get { return _iconAbout; }
            set { SetProperty(ref _iconAbout, value); }
        }
        string _iconLogout = "icon_logout";
        public string IconLogout
        {
            get { return _iconLogout; }
            set { SetProperty(ref _iconLogout, value); }
        }

        private string _appVersion;
        public string AppVersion
        {
            get { return _appVersion; }
            set { SetProperty(ref _appVersion, value); }
        }
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;
            ShowOwnerFlyoutItem = false;
            MessagingCenter.Subscribe<object, (bool,string)>(this, "ChangeCheckinSection", SetFlyoutItemVisibility);
            AppVersion = "App Version: v" + VersionTracking.CurrentVersion;
        }

        private void SetFlyoutItemVisibility(object arg1, (bool, string) arg2)
        {
            var visibility = arg2.Item1;
            var shellContentName = arg2.Item2;
            ShellContent con = (ShellContent)this.FindByName(shellContentName);
            con.IsVisible = visibility;

        }


        /// <summary>
        /// RegisterRoutes adds all routes to a contentPage dictionary 
        /// </summary>
        void RegisterRoutes()
        {
            Routes.Add(nameof(AboutPage), typeof(AboutPage));
            Routes.Add(nameof(CheckinPage), typeof(CheckinPage));
            Routes.Add(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routes.Add(nameof(ItemsPage), typeof(ItemsPage));
            Routes.Add(nameof(NewItemPage), typeof(NewItemPage)); 
            Routes.Add(nameof(PhoneNumberCheckinPage), typeof(PhoneNumberCheckinPage));
            Routes.Add(nameof(ClientCheckinPage), typeof(ClientCheckinPage));
            Routes.Add(nameof(OwnerDetailsPage), typeof(OwnerDetailsPage));
            Routes.Add(nameof(BarberInfoPage), typeof(BarberInfoPage));
            Routes.Add(nameof(BarberInfoAddBarberPage), typeof(BarberInfoAddBarberPage));
            Routes.Add(nameof(BarberInfoDetailsPage), typeof(BarberInfoDetailsPage));
            Routes.Add(nameof(BarberInfoRemoveBarberPage), typeof(BarberInfoRemoveBarberPage));
            Routes.Add(nameof(ClientInfoPage), typeof(ClientInfoPage));
            Routes.Add(nameof(ClientInfoAddClientPage), typeof(ClientInfoAddClientPage));
            Routes.Add(nameof(ClientInfoDetailsPage), typeof(ClientInfoDetailsPage));
            Routes.Add(nameof(LoginPage), typeof(LoginPage));
            Routes.Add(nameof(LoginPage)+"/"+nameof(LogoutPage), typeof(LogoutPage));
            Routes.Add(nameof(BarberInfoEditBarberPage), typeof(BarberInfoEditBarberPage));
            Routes.Add(nameof(ClientInfoRemoveClientPage), typeof(ClientInfoRemoveClientPage));
            Routes.Add(nameof(OwnerInfoPage), typeof(OwnerInfoPage));
            Routes.Add(nameof(NewEmailPage), typeof(NewEmailPage));
            // Routes.Add(nameof(SignUpPage), typeof(SignUpPage));
            Routes.Add(nameof(ForgotPasswordPage), typeof(ForgotPasswordPage));
            Routes.Add(nameof(SimpleForgotPasswordPage), typeof(SimpleForgotPasswordPage));

            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        private void ShellContent_Disappearing(object sender, EventArgs e)
        {
            if(DependencyService.Resolve<IFireBaseAuthentication>().IsSignIn())
            {
                IsUserAuthenticated = true;
                ShowOwnerFlyoutItem = true;
            }
        }
        private void ShellContent_Appearing(object sender, EventArgs e)
        {
            if (DependencyService.Resolve<IFireBaseAuthentication>().IsSignIn())
            {
                IsUserAuthenticated = true;
                ShowOwnerFlyoutItem = true;
            }
        }
        public void SetOwnerFlyoutItemVisibility()
        {
            ShowOwnerFlyoutItem = false;
            IsUserAuthenticated = false;
        }

        #region OnPropertyChangedEV

        protected bool SetProperty<T>(ref T backingStore, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = "", System.Action onChanged = null)
        {
            if (System.Collections.Generic.EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
        
    }
}
