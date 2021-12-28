using Xamarin.Forms;

namespace NGS_Studio.Views
{
    public partial class PhoneNumberCheckinPage : ContentPage
    {
        public PhoneNumberCheckinPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
           BarberPicker.ItemsSource = await App.Database.GetBarbersAsync();
        }
    }
}