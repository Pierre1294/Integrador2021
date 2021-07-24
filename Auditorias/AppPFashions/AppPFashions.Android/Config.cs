using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using AppPFashions.Interfaces;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppPFashions.Droid.Config))]

namespace AppPFashions.Droid
{
    public class Config : IConfig
    {        
        private string directoryDB;
        private ISQLitePlatform platform;

        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(directoryDB))
                {
                    //directoryDB = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                    directoryDB = "/storage/emulated/0/DB";
                }

                return directoryDB;
            }
        }

        public ISQLitePlatform Platform
        {
            get
            {
                if (platform == null)
                {
                    platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }

                return platform;

            }
        }

    }
}