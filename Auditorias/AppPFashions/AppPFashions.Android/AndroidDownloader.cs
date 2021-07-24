using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidHUD;
using AppPFashions.Droid;
using AppPFashions.Interfaces;
using Xamarin.Forms;    

[assembly: Dependency(typeof(AndroidDownloader))]
namespace AppPFashions.Droid
{
    public class AndroidDownloader : IDownloader
    {
        public event EventHandler<DownloadEventArgs> OnFileDownloaded;

        public void DownloadFile(string url, string folder)
        {
            string pathToNewFolder = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, folder);
            Directory.CreateDirectory(pathToNewFolder);

            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                string pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName(url));
                webClient.DownloadFileAsync(new Uri(url), pathToNewFile);
            }
            catch (Exception ex)
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));
            }
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));
            }
            else
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(true));
            }
        }

        public void InstallAPK()
        {
            Intent promptInstall = new Intent(Intent.ActionView).SetDataAndType(Android.Net.Uri.FromFile(new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory + "/download/Auditoria.apk")), "application/vnd.android.package-archive");
            promptInstall.AddFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(promptInstall);

            //Intent promptInstall = new Intent(Intent.ActionView).SetDataAndType(Android.Net.Uri.FromFile(new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory.Path + "/download/Auditoria.apk")), "application/vnd.android.package-archive");
            //promptInstall.AddFlags(ActivityFlags.NewTask);
            //Android.App.Application.Context.StartActivity(promptInstall);
            //Intent i = new Intent();
            //i.SetAction(Intent.ActionView);
            //i.SetDataAndType(Android.Net.Uri.FromFile(new Java.IO.File("/download/Auditoria.apk")), "application/vnd.android.package-archive");
            ////Log.d("Lofting", "About to install new .apk");
            //Android.App.Application.Context.StartActivity(i);
        }

        public void Show(string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                AndHUD.Shared.Show(Forms.Context, status:message ,maskType: MaskType.Black);
            });
        }
        
        public void Hide()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                AndHUD.Shared.Dismiss(Forms.Context);
            });
        }

    }
}