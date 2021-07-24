using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPFashions.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuUserPage : ContentPage
	{
        public MenuUserPage ()
		{           
            InitializeComponent ();

            var user = App.baseDatos.GetUsuario();
            string xctraba = "";
            if (Int32.Parse(user.ctraba.Substring(1, 5)) > 10000)
            {
                xctraba = user.ctraba;
            }
            else
            {
                xctraba = user.ctraba.Substring(0, 1) + user.ctraba.Substring(2, 4);
            }
            userimg.Source = "/storage/emulated/0/Fotos/" + xctraba + ".bmp";
            lbluser.Text = user.ctraba + " - " + user.dusuar;
        }
	}
}