using Xamarin.Forms;
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
        }
    }
}
