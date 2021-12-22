using NGS_Studio.Models;
using NGS_Studio.ViewModels;
using Xamarin.Forms;

namespace NGS_Studio.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}