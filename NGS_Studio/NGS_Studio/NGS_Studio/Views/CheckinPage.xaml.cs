using System;
using Xamarin.Forms;

namespace NGS_Studio.Views
{
    public partial class CheckinPage : ContentPage
    {
        public CheckinPage()
        {
            InitializeComponent();
        }
        private void onCheckinBtn_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsVisible = false;
            YesCheckinBtn.IsVisible = true;
            NoCheckinBtn.IsVisible = true;
            CheckinLabel.IsVisible = true;
            
        }
    }
}