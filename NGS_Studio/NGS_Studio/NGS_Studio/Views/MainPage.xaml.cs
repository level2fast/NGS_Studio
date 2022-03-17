using NGS_Studio.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NGS_Studio.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		readonly MainViewModel _viewModel;
		public MainPage()
		{
			InitializeComponent();
			BindingContext = _viewModel = new MainViewModel();
			//BindingContext = new MainViewModel();
			_viewModel.OnAppearing(); 

		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
			
            //_viewModel.OnAppearing();
        }
    }
}

