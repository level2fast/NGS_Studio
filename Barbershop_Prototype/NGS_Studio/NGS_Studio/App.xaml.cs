using NGS_Studio.Services;
using System;
using System.IO;
using Xamarin.Forms;
using LocalDatabase;

namespace NGS_Studio
{
    public partial class App : Application
    {
        static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "people.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        //protected async override void OnStart()
        //{
        //    await Shell.Current.GoToAsync("//LoginPage");
        //}

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
