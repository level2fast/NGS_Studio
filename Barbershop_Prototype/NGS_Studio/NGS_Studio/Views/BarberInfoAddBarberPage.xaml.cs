using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace NGS_Studio.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BarberInfoAddBarberPage : ContentPage
	{
        public BarberInfoAddBarberPage()
		{
			InitializeComponent();
		}
        protected override async void OnAppearing()
        {
            base.OnAppearing();
           // collectionView.ItemsSource = await App.Database.GetBarbersAsync();
        }

    }
}
