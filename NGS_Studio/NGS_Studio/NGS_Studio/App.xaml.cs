using NGS_Studio.Services;
using Xamarin.Forms;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using System;
using static NGS_Studio.Configuration.Configuration;
using NGS_Studio.Views;
using NGS_Studio.Configuration;

namespace NGS_Studio
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
            LoadConfigData();
        }
        private void LoadConfigData()
        {
            ApiTokens.TWILIO_ACCOUNT_SID = AppSettingsManager.Settings["TWILIO_ACCOUNT_SID"];
            ApiTokens.TWILIO_AUTH_TOKEN = AppSettingsManager.Settings["TWILIO_AUTH_TOKEN"];
        }
    }
}
