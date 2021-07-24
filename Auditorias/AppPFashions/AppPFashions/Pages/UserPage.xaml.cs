using AppPFashions.Data;
using AppPFashions.Interfaces;
using AppPFashions.Models;
using AppPFashions.Services;
using AppPFashions.Templates;
using Syncfusion.SfKanban.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPFashions.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : INotifyPropertyChanged
    {
        IDownloader downloader = DependencyService.Get<IDownloader>();
        List<taudit00> auditcp;
        List<taudit00> audites;
        List<taudit00> audittr;
        List<taudit00> auditcf;
        taudit00 xoperac;
        mtraba00 ldtraba;
        string xccargo,xcusuar;
        public string dusuar { get; set; }
        public string taudit { get; set; }

        private ApiService apiService;

        public UserPage()
        {
            apiService = new ApiService();
            
            InitializeComponent();
            //downloader.OnFileDownloaded += OnFileDownloaded;
            //CargaCards();
            BindingContext = this;

                        
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            var user = App.baseDatos.GetUsuario();
            xccargo = user.ccargo;
            xcusuar = user.cusuar;
            
            CargaCards();            
        }

        async void FechaApk()
        {
            string response = await apiService.GetFechaApk();            
            DateTime dtapkserver = DateTime.Parse(response.Trim(new char[] {'"'}));
            //FileInfo fi = new FileInfo("/storage/emulated/0/Download/Auditoria.apk");
            DateTime dtapkmobile = File.GetLastWriteTime("/storage/emulated/0/Download/Auditoria.apk");
            
            if (dtapkserver > DateTime.Parse(dtapkmobile.ToString("dd/MM/yyyy HH:mm:ss")))
            {
                if (await DisplayAlert("Aviso", "Existe una actualización para la aplicación, desea descargarla ahora?", "Si", "No"))
                {
                    DependencyService.Get<IDownloader>().Show("Descargando");
                    string rutapdf = "ftp://192.168.2.55/Auditoria.apk";
                    downloader.DownloadFile(rutapdf, "Download");
                }
            }
        }

        private void OnFileDownloaded(object sender, DownloadEventArgs e)
        {
            if (e.FileSaved)
            {
                DependencyService.Get<IDownloader>().Hide();
            }
            else
            {
                DisplayAlert("Aviso", "Hubo un error al descargar el archivo", "OK");
            }
        }

        async void CargaCards()
        {

            try
            {

                    if (xccargo=="34" /*|| xccargo == "06"*/ || xccargo == "53")
                    { 
                        auditcp = App.baseDatos.GetList<taudit00>(false).Where(x => x.careas == "19" && x.status=="D" && x.sreaud=="N").OrderByDescending(x=>x.faudit).ToList();
                        int qauditcp= App.baseDatos.GetList<taudit00>(false).Where(x => x.careas == "19" && x.status == "D" && x.sreaud == "N").Count();
                        auditcf = App.baseDatos.GetList<taudit00>(false).Where(x => x.careas == "FC" && x.status == "D" && x.sreaud == "N").OrderByDescending(x => x.faudit).ToList();
                        int qauditcf = App.baseDatos.GetList<taudit00>(false).Where(x => x.careas == "FC" && x.status == "D" && x.sreaud == "N").OrderByDescending(x => x.faudit).Count();
                        //listView_cproceso.ItemsSource = auditcp;
                        listView_cfinal.ItemsSource = auditcf;
                        //txtaudi1.Text = "Costura Proceso ("+ qauditcp +")";                        
                        txtaudi2.Text = "Costura Final ("+ qauditcf + ")";
                        auditoria3.IsVisible = false;
                    }

                    if (xccargo == "05")
                    {
                        auditcp = App.baseDatos.GetList<taudit00>(false).Where(x => x.careas == "16" && (x.status == "D" || x.status == "P") && x.sreaud == "N").OrderByDescending(x => x.faudit).ToList();
                        int qauditcp = App.baseDatos.GetList<taudit00>(false).Where(x => x.careas == "16" && (x.status == "D" || x.status == "P") && x.sreaud == "N").Count();
                        //listView_cproceso.ItemsSource = auditcp;
                        auditoria2.IsVisible = false;
                        auditoria3.IsVisible = false;
                        //txtaudi1.Text = "Corte ("+ qauditcp +")";  
       
                }

                if (xccargo == "57" || xccargo == "56" || xccargo == "24")
                {
                    auditcp = App.baseDatos.GetList<taudit00>(false).Where(x => x.careas == "29" && x.status == "D" && x.sreaud == "N").OrderBy(x => x.faudit).ToList();
                    int qauditcp = App.baseDatos.GetList<taudit00>(false).Where(x => x.careas == "29" && x.status == "D" && x.sreaud == "N").Count();
                    //listView_cproceso.ItemsSource = auditcp;                    
                    //txtaudi1.Text = "Bordado (" + qauditcp + ")";


                    audites = App.baseDatos.GetList<taudit00>(false).Where(x => x.careas == "33" && x.status == "D" && x.sreaud == "N").OrderBy(x => x.faudit).ToList();
                    int qaudites = App.baseDatos.GetList<taudit00>(false).Where(x => x.careas == "33" && x.status == "D" && x.sreaud == "N").Count();
                    listView_cfinal.ItemsSource = audites;                    
                    txtaudi2.Text = "Estampado (" + qaudites + ")";
  

                    audittr = App.baseDatos.GetList<taudit00>(false).Where(x => x.careas == "31" && x.status == "D" && x.sreaud == "N").OrderBy(x => x.faudit).ToList();
                    int qaudittr = App.baseDatos.GetList<taudit00>(false).Where(x => x.careas == "31" && x.status == "D" && x.sreaud == "N").Count();
                    listView_transfer.ItemsSource = audittr;
                    txtaudi3.Text = "Transfer (" + qaudittr + ")";


                }

                if (xccargo == "06")
                {
                    auditcp = App.baseDatos.GetList<taudit00>(false).Where(x => x.careas == "17" && (x.status == "D" || x.status == "P") && x.sreaud == "N").OrderBy(x => x.faudit).ToList();
                    int qauditcp = App.baseDatos.GetList<taudit00>(false).Where(x => x.careas == "17" && (x.status == "D" || x.status == "P") && x.sreaud == "N").Count();
                    //listView_cproceso.ItemsSource = auditcp;
                    auditoria2.IsVisible = false;
                    auditoria3.IsVisible = false;
                    //txtaudi1.Text = "Acabado Proceso (" + qauditcp + ")";
                }

                //kanban.ItemsSource = Cards;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", ex.StackTrace, "OK");
            }

        }


        public void User()
        {
            //using (var data = new DataAccess())
            //{
                var user = App.baseDatos.GetUsuario();
                VariableGlobal.ctraba = user.ctraba;
                VariableGlobal.dtraba = user.dusuar;
            //}
            dusuar = VariableGlobal.ctraba +" - "+  VariableGlobal.dtraba;
        }


        private void Tlb_auditdefectos_Clicked(object sender, EventArgs e)
        {
            //App.Navigator.PushAsync(new AuditoriaDefectos());
        }

        private async void ListView_cproceso_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            string newclinea;
            try
            {
                if (await DisplayAlert("Aviso", "Desea realizar la reauditoria", "Si", "No"))
                {
                    var selkanban = (e.ItemData) as taudit00;
                    //using (var data = new DataAccess())
                    //{
                    if (xccargo == "56" || xccargo == "57") { newclinea = ""; }
                    else { newclinea = selkanban.clinea; }

                    xoperac = App.baseDatos.GetList<taudit00>(false).Where(x => x.nsecue == selkanban.nsecue && x.idaudi == selkanban.idaudi && x.clinea == newclinea && (x.status == "D" || x.status == "P") && x.careas == selkanban.careas && x.faudit.ToString("dd-MM-yyyy") == selkanban.faudit.ToString("dd-MM-yyyy")).FirstOrDefault();
                    //}

                    //using (var data = new DataAccess())
                    //{
                    var audenvio = App.baseDatos.GetList<taudit00>(false).Where(x => x.careas == selkanban.careas && x.clinea == newclinea && x.nsecue == selkanban.nsecue && x.idaudi==selkanban.idaudi && x.faudit.ToString("dd-MM-yyyy") == selkanban.faudit.ToString("dd-MM-yyyy") && x.sreaud == "S").ToList();
                    if (audenvio.Count > 0)
                    {
                        await DisplayAlert("Aviso", "Ya se realizo la reauditoria", "OK");
                        return;
                    }
                    //}

                    var dataok = new List<taudit00>
                    {
                        new taudit00
                        {
                        idaudi = xoperac.idaudi,
                        careas = xoperac.careas.ToString(),
                        faudit = DateTime.Parse(xoperac.faudit.ToString()),
                        nsecue = Int32.Parse(xoperac.nsecue.ToString()),
                        clinea = xoperac.clinea.ToString(),
                        nordpr = xoperac.nordpr.ToString(),
                        ccarub = xoperac.ccarub.ToString(),
                        dcarub = xoperac.dcarub.ToString(),
                        ctraba = xoperac.ctraba.ToString(),
                        copera = xoperac.copera.ToString(),
                        dopera = xoperac.dopera.ToString(),
                        dclien = xoperac.dclien.ToString(),
                        nlotes = Int32.Parse(xoperac.nlotes.ToString()),
                        nmuest = Int32.Parse(xoperac.nmuest.ToString()),
                        status = xoperac.status.ToString(),
                        dobser = xoperac.dobser.ToString(),
                        smodif = "R",
                        nordct = xoperac.nordct.ToString(),
                        npieza = Int32.Parse(xoperac.npieza.ToString()),
                        dpieza = xoperac.dpieza.ToString(),
                        clotei = xoperac.clotei.ToString(),
                        citems = xoperac.citems.ToString(),
                        ditems = xoperac.ditems.ToString(),
                        cencog = xoperac.cencog.ToString(),
                        dtalla = xoperac.dtalla.ToString(),
                        qprend = Int32.Parse(xoperac.qprend.ToString()),
                        npanos = Int32.Parse(xoperac.npanos.ToString()),
                        cmaqui = (xoperac.cmaqui ?? string.Empty),
                        cturno = (xoperac.cturno ?? string.Empty)
                        }
                    };
                    
                    if (selkanban.careas == "FC") App.Navigator.PushAsync(new CosturaFinalPage(dataok));                    
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", ex.StackTrace, "OK");
            }
        }

        private async void ListView_cfinal_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            string newclinea;
            try
            {
                if (await DisplayAlert("Aviso", "Desea realizar la reauditoria", "Si", "No"))
                {
                    var selkanban = (e.ItemData) as taudit00;
                    //using (var data = new DataAccess())
                    //{
                    if (xccargo == "56" || xccargo == "57") { newclinea = ""; }
                    else { newclinea = selkanban.clinea; }

                    xoperac = App.baseDatos.GetList<taudit00>(false).Where(x => x.nsecue == selkanban.nsecue && x.idaudi == selkanban.idaudi && x.clinea == newclinea && x.status == "D" && x.careas == selkanban.careas && x.faudit.ToString("dd-MM-yyyy") == selkanban.faudit.ToString("dd-MM-yyyy")).FirstOrDefault();
                    //}

                    //using (var data = new DataAccess())
                    //{
                    var audenvio = App.baseDatos.GetList<taudit00>(false).Where(x => x.careas == selkanban.careas && x.clinea == newclinea && x.nsecue == selkanban.nsecue && x.idaudi == selkanban.idaudi && x.faudit.ToString("dd-MM-yyyy") == selkanban.faudit.ToString("dd-MM-yyyy") && x.sreaud == "S").ToList();
                    if (audenvio.Count > 0)
                    {
                        await DisplayAlert("Aviso", "Ya se realizo la reauditoria", "OK");
                        return;
                    }
                    //}

                    var dataok = new List<taudit00>
                    {
                        new taudit00
                        {
                        idaudi = xoperac.idaudi,
                        careas = xoperac.careas.ToString(),
                        faudit = DateTime.Parse(xoperac.faudit.ToString()),
                        nsecue = Int32.Parse(xoperac.nsecue.ToString()),
                        clinea = xoperac.clinea.ToString(),
                        nordpr = xoperac.nordpr.ToString(),
                        ccarub = xoperac.ccarub.ToString(),
                        dcarub = xoperac.dcarub.ToString(),
                        ctraba = xoperac.ctraba.ToString(),
                        copera = xoperac.copera.ToString(),
                        dopera = xoperac.dopera.ToString(),
                        dclien = xoperac.dclien.ToString(),
                        nlotes = Int32.Parse(xoperac.nlotes.ToString()),
                        nmuest = Int32.Parse(xoperac.nmuest.ToString()),
                        status = xoperac.status.ToString(),
                        dobser = xoperac.dobser.ToString(),
                        smodif = "R",
                        nordct = xoperac.nordct.ToString(),
                        npieza = Int32.Parse(xoperac.npieza.ToString()),
                        dpieza = xoperac.dpieza.ToString(),
                        clotei = xoperac.clotei.ToString(),
                        citems = xoperac.citems.ToString(),
                        ditems = xoperac.ditems.ToString(),
                        cencog = xoperac.cencog.ToString(),
                        dtalla = xoperac.dtalla.ToString(),
                        qprend = Int32.Parse(xoperac.qprend.ToString()),
                        npanos = Int32.Parse(xoperac.npanos.ToString()),
                        cmaqui = (xoperac.cmaqui ?? string.Empty),
                        cturno = (xoperac.cturno ?? string.Empty)
                        }
                    };
                    
                    if (selkanban.careas == "FC") App.Navigator.PushAsync(new CosturaFinalPage(dataok));                    
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", ex.StackTrace, "OK");
            }
        }

        private async void ListView_transfer_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            string newclinea;
            try
            {
                if (await DisplayAlert("Aviso", "Desea realizar la reauditoria", "Si", "No"))
                {
                    var selkanban = (e.ItemData) as taudit00;
                    //using (var data = new DataAccess())
                    //{
                    if (xccargo == "56" || xccargo == "57") { newclinea = ""; }
                    else { newclinea = selkanban.clinea; }

                    xoperac = App.baseDatos.GetList<taudit00>(false).Where(x => x.nsecue == selkanban.nsecue && x.idaudi == selkanban.idaudi && x.clinea == newclinea && x.status == "D" && x.careas == selkanban.careas && x.faudit.ToString("dd-MM-yyyy") == selkanban.faudit.ToString("dd-MM-yyyy")).FirstOrDefault();
                    //}

                    //using (var data = new DataAccess())
                    //{
                    var audenvio = App.baseDatos.GetList<taudit00>(false).Where(x => x.careas == selkanban.careas && x.clinea == newclinea && x.nsecue == selkanban.nsecue && x.idaudi == selkanban.idaudi && x.faudit.ToString("dd-MM-yyyy") == selkanban.faudit.ToString("dd-MM-yyyy") && x.sreaud == "S").ToList();
                    if (audenvio.Count > 0)
                    {
                        await DisplayAlert("Aviso", "Ya se realizo la reauditoria", "OK");
                        return;
                    }
                    //}

                    var dataok = new List<taudit00>
                    {
                        new taudit00
                        {
                        idaudi = xoperac.idaudi,
                        careas = xoperac.careas.ToString(),
                        faudit = DateTime.Parse(xoperac.faudit.ToString()),
                        nsecue = Int32.Parse(xoperac.nsecue.ToString()),
                        clinea = xoperac.clinea.ToString(),
                        nordpr = xoperac.nordpr.ToString(),
                        ccarub = xoperac.ccarub.ToString(),
                        dcarub = xoperac.dcarub.ToString(),
                        ctraba = xoperac.ctraba.ToString(),
                        copera = xoperac.copera.ToString(),
                        dopera = xoperac.dopera.ToString(),
                        dclien = xoperac.dclien.ToString(),
                        nlotes = Int32.Parse(xoperac.nlotes.ToString()),
                        nmuest = Int32.Parse(xoperac.nmuest.ToString()),
                        status = xoperac.status.ToString(),
                        dobser = xoperac.dobser.ToString(),
                        smodif = "R",
                        nordct = xoperac.nordct.ToString(),
                        npieza = Int32.Parse(xoperac.npieza.ToString()),
                        dpieza = xoperac.dpieza.ToString(),
                        clotei = xoperac.clotei.ToString(),
                        citems = xoperac.citems.ToString(),
                        ditems = xoperac.ditems.ToString(),
                        cencog = xoperac.cencog.ToString(),
                        dtalla = xoperac.dtalla.ToString(),
                        qprend = Int32.Parse(xoperac.qprend.ToString()),
                        npanos = Int32.Parse(xoperac.npanos.ToString()),
                        cmaqui = (xoperac.cmaqui ?? string.Empty),
                        cturno = (xoperac.cturno ?? string.Empty)
                        }
                    };
                    
                    if (selkanban.careas == "FC") App.Navigator.PushAsync(new CosturaFinalPage(dataok));                    
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", ex.StackTrace, "OK");
            }
        }

        private void Tlb_viewpdf_Clicked(object sender, EventArgs e)
        {
            //App.Navigator.PushAsync(new ProductionOrderPage());
        }
    }

}