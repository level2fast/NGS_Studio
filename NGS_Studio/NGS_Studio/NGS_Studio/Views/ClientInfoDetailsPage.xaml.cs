using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NGS_Studio.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClientInfoDetailsPage : ContentPage
	{
		public ClientInfoDetailsPage()
		{
			InitializeComponent();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
           // collectionView.ItemsSource = await App.Database.GetClientsAsync();
        }

    }
}
