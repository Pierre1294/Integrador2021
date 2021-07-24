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
	public partial class MenuCosturaPage : ContentPage
	{
		public MenuCosturaPage ()
		{
			InitializeComponent ();
		}

        private void Tlb_viewpdf_Clicked(object sender, EventArgs e)
        {
            //App.Navigator.PushAsync(new ProductionOrderPage());
        }
    }
}