using AppPFashions.Models;
using AppPFashions.Services;
using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace AppPFashions.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QualityPage : ContentPage
	{                
        public QualityPage ()
		{                        
            InitializeComponent ();
            popupLayout.PopupView.AcceptCommand = new AcceptButtonCustomCommand();

            var user = App.baseDatos.GetUsuario();            
            VariableGlobal.cusuar = user.cusuar;

            //popupLayout.PopupView.AcceptCommand = new Command(() =>
            //{                
            //    paudob00 observ = new paudob00
            //    {
            //        fregis = DateTime.Now,
            //        clinea = VariableGlobal.cblobs,
            //        dobser = VariableGlobal.dobser,
            //        cusuar = VariableGlobal.cusuar,
            //    };
            //    dataService.InsertObservacionAuditoria(observ);
            //});
        }

        private void Tlb_viewpdf_Clicked(object sender, EventArgs e)
        {
            //App.Navigator.PushAsync(new ProductionOrderPage());
        }

        private void Tlb_obsauditoria_Clicked(object sender, EventArgs e)
        {
            //popupLayout.Show();
        }

        private void Ety_cbloqu_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            VariableGlobal.cblobs = entry.Text;            
        }

        private void Ety_dobser_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            VariableGlobal.dobser = entry.Text;
        }
    }

    public class AcceptButtonCustomCommand : ICommand
    {        
        private DialogService dialogService;
        private DataService dataService;

        public event EventHandler CanExecuteChanged;

        public AcceptButtonCustomCommand()
        {
            dialogService = new DialogService();
            dataService = new DataService();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            paudob00 observ = new paudob00
            {
                fregis = DateTime.Now,
                clinea = VariableGlobal.cblobs,
                dobser = VariableGlobal.dobser,
                cusuar = VariableGlobal.cusuar,
                senvio = "N",
            };
            dataService.InsertObservacionAuditoria(observ);
            await dialogService.ShowMessage("Aviso", "La observación se guardo de manera satisfactoria.");
        }
    }

}