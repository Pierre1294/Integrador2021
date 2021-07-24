using Acr.UserDialogs;
using AppPFashions.Data;
using AppPFashions.Interfaces;
using AppPFashions.Models;
using AppPFashions.Services;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPFashions.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResumenAuditoriasPage : INotifyPropertyChanged
    {
        public ObservableCollection<AuditFolder> Folders { get; set; }

        public ObservableCollection<SubFolder> SubFolders { get; set; }
        IDownloader downloader = DependencyService.Get<IDownloader>();

        private ApiService apiService;
        private DialogService dialogService;
        private AlertService alertService;
        List<taudit00> xoperac;
        List<taudit00> xoperacs;
        taudit00 resaud;        
        string taudit01;
        string dimgdef = "";
        string desauditoria;
        string desbloque;
        int newnsecue;

        public string Desauditoria
        {
            get
            {
                return desauditoria;
            }
            set
            {
                if (desauditoria != value)
                {
                    desauditoria = value;
                    OnPropertyChanged("Desauditoria");
                }
            }
        }

        public ResumenAuditoriasPage (string taudit)
		{
            apiService = new ApiService();
            dialogService = new DialogService();
            taudit01 = taudit;

            InitializeComponent();
            BindingContext = this;

            LoadResumenAuditorias();
            if (taudit01 == "19") Desauditoria = "Auditoria Costura Proceso - Bloques";
            if (taudit01 == "FC") Desauditoria = "Auditoria Costura Final - Bloques";
            if (taudit01 == "16") Desauditoria = "Auditoria Corte - Modulos";
            if (taudit01 == "29") Desauditoria = "Auditoria Bordado";
            if (taudit01 == "33") Desauditoria = "Auditoria Estampado";
            if (taudit01 == "31") Desauditoria = "Auditoria Transfer";
            if (taudit01 == "17") Desauditoria = "Auditoria Acabado Proceso";
            if (taudit01 == "FI") Desauditoria = "Auditoria Final";
            

            if (taudit01 == "19") desbloque = "Bloque ";
            if (taudit01 == "FC") desbloque = "Bloque ";
            if (taudit01 == "16") desbloque = "Módulo ";
            if (taudit01 == "29") desbloque = "Bordadora ";
            if (taudit01 == "33") desbloque = "Maquina ";
            if (taudit01 == "31") desbloque = "Maquina ";
            if (taudit01 == "17") desbloque = "Linea ";
            if (taudit01 == "FI") desbloque = "Linea ";

        }

        protected override void OnAppearing()
        {
            LoadResumenAuditorias();
            base.OnAppearing();
        }

        async void LoadResumenAuditorias()
        {
            try
            {
                Folders = GenerateItems();
                treeView.ItemsSource = Folders;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", ex.StackTrace, "OK");
            }                      
        }

        private ObservableCollection<AuditFolder> GenerateItems()
        {
            int conta = 1;
            var folders = new ObservableCollection<AuditFolder>(); 
            //using (var data = new DataAccess())
            //{
                //xoperac = data.GetList<taudit00>(false).Where(x => x.careas == taudit01).OrderBy(x => x.clinea).ToList();

                //var taudit02 = (from a in xoperac
                //                group a by new
                //                {
                //                    a.clinea,                                    
                //                }
                //       into b
                //                select new
                //                {
                //                    Clinea = b.Key.clinea,
                //                    Qtaudi = b.Count()
                //                }).ToList();
                var resumenaudit = App.baseDatos.GetList<raudit00>(false).Where(x => x.careas == taudit01).GroupBy(x=>x.clinea).Select(x=>x.First()).OrderBy(x => x.clinea).ToList();
                foreach (var record in resumenaudit)
                {
                    var subfol01 = new SubFolder();
                    var subfolders = new ObservableCollection<SubFolder>();
                    int xqaprob = App.baseDatos.GetList<raudit00>(false).Where(x => x.careas == taudit01 && x.clinea == record.clinea).ToList().Sum(x => x.qaprob);
                    int xqdesap = App.baseDatos.GetList<raudit00>(false).Where(x => x.careas == taudit01 && x.clinea == record.clinea).ToList().Sum(x => x.qdesap);
                    int xqaprex = App.baseDatos.GetList<raudit00>(false).Where(x => x.careas == taudit01 && x.clinea == record.clinea).ToList().Sum(x => x.qaprex);
                    int xqprapr = App.baseDatos.GetList<raudit00>(false).Where(x => x.careas == taudit01 && x.clinea == record.clinea).ToList().Sum(x => x.qprapr);
                var fol01 = new AuditFolder() { FolderName = desbloque + record.clinea , ImageName= "ic_bloque.jpg", AuditCount = xqaprob + xqdesap + xqaprex + xqprapr, FontBold=1 };
                    conta = 1;

                    //xoperacs = data.GetList<taudit00>(false).Where(x => x.careas == taudit01 && x.clinea==record.Clinea).OrderByDescending(x=>x.faudit).ToList();
                    //var taudit03 = (from a in xoperacs
                    //                group a by new
                    //                {
                    //                    a.faudit.Date,
                    //                }
                    //       into b
                    //                select new
                    //                {
                    //                    Faudit = b.Key.Date,                                                                                
                    //                }).ToList();             
                    var resumenlinea = App.baseDatos.GetList<raudit00>(false).Where(x => x.careas == taudit01 && x.clinea == record.clinea).OrderByDescending(x => x.faudit).ToList();
                    foreach (var recorda in resumenlinea)
                    {
                        if (taudit01 == "16")
                        {

                            if (conta == 1)
                            {
                                subfol01 = new SubFolder() { FolderName = recorda.faudit.ToString("dd-MM-yyyy"), ImageName = "ic_calendar_now.png", AuditCount = recorda.qaprob + recorda.qdesap + recorda.qaprex, Clinea = record.clinea, Faudit = recorda.faudit };
                                subfol01.DetFolder = new ObservableCollection<DetFolder>
                                    {
                                        new DetFolder() { FolderName = "Aprobado", ImageName= "ic_aprobado.png" , AuditCount = recorda.qaprob , Careas=taudit01, Status="A", Clinea=record.clinea, Faudit=recorda.faudit.ToString("dd-MM-yyyy")},
                                        new DetFolder() { FolderName = "Desaprobado", ImageName= "ic_desaprobado.png" , AuditCount = recorda.qdesap, Careas=taudit01, Status="D", Clinea=record.clinea, Faudit=recorda.faudit.ToString("dd-MM-yyyy") },
                                        new DetFolder() { FolderName = "Aprobado Ext.", ImageName= "ic_aprobext.png" , AuditCount = recorda.qaprex, Careas=taudit01, Status="E", Clinea=record.clinea, Faudit=recorda.faudit.ToString("dd-MM-yyyy") },
                                        new DetFolder() { FolderName = "Pre-Aprobado", ImageName= "ic_preaprobado.png" , AuditCount = recorda.qprapr, Careas=taudit01, Status="P", Clinea=record.clinea, Faudit=recorda.faudit.ToString("dd-MM-yyyy") }
                                    };
                                subfolders.Add(subfol01);
                            }
                            if (conta > 1)
                            {
                                subfol01 = new SubFolder() { FolderName = recorda.faudit.ToString("dd-MM-yyyy"), ImageName = "ic_calendar_past.png", AuditCount = recorda.qaprob + recorda.qdesap + recorda.qaprex, Clinea = record.clinea, Faudit = recorda.faudit };
                                subfol01.DetFolder = new ObservableCollection<DetFolder>
                                    {
                                        new DetFolder() { FolderName = "Aprobado",ImageName= "ic_aprobado.png" , AuditCount = recorda.qaprob  , Careas=taudit01, Status="A", Clinea=record.clinea, Faudit=recorda.faudit.ToString("dd-MM-yyyy")},
                                        new DetFolder() { FolderName = "Desaprobado", ImageName= "ic_desaprobado.png" , AuditCount = recorda.qdesap , Careas=taudit01, Status="D", Clinea=record.clinea, Faudit=recorda.faudit.ToString("dd-MM-yyyy") },
                                        new DetFolder() { FolderName = "Aprobado Ext.", ImageName= "ic_aprobext.png", AuditCount = recorda.qaprex , Careas=taudit01, Status="E", Clinea=record.clinea, Faudit=recorda.faudit.ToString("dd-MM-yyyy")},
                                        new DetFolder() { FolderName = "Pre-Aprobado", ImageName= "ic_preaprobado.png", AuditCount = recorda.qprapr , Careas=taudit01, Status="P", Clinea=record.clinea, Faudit=recorda.faudit.ToString("dd-MM-yyyy")}
                                    };
                                subfolders.Add(subfol01);
                            }
                        }
                        else
                        {
                            if (conta == 1)
                            {
                                subfol01 = new SubFolder() { FolderName = recorda.faudit.ToString("dd-MM-yyyy"), ImageName = "ic_calendar_now.png", AuditCount = recorda.qaprob + recorda.qdesap + recorda.qaprex, Clinea = record.clinea, Faudit = recorda.faudit };
                                subfol01.DetFolder = new ObservableCollection<DetFolder>
                                        {
                                            new DetFolder() { FolderName = "Aprobado", ImageName= "ic_aprobado.png" , AuditCount = recorda.qaprob , Careas=taudit01, Status="A", Clinea=record.clinea, Faudit=recorda.faudit.ToString("dd-MM-yyyy")},
                                            new DetFolder() { FolderName = "Desaprobado", ImageName= "ic_desaprobado.png" , AuditCount = recorda.qdesap, Careas=taudit01, Status="D", Clinea=record.clinea, Faudit=recorda.faudit.ToString("dd-MM-yyyy") },
                                            new DetFolder() { FolderName = "Aprobado Ext.", ImageName= "ic_aprobext.png" , AuditCount = recorda.qaprex, Careas=taudit01, Status="E", Clinea=record.clinea, Faudit=recorda.faudit.ToString("dd-MM-yyyy") }
                                        };
                                subfolders.Add(subfol01);
                            }
                            if (conta > 1)
                            {
                                subfol01 = new SubFolder() { FolderName = recorda.faudit.ToString("dd-MM-yyyy"), ImageName = "ic_calendar_past.png", AuditCount = recorda.qaprob + recorda.qdesap + recorda.qaprex, Clinea = record.clinea, Faudit = recorda.faudit };
                                subfol01.DetFolder = new ObservableCollection<DetFolder>
                                        {
                                            new DetFolder() { FolderName = "Aprobado",ImageName= "ic_aprobado.png" , AuditCount = recorda.qaprob  , Careas=taudit01, Status="A", Clinea=record.clinea, Faudit=recorda.faudit.ToString("dd-MM-yyyy")},
                                            new DetFolder() { FolderName = "Desaprobado", ImageName= "ic_desaprobado.png" , AuditCount = recorda.qdesap , Careas=taudit01, Status="D", Clinea=record.clinea, Faudit=recorda.faudit.ToString("dd-MM-yyyy") },
                                            new DetFolder() { FolderName = "Aprobado Ext.", ImageName= "ic_aprobext.png", AuditCount = recorda.qaprex , Careas=taudit01, Status="E", Clinea=record.clinea, Faudit=recorda.faudit.ToString("dd-MM-yyyy")}
                                        };
                                subfolders.Add(subfol01);
                            }
                        }
                    conta = conta + 1;
                    }
                    fol01.SubFolder = subfolders;
                    folders.Add(fol01);
                }
            //}
                            
            return folders;
        }

        private void TreeView_ItemTapped(object sender, Syncfusion.XForms.TreeView.ItemTappedEventArgs e)
        {
            string xclinea;
            var selaudit = (e.Node.Content) as DetFolder;     

            if (selaudit != null)
            {
                if ((selaudit.Clinea ?? string.Empty).Length == 0)
                {
                    xclinea = "00";
                }
                else
                {
                    xclinea = selaudit.Clinea;
                }
              
            }
        }

        private void fab_nuevaauditoria_Clicked(object sender, EventArgs e)
        {
            //App.Navigator.PushAsync(new ImageEditorPage());
            List<taudit00> dataok = new List<taudit00>();
            
            if (taudit01 == "FC") App.Navigator.PushAsync(new CosturaFinalPage(dataok));            
        }

        private void tlb_sincroauditoria_Clicked(object sender, EventArgs e)
        {

            GrabaAuditoriaDB();
            GrabaDefectoDB();            
        }

        async void GrabaAuditoriaDB()
        {
            try
            {            
                var ultregis = 0;
                using (var client = new HttpClient())
                {

                    var listobs = App.baseDatos.GetList<paudob00>(false).Where(x => x.senvio == "N").ToList();
                    foreach (var recordobs in listobs)
                    {
                        var regobs = new paudob00
                        {
                            fregis = recordobs.fregis,
                            clinea = recordobs.clinea,
                            dobser = recordobs.dobser,
                            cusuar = recordobs.cusuar,
                        };

                        var jsondef = JsonConvert.SerializeObject(regobs);
                        var contentdef = new StringContent(jsondef, System.Text.Encoding.UTF8, "application/json");

                        var uriobs = "http://192.168.0.26:7030/api/paudob00";
                        var resultobs = await client.PostAsync(uriobs, contentdef);

                        if (!resultobs.IsSuccessStatusCode)
                        {
                            await DisplayAlert("Error", "Hubo un error con el Servicio Web, Por favor reintente guardar 4. " + resultobs.RequestMessage.ToString(), "OK");
                            return;
                        }

                        var resultStringdef = await resultobs.Content.ReadAsStringAsync();
                        var postdef = JsonConvert.DeserializeObject<pdefec01>(resultStringdef);

                        paudob00 updobs = new paudob00
                        {      
                            idobse = recordobs.idobse,
                            fregis = recordobs.fregis,
                            clinea = recordobs.clinea,
                            dobser = recordobs.dobser,
                            cusuar = recordobs.cusuar,
                            senvio = "S",
                        };
                        App.baseDatos.Update(updobs);
                    }
                    //using (var data = new DataAccess())
                    //{
                    var listickets = App.baseDatos.GetList<paudit01>(false).Where(x => x.senvio == "N" && x.careas == taudit01).OrderBy(x=>x.nsecue).ToList();                        
                        int qlistau = listickets.Count();
                        if (qlistau==0)
                        {
                            await DisplayAlert("Aviso", "No existen auditorias pendientes para sincronizar", "OK");
                        }
                        int xqaudi = 0;
                        using (IProgressDialog fooDialog = UserDialogs.Instance.Progress("Sincronizando...", null, null, true, MaskType.Black))
                        {
                            foreach (var record in listickets)
                            {
                            if (taudit01 == "16")
                            {
                                var responseur = await apiService.GetUltRegistroCorte(record.careas, record.faudit.Date.ToString("yyyy-MM-dd"));
                                if (!responseur.IsSuccess)
                                {
                                    ultregis = 0;
                                }
                                else
                                {
                                    var dataultreg = (paudit02)responseur.Result;
                                    if (dataultreg.clinea != null || dataultreg.ultreg > 0)
                                    {
                                        ultregis = dataultreg.ultreg;
                                    }
                                }
                            }
                            else
                            {
                                var responseur = await apiService.GetUltRegistro(record.careas, record.faudit.Date.ToString("yyyy-MM-dd"), record.clinea);
                                if (!responseur.IsSuccess)
                                {
                                    ultregis = 0;
                                }
                                else
                                {
                                    var dataultreg = (paudit02)responseur.Result;
                                    if (dataultreg.clinea != null || dataultreg.ultreg > 0)
                                    {
                                        ultregis = dataultreg.ultreg;
                                    }
                                }
                            }                      

                                if (record.flgrau == "S")
                                {
                                    var listnsecueref = App.baseDatos.GetList<paudit01>(false).Where(x => x.careas == taudit01 && x.clinea == record.clinea && x.copera == record.copera && x.ctraba == record.ctraba && x.ccarub == record.ccarub && x.nordpr==record.nordpr && x.nordct==record.nordct && (x.status == "D" || x.status == "P")).FirstOrDefault();
                                    if (listnsecueref != null)
                                    {
                                        newnsecue = listnsecueref.nseref;
                                    }
                                    else
                                    {
                                        newnsecue = 0;
                                    }
                                }
                                else
                                {
                                    newnsecue = 0;
                                }

                                var novoPost = new paudit01
                                {
                                    careas = record.careas,
                                    faudit = DateTime.Parse(record.faudit.ToShortDateString()),
                                    nsecue = ultregis + 1,//record.nsecue,
                                    clinea = record.clinea,
                                    ctpord = record.ctpord,
                                    nordpr = record.nordpr,
                                    nordct = record.nordct,
                                    cmarbe = record.cmarbe,
                                    ccarub = record.ccarub.Trim(),
                                    npieza = record.npieza,
                                    cencog = record.cencog.Trim(),
                                    citems = record.citems.Trim(),
                                    ccolor = record.ccolor.Trim(),
                                    npanos = record.npanos,
                                    dtalla = record.dtalla.Trim(),
                                    qtotal = record.qtotal,
                                    dlotes = record.dlotes,
                                    ctraba = record.ctraba,
                                    copera = record.copera,
                                    cprove = record.cprove,
                                    cmaqui = record.cmaqui,
                                    cturno = record.cturno,
                                    cparti = record.cparti,
                                    nordco = record.nordco,
                                    npacki = record.npacki,
                                    nlotes = record.nlotes,
                                    nmuest = record.nmuest,
                                    pcierr = record.pcierr,
                                    nrecup = record.nrecup,
                                    nsegun = record.nsegun,
                                    porcen = record.porcen,
                                    flgcie = record.flgcie,
                                    ndefec = record.ndefec,
                                    status = record.status,
                                    dobser = record.dobser.Trim(),
                                    flgrau = record.flgrau,
                                    nreaud = record.nreaud,
                                    caudit = record.caudit,
                                    flgext = record.flgext,
                                    cliref = record.cliref,
                                    fauref = DateTime.Parse(record.fauref.ToShortDateString()),
                                    nseref = newnsecue,//record.nseref,
                                    cusuar = record.cusuar,
                                    fcreac = DateTime.Now,
                                    fmodif = DateTime.Parse(record.fmodif.ToShortDateString()),
                                    nordpo = record.nordpo,
                                    fprogr = DateTime.Parse(record.fprogr.ToShortDateString()),
                                    caudpr = record.caudpr,
                                    drefpr = record.drefpr,
                                    ctpaud = record.ctpaud,
                                    csuppl = record.csuppl,
                                    flgenv = record.flgenv,
                                    sanula = record.sanula,
                                    ndesap = record.ndesap,
                                    sautab = record.sautab,
                                };

                                var json = JsonConvert.SerializeObject(novoPost);
                                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                                var uri = "http://192.168.0.26:7030/api/paudit01";
                                var result = await client.PostAsync(uri, content);

                                if (!result.IsSuccessStatusCode)
                                {
                                    await DisplayAlert("Error", "Hubo un error con el Servicio Web, Por favor reintente guardar 1. " + result.RequestMessage.ToString()+" " +result.StatusCode+" "+result.Content, "OK");
                                    return;
                                }

                                var resultString = await result.Content.ReadAsStringAsync();
                                var post = JsonConvert.DeserializeObject<paudit01>(resultString);


                                //using (var datadef = new DataAccess())
                                //{
                                    int qdefec = App.baseDatos.GetList<pdefec01>(false).Where(x => x.senvio == "N" && x.faudit == record.faudit && x.clinea == record.clinea && x.nsecue == record.nsecue && x.careas == taudit01).Count();
                                    if (qdefec > 0)
                                    {
                                        var lisdef = App.baseDatos.GetList<pdefec01>(false).Where(x => x.senvio == "N" && x.faudit == record.faudit && x.clinea == record.clinea && x.nsecue == record.nsecue && x.careas == taudit01).ToList();
                                        foreach (var recorddef in lisdef)
                                        {
                                            var lisdefimg = App.baseDatos.GetList<pdefec10>(false).Where(x => x.faudit == recorddef.faudit && x.clinea == recorddef.clinea && x.nsecue == recorddef.nsecue && x.careas == taudit01 && x.coddef == recorddef.coddef).ToList();
                                            foreach (var recorddefimg in lisdefimg)
                                            {
                                                dimgdef = recorddefimg.defjpg;
                                            }

                                            var novoPostdef = new pdefec01
                                            {
                                                careas = recorddef.careas,
                                                faudit = DateTime.Parse(recorddef.faudit.ToShortDateString()),
                                                nsecue = ultregis + 1,//recorddef.nsecue,
                                                clinea = recorddef.clinea,
                                                codigo = recorddef.codigo,
                                                coddef = recorddef.coddef,
                                                qcanti = recorddef.qcanti,
                                                dobser = recorddef.dobser,
                                                cgrupo = recorddef.cgrupo,
                                                cardef = recorddef.cardef,
                                                imgdef = dimgdef,

                                            };

                                            var jsondef = JsonConvert.SerializeObject(novoPostdef);
                                            var contentdef = new StringContent(jsondef, System.Text.Encoding.UTF8, "application/json");

                                            var uridef = "http://192.168.0.26:7030/api/pdefec01";
                                            var resultdef = await client.PostAsync(uridef, contentdef);

                                            if (!result.IsSuccessStatusCode)
                                            {
                                                await DisplayAlert("Error", "Hubo un error con el Servicio Web, Por favor reintente guardar 2. " + resultdef.RequestMessage.ToString(), "OK");
                                                return;
                                            }

                                            var resultStringdef = await result.Content.ReadAsStringAsync();
                                            var postdef = JsonConvert.DeserializeObject<pdefec01>(resultStringdef);

                                            //using (var dataimg = new DataAccess())
                                            //{
                                                int qimgde = App.baseDatos.GetList<pdefec10>(false).Where(x => x.careas == taudit01 && x.faudit == recorddef.faudit && x.clinea == recorddef.clinea && x.nsecue == recorddef.nsecue && x.vimage == true && x.iddefe == recorddef.iddefe).Count();
                                                if (qimgde > 0)
                                                {
                                                    var ldefecimg = App.baseDatos.GetList<pdefec10>(false).Where(x => x.careas == taudit01 && x.faudit == recorddef.faudit && x.clinea == recorddef.clinea && x.nsecue == recorddef.nsecue && x.iddefe == recorddef.iddefe).ToList();
                                                    foreach (var recordimg in ldefecimg)
                                                    {
                                                        var contentimg = new MultipartFormDataContent();
                                                        var path = "/storage/emulated/0/Pictures/Test/" + recordimg.defjpg;

                                                        contentimg.Add(new ByteArrayContent(File.ReadAllBytes(path)), "file", recordimg.defjpg);

                                                        var httpClient = new HttpClient();
                                                        var uploadServiceBaseAddress = "http://192.168.0.26:7030/api/Upload";
                                                        var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, contentimg);

                                                        await httpResponseMessage.Content.ReadAsStringAsync();
                                                    }
                                                }
                                            //}

                                            pdefec01 ndefec = new pdefec01
                                            {
                                                iddefe = recorddef.iddefe,
                                                careas = recorddef.careas,
                                                faudit = DateTime.Parse(recorddef.faudit.ToShortDateString()),
                                                nsecue = recorddef.nsecue,
                                                clinea = recorddef.clinea,
                                                codigo = recorddef.codigo,
                                                coddef = recorddef.coddef,
                                                qcanti = recorddef.qcanti,
                                                dobser = recorddef.dobser,
                                                cgrupo = recorddef.cgrupo,
                                                cardef = recorddef.cardef,
                                                senvio = "S",
                                                imgdef = recorddef.imgdef,
                                            };
                                            App.baseDatos.Update(ndefec);
                                        }
                                    }
                   
                                paudit01 nauditoria = new paudit01
                                {
                                    idaudi = record.idaudi,
                                    careas = record.careas,
                                    faudit = record.faudit,
                                    nsecue = record.nsecue,
                                    clinea = record.clinea,
                                    ctpord = record.ctpord,
                                    nordpr = record.nordpr,
                                    nordct = record.nordct,
                                    cmarbe = record.cmarbe,
                                    ccarub = record.ccarub,
                                    npieza = record.npieza,
                                    cencog = record.cencog,
                                    citems = record.citems,
                                    ccolor = record.ccolor,
                                    npanos = record.npanos,
                                    dtalla = record.dtalla,
                                    qtotal = record.qtotal,
                                    dlotes = record.dlotes,
                                    ctraba = record.ctraba,
                                    copera = record.copera,
                                    cprove = record.cprove,
                                    cmaqui = record.cmaqui,
                                    cturno = record.cturno,
                                    cparti = record.cparti,
                                    nordco = record.nordco,
                                    npacki = record.npacki,
                                    nlotes = record.nlotes,
                                    nmuest = record.nmuest,
                                    pcierr = record.pcierr,
                                    nrecup = record.nrecup,
                                    nsegun = record.nsegun,
                                    porcen = record.porcen,
                                    flgcie = record.flgcie,
                                    ndefec = record.ndefec,
                                    status = record.status,
                                    dobser = record.dobser,
                                    flgrau = record.flgrau,
                                    nreaud = record.nreaud,
                                    caudit = record.caudit,
                                    flgext = record.flgext,
                                    cliref = record.cliref,
                                    fauref = record.fauref,
                                    nseref = ultregis + 1,
                                    cusuar = record.cusuar,
                                    fcreac = record.fcreac,
                                    fmodif = record.fmodif,
                                    nordpo = record.nordpo,
                                    fprogr = record.fprogr,
                                    caudpr = record.caudpr,
                                    drefpr = record.drefpr,
                                    ctpaud = record.ctpaud,
                                    csuppl = record.csuppl,
                                    flgenv = record.flgenv,
                                    sanula = record.sanula,
                                    ndesap = record.ndesap,
                                    sautab = record.sautab,
                                    senvio = "S",
                                };
                                App.baseDatos.Update(nauditoria);

                                xqaudi = xqaudi + 1;
                                fooDialog.PercentComplete = xqaudi;
                                fooDialog.Title = xqaudi + " de " + qlistau;
                                await Task.Delay(10);
                            }
                        }
                    //}
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Hubo un error con el Servicio Web, Por favor reintente guardar 3. " + ex.Message, "OK");
            }
        }

        async void GrabaDefectoDB()
        {
            using (var client = new HttpClient())
            {
                //using (var data = new DataAccess())
                //{
                    var listickets = App.baseDatos.GetList<pdefec01>(false).Where(x => x.senvio == "N").ToList();
                    foreach (var record in listickets)
                    {
                        var novoPost = new pdefec01
                        {
                            careas = record.careas,
                            faudit = DateTime.Parse(record.faudit.ToShortDateString()),
                            nsecue = record.nsecue,
                            clinea = record.clinea,
                            codigo = record.codigo,
                            coddef = record.coddef,
                            qcanti = record.qcanti,
                            dobser = record.dobser,
                            cgrupo = record.cgrupo,
                            cardef = record.cardef,
                        };

                        var json = JsonConvert.SerializeObject(novoPost);
                        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                        var uri = "http://192.168.0.26:7030/api/pdefec01";
                        var result = await client.PostAsync(uri, content);

                        if (!result.IsSuccessStatusCode)
                        {
                            await DisplayAlert("Error", "Hubo un error con el Servicio Web, Por favor reintente guardar. " + result.RequestMessage.ToString(), "OK");
                            return;
                        }

                        var resultString = await result.Content.ReadAsStringAsync();
                        var post = JsonConvert.DeserializeObject<pdefec01>(resultString);

                        pdefec01 ndefec = new pdefec01
                        {
                            iddefe = record.iddefe,
                            careas = record.careas,
                            faudit = DateTime.Parse(record.faudit.ToShortDateString()),
                            nsecue = record.nsecue,
                            clinea = record.clinea,
                            codigo = record.codigo,
                            coddef = record.coddef,
                            qcanti = record.qcanti,
                            dobser = record.dobser,
                            cgrupo = record.cgrupo,
                            cardef = record.cardef,
                            senvio = "S",
                        };
                        App.baseDatos.Update(ndefec);
                    }
                //}
            }
        }

        private async void Tlb_deleteauditoria_Clicked(object sender, EventArgs e)
        {
            //var result = await DisplayAlert("Aviso", "Desea eliminar las auditorias aprobadas y reauditadas", "Si", "No");
            //if (result == true)
            //{
            //    App.baseDatos.Conexion.Execute(@"Delete from pdefec01 where iddefe in ( select iddefe 
            //                                                                        from pdefec01 as a
            //                                                                        inner join taudit00 as b
            //                                                                        inner join paudit01 as c
            //                                                                        where b.faudit = a.faudit and b.careas = a.careas and
            //                                                                                b.clinea = a.clinea and b.nsecue = a.nsecue and
            //                                                                                b.faudit = c.faudit and b.careas = c.careas and
            //                                                                                b.clinea = c.clinea and b.nsecue = c.nsecue and
            //                                                                                b.faudit != (select max(faudit) from paudit01) and c.senvio = 'S' and (b.status in ('A','E') or(b.status = 'D' and b.sreaud = 'S')))");

            //    App.baseDatos.Conexion.Execute(@"Delete from pdefec10 where iddefe in ( select iddefe 
            //                                                                        from pdefec10 as a
            //                                                                        inner join taudit00 as b
            //                                                                        inner join paudit01 as c
            //                                                                        where b.faudit = a.faudit and b.careas = a.careas and
            //                                                                                b.clinea = a.clinea and b.nsecue = a.nsecue and
            //                                                                                b.faudit = c.faudit and b.careas = c.careas and
            //                                                                                b.clinea = c.clinea and b.nsecue = c.nsecue and
            //                                                                                b.faudit != (select max(faudit) from paudit01) and c.senvio = 'S' and (b.status in ('A','E') or(b.status = 'D' and b.sreaud = 'S')))");

            //    App.baseDatos.Conexion.Execute(@"Delete from taudit00 where idaudi in ( select b.idaudi 
            //                                                                        from taudit00 as b 
            //                                                                        inner join paudit01 as c 
            //                                                                        where b.faudit=c.faudit and b.careas=c.careas and 
            //                                                                        b.clinea=c.clinea and b.nsecue=c.nsecue and 
	           //                                                                     b.faudit!=(select max(faudit) from paudit01) and c.senvio='S' and (b.status in ('A','E') or (b.status='D' and b.sreaud='S')))");

            //    App.baseDatos.Conexion.Execute(@"Delete from paudit01 where idaudi not in (select idaudi 
            //                                                                        from taudit00)");

            //    App.baseDatos.Conexion.Execute(@"Delete from raudit00");

            //    App.baseDatos.Conexion.Execute(@"Delete from paudob00 where senvio='S'");



            //    var resuad=App.baseDatos.Conexion.Query<raudit00>(@"select careas,faudit,clinea,sum(qaprob)qaprob,sum(qdesap)qdesap,sum(qaprex)qaprex 
            //                                    from (
            //                                        select careas,faudit,clinea,count(*)qaprob,0 qdesap,0 qaprex from paudit01 where status='A' group by careas,faudit,clinea
            //                                        union
            //                                        select careas,faudit,clinea,0,count(*),0 from paudit01 where status='D' group by careas,faudit,clinea
            //                                        union
            //                                        select careas,faudit,clinea,0,0,count(*) from paudit01 where status='E' group by careas,faudit,clinea
            //                                    )f group by careas,faudit,clinea");

            //    foreach (var record in resuad)
            //    {
            //        raudit00 resaudit = new raudit00
            //        {                        
            //            careas = record.careas,
            //            faudit = record.faudit,
            //            clinea = record.clinea,
            //            qaprob = record.qaprob,
            //            qdesap = record.qdesap,
            //            qaprex = record.qaprex,
            //        };
            //        App.baseDatos.Insert(resaudit);
            //    }

            //    LoadResumenAuditorias();
            //    await DisplayAlert("Aviso", "Auditorias eliminadas", "OK");
            //}                    
        }

        private async void TreeView_ItemHolding(object sender, Syncfusion.XForms.TreeView.ItemHoldingEventArgs e)
        {
            var selaudit = (e.Node.Content) as SubFolder;
            if (selaudit != null)
            {
                var result = await DisplayAlert("Aviso", "Desea eliminar las auditorias con fecha "+selaudit.FolderName, "Si", "No");
                if (result == true)
                {
                    //using (var data = new DataAccess())
                    //{
                        //DateTime str = selaudit.Faudit;
                        //var lista = data.GetList<paudit01>(false).Where(x => x.faudit.ToString("dd-MM-yyyy") == selaudit.FolderName && x.clinea == selaudit.Clinea && x.senvio == "S").ToList();
                        //foreach (var recor in lista)
                        //{
                        //    DateTime stra = recor.faudit;
                        //    await DisplayAlert("Aviso", stra.ToString() + ' ' + str.ToString(), "OK");
                        //}

                        var listickets = App.baseDatos.GetList<paudit01>(false).Where(x => x.faudit.ToString("dd-MM-yyyy") == selaudit.FolderName && x.clinea==selaudit.Clinea && x.senvio == "N" && x.careas==taudit01).ToList();
                        int qlistau = listickets.Count();
                        if (qlistau > 0)
                        {
                            await DisplayAlert("Alerta", "Existen auditorias sin sincronizar, por favor sincronice antes de eliminar", "OK");
                            return;
                        }

                        App.baseDatos.DeleteAuditoria(selaudit.Faudit,selaudit.Clinea,taudit01);                        
                        LoadResumenAuditorias();
                    //}
                    await DisplayAlert("Aviso", "Auditorias eliminadas", "OK");

                }
            }
        }
    }
}