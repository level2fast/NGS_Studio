using NGS_Studio.Models;
using NGS_Studio.ViewModels;
using Xamarin.Forms;

namespace NGS_Studio.Views
{
    public partial class NewEmailPage : ContentPage
    {
        public Item Item { get; set; }

        public NewEmailPage()
        {
            InitializeComponent();
            BindingContext = new NewEmailViewModel();
        }
    }
}