using NGS_Studio.Services;
using System;
using System.IO;
using Xamarin.Forms;

namespace NGS_Studio
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }
    }
}
