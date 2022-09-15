using NGS_Studio.Services;
using Xamarin.Forms;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using System;
using static NGS_Studio.Configuration.Configuration;
using NGS_Studio.Views;
using NGS_Studio.Configuration;
using NGS_Studio.Data;

[assembly: ExportFont("Montserrat-Bold.ttf",Alias="Montserrat-Bold")]
     [assembly: ExportFont("Montserrat-Medium.ttf", Alias = "Montserrat-Medium")]
     [assembly: ExportFont("Montserrat-Regular.ttf", Alias = "Montserrat-Regular")]
     [assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "Montserrat-SemiBold")]
     [assembly: ExportFont("UIFontIcons.ttf", Alias = "FontIcons")]
namespace NGS_Studio
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            _ = Globals.Instance.Firebase;
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.
                RegisterLicense("NzA2ODk2QDMyMzAyZTMyMmUzMEtDQ3grSnBWNlBoRHdjSjFlNzNIaTdmZEQyZDRHdUd5RzgwZlQ2YUxZaVk9");
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
