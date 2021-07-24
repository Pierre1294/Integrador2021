using AppPFashions.Data;
using AppPFashions.Models;
using AppPFashions.Pages;
using AppPFashions.Services;
using SQLite.Net.Interop;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppPFashions
{
    public partial class App : Application
    {
        public static BaseDatos baseDatos { get; set; }
        #region Attributes
        private DataService dataService;
        #endregion

        #region Properties
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }
        public static MasterUserPage UserMaster { get; internal set; }        
        public static Usuario CurrentUser { get; internal set; }

        #endregion

        #region Constructors
        public App(string rutaBD, ISQLitePlatform plataforma)
        {
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("OTQyQDMxMzYyZTMzMmUzMExKcE4yYUx1QTg5SU1mU0pDTWdIOVJoczA1R0VlMW9aYmtoTDFWbGc5aUE9;OTQzQDMxMzYyZTMzMmUzMEducFkyUE1sUUlRc0tra0dFQVRuSnVKS25udVFrV2xiVG5iQ2F6OTh1T3M9;OTQ0QDMxMzYyZTMzMmUzMEhDZEpvM0xoNUprU3E0ZmlHOWV6SE50WFJhZ2ZPOXIyNWlmQTFXZ0M1emc9;OTQ1QDMxMzYyZTMzMmUzMEFnYU9qbHZMRVozSlZtejVIa0d3d2duTHBTSzMxK1grZXFpKzlsZ2JSNkk9;OTQ2QDMxMzYyZTMzMmUzMGdJZE9GMzk4K1lLZlZzQ2tackR5U3R1VnZadHNpd1N4dXBUM3hJTVJibVk9;OTQ3QDMxMzYyZTMzMmUzMG8yNFdDdHdwZW15cGZHRy9jTGwwbXUremxETmlkTDh4RXVybUQ4aS9sZHM9;OTQ4QDMxMzYyZTMzMmUzMGNDb3NHd09IMnF6YUxWdE1hWUU5Z0drM3N2VnFranQ0VjAyTXVMeVZmMjQ9;OTQ5QDMxMzYyZTMzMmUzMGpsdXhBMlpqM2FiRk5ycHlDQ0xjcTVyOVdVRDBSVHRrZVZ5bjJlQzBRWm89;OTUwQDMxMzYyZTMzMmUzMEV5cjRZMm1JV1BkbnBuajZxd09jTGoxMkFoeWFXYW5WR1pHN3lYRU5hdk09;");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDg0MzdAMzEzNzJlMzIyZTMwb052b1AyaXZiVWJQaHBvcGhOQlMzNDF3N3RUOXFwdElCVVFremhpeHpzMD0=");

            baseDatos = new BaseDatos(plataforma, rutaBD);
            baseDatos.Conectar();

            InitializeComponent();
            dataService = new DataService();
            var user = dataService.GetUser();
            if (user != null && user.IsRemembered)
            {
                App.CurrentUser = user;
                MainPage = new MasterUserPage();
            }
            else
            {
                MainPage = new LoginPage();
            }
        }
        #endregion

        #region Methods
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
        
        #endregion
    }
}
