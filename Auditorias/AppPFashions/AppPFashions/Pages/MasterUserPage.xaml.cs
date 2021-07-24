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
    public partial class MasterUserPage : MasterDetailPage
    {
        public MasterUserPage()
        {
            InitializeComponent();        
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.UserMaster = this;
            App.Navigator = Navigator;
        }

    }
}