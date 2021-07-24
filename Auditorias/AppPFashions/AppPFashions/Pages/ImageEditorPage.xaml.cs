using AppPFashions.Interfaces;
using Syncfusion.SfImageEditor.XForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPFashions.Pages
{
    public interface ISave
    {
        string Save(Stream stream,string dfiles);
    }
    public partial class ImageEditorPage : ContentPage
	{
        string desjpg;
        public Stream stream;
        public ImageEditorPage (string defjpg)
		{            
            InitializeComponent ();
            desjpg = defjpg;
            sie_imagen.Source = ImageSource.FromFile("/storage/emulated/0/Pictures/Test/"+ defjpg);                        
        }

        private void sie_imagen_ImageSaving(object sender, ImageSavingEventArgs args)
        {
            args.Cancel = true;
            var location = DependencyService.Get<ISave>().Save(args.Stream, desjpg);
            Task.Delay(500);
            DisplayAlert("Image Saved", location, "OK");
        }
    }

    
}