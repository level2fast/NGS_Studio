using NGS_Studio.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace NGS_Studio.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}