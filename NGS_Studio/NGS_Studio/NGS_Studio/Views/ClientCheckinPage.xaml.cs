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
			//ToDo SDD: Update to populate picker with all barbers in the shop
			//barberPicker.ItemsSource   = await App.Database.GetBarbersAsync();
		}

	}
}
