using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NGS_Studio.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClientCheckinPage : ContentPage
	{
		public ClientCheckinPage()
		{
			InitializeComponent();
		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			barberPicker.ItemsSource   = await App.Database.GetBarbersAsync();
		}

	}
}
