using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ZXing.Mobile;
using Acr.UserDialogs;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.CurrentActivity;
using SuaveControls.FloatingActionButton.Droid.Renderers;
using Android.Content;
using System.IO;
using Xamarin.Forms;
using AppPFashions.Pages;
using System.Linq;
using Android.Speech;
using Android.Support.V4.Content;
using Android;
using System.Collections.Generic;
using Android.Support.V4.App;
using System.Threading;


[assembly: Dependency(typeof(AppPFashions.Droid.Save))]
namespace AppPFashions.Droid
{
    [Activity(Label = "Perú Fashions", Icon = "@drawable/ic_auditorias_logo", Theme = "@style/MainTheme", MainLauncher = true, ScreenOrientation=ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        List<string> _permission = new List<string>();
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("OTQyQDMxMzYyZTMzMmUzMExKcE4yYUx1QTg5SU1mU0pDTWdIOVJoczA1R0VlMW9aYmtoTDFWbGc5aUE9;OTQzQDMxMzYyZTMzMmUzMEducFkyUE1sUUlRc0tra0dFQVRuSnVKS25udVFrV2xiVG5iQ2F6OTh1T3M9;OTQ0QDMxMzYyZTMzMmUzMEhDZEpvM0xoNUprU3E0ZmlHOWV6SE50WFJhZ2ZPOXIyNWlmQTFXZ0M1emc9;OTQ1QDMxMzYyZTMzMmUzMEFnYU9qbHZMRVozSlZtejVIa0d3d2duTHBTSzMxK1grZXFpKzlsZ2JSNkk9;OTQ2QDMxMzYyZTMzMmUzMGdJZE9GMzk4K1lLZlZzQ2tackR5U3R1VnZadHNpd1N4dXBUM3hJTVJibVk9;OTQ3QDMxMzYyZTMzMmUzMG8yNFdDdHdwZW15cGZHRy9jTGwwbXUremxETmlkTDh4RXVybUQ4aS9sZHM9;OTQ4QDMxMzYyZTMzMmUzMGNDb3NHd09IMnF6YUxWdE1hWUU5Z0drM3N2VnFranQ0VjAyTXVMeVZmMjQ9;OTQ5QDMxMzYyZTMzMmUzMGpsdXhBMlpqM2FiRk5ycHlDQ0xjcTVyOVdVRDBSVHRrZVZ5bjJlQzBRWm89;OTUwQDMxMzYyZTMzMmUzMEV5cjRZMm1JV1BkbnBuajZxd09jTGoxMkFoeWFXYW5WR1pHN3lYRU5hdk09;");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDg0MzdAMzEzNzJlMzIyZTMwb052b1AyaXZiVWJQaHBvcGhOQlMzNDF3N3RUOXFwdElCVVFremhpeHpzMD0=");

            base.OnCreate(bundle);            

            CrossCurrentActivity.Current.Init(this, bundle);            

            RequestPermissionsManually();

            Forms.SetFlags("UseLegacyRenderers");

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Syncfusion.XForms.Android.PopupLayout.SfPopupLayoutRenderer.Init();


            // Inicializamos el scanner
            UserDialogs.Init(this);
            //MobileBarcodeScanner.Initialize(this.Application);

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) == Android.Content.PM.Permission.Granted)
            {
            //    ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.WriteExternalStorage);
            //    //Thread.Sleep(10000);
                string rutaBD = Helpers.FileHelper.ObtenerRutaLocal("DBPFashions.db3");
                LoadApplication(new App(rutaBD, new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid()));

                //LoadApplication(UXDivers.Gorilla.Droid.Player.CreateApplication(
                //                    this,
                //                    new UXDivers.Gorilla.Config("Good Gorilla")
                //                      // Register Grial Shared assembly
                //                      .RegisterAssemblyFromType<UXDivers.Artina.Shared.CircleImage>()
                //                      // Register UXDivers Effects assembly
                //                      .RegisterAssembly(typeof(UXDivers.Effects.Effects).Assembly)
                //                      // FFImageLoading.Transformations
                //                      .RegisterAssemblyFromType<FFImageLoading.Transformations.BlurredTransformation>()
                //                      // FFImageLoading.Forms
                //                      .RegisterAssemblyFromType<FFImageLoading.Forms.CachedImage>()
                //                    ));
            }
            //else
            //{
            //Thread.Sleep(10000);
            //string rutaBD = Helpers.FileHelper.ObtenerRutaLocal("DBPFashions.db3");
            //LoadApplication(new App(rutaBD, new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid()));
            //}


            //LoadApplication(new App());
        }

        const int VOICE = 10;
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == VOICE)
            {
                if (resultCode == Result.Ok)
                {
                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {
                        var textInput = matches[0];
                        if (textInput.Length > 500)
                            textInput = textInput.Substring(0, 500);
                        SpeechToText_Android.SpeechText = textInput;
                    }
                }
                SpeechToText_Android.autoEvent.Set();
            }
        }

        private void RequestPermissionsManually()
        {
            try
            {
                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != Android.Content.PM.Permission.Granted)                                       
                _permission.Add(Manifest.Permission.WriteExternalStorage);

                if (_permission.Count > 0)
                {
                    string[] array = _permission.ToArray();

                    RequestPermissions(array, array.Length);
                }     
            }
            catch (Exception )
            {
                throw;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (requestCode == 1)
            {
                if (grantResults.Length == _permission.Count)
                {
                    for (int i = 0; i < requestCode; i++)
                    {
                        if (grantResults[i] == Android.Content.PM.Permission.Granted)
                        {
                            string rutaBD = Helpers.FileHelper.ObtenerRutaLocal("DBPFashions.db3");
                            LoadApplication(new App(rutaBD, new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid()));
                        }
                        else
                        {
                            _permission = new List<string>();
                            RequestPermissionsManually();
                            break;
                        }
                    }
                }
            }            
        }
    }



    public class Save : ISave
    {
        string ISave.Save(Stream stream,string dfiles)
        {
            System.IO.File.WriteAllBytes(GetLocation(dfiles), ReadFully(stream));
            return GetLocation(dfiles);
        }

        byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        string GetLocation(string dfiles)
        {
            var appdata = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).Path.ToString();
            var directoryname = Path.Combine(appdata, "Test");
            return System.IO.Path.Combine(directoryname, dfiles);
        }
    }

}

