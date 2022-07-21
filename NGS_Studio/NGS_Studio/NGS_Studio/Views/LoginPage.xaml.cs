using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NGS_Studio.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();

		}

        protected override void OnAppearing()
        {
			passwordEntry.Text = string.Empty;
			usernameEntry.Text = string.Empty;
		}
	}
}
