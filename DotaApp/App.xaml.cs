using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DotaApp
{
    public partial class App : Application
    {
        static DotaAppDbController database = null;

        public static DotaAppDbController Database
        {
            get
            {
                
                if (database == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DotaHero.db3");

                    database = new DotaAppDbController(dbPath);
                }
                return database;
            }
                
        }
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
