using NGS_Studio.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NGS_Studio.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
            this.BindingContext = new ForgotPasswordViewModel();
        }
    }
}