using NGS_Studio.Models;
using NGS_Studio.ViewModels;
using Xamarin.Forms;

namespace NGS_Studio.Views
{
    public partial class NewEmailPage : ContentPage
    {
        readonly NewEmailViewModel _viewModel;
        public NewEmailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new NewEmailViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.OnAppearing();
        }
    }
}