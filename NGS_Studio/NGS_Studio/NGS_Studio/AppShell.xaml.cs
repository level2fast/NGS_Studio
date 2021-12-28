using NGS_Studio.ViewModels;
using NGS_Studio.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NGS_Studio
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }
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
            Routes.Add(nameof(OwnerLoginPage), typeof(OwnerLoginPage));
            Routes.Add(nameof(BarberInfoPage), typeof(BarberInfoPage));
            Routes.Add(nameof(BarberInfoAddBarberPage), typeof(BarberInfoAddBarberPage));
            Routes.Add(nameof(BarberInfoDetailsPage), typeof(BarberInfoDetailsPage));
            Routes.Add(nameof(BarberInfoRemoveBarberPage), typeof(BarberInfoRemoveBarberPage));
            Routes.Add(nameof(ClientInfoPage), typeof(ClientInfoPage));
            Routes.Add(nameof(ClientInfoAddClientPage), typeof(ClientInfoAddClientPage));
            Routes.Add(nameof(ClientInfoDetailsPage), typeof(ClientInfoDetailsPage));
            Routes.Add(nameof(LoginPage), typeof(LoginPage));
            // Routes.Add(nameof(ClientInfoRemoveClientPage), typeof(ClientInfoRemoveClientPage));

            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
    }
}
