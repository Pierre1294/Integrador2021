using Acr.UserDialogs;
using AppPFashions.Data;
using AppPFashions.Interfaces;
using AppPFashions.Models;
using AppPFashions.Services;
using PCLStorage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPFashions.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CosturaFinalPage : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Atributos      
        private DialogService dialogService;
        private AlertService alertService;
        private ApiService apiService;
        private DataService dataService;        
        private MediaFile _mediaFile;        
        string estadoReauditoria = "N";
        string nuevaAuditoria;        
        string sgraud;
        Int32 xidaudi;
        int xqaprob, xqdesap, xqaprex;
        string sapaud = "";
        string obsdefecto;
        string obsaudit,sfchipop,sfchipcombo,sfchipdcarub,selbloque;
        string saudtot;
        string xcoddef, xdedef,xcodigodef;
        string xcliref = "";
        DateTime xfauref = DateTime.Parse("1900-01-01 00:00:00");
        Int32 xnseref = 0;
        public List<string> SectorList { get; set; }
        public List<taudit00> listaCargaAuditoria { get; set; }
        public List<pdefec10> listaCargaDefectos { get; set; }
        ObservableCollection<procos00> dealerDetails = new ObservableCollection<procos00>();
        
        #endregion

        #region Constructor
        public CosturaFinalPage(List<taudit00> listaAuditoria)
        {
            dialogService = new DialogService();
            alertService = new AlertService();
            apiService = new ApiService();
            dataService = new DataService();            
            List<pdefec10> xopera = new List<pdefec10>();
            SectorList = new List<string>
            {
                "01","02","03","04","05","06",
                "07","08","09","10","11","12","SE"
            };

            this.listaCargaAuditoria = listaAuditoria;
            InitializeComponent();

            nuevaAuditoria = "S";
            sgraud = "N";
            pck_bloque.ItemsSource = SectorList;
            if (listaCargaAuditoria.Count != 0)
            {
                CargaAuditoria();
            }
      
            
            //using (var data = new DataAccess())
            //{
            var ldefec = App.baseDatos.GetList<pdefec10>(false).Where(x => x.svigen == "N").ToList();
                foreach (var record in ldefec)
                {
                    pdefec10 cdefecto = new pdefec10
                    {
                        iddefe = record.iddefe,
                        careas = record.careas,
                        faudit = record.faudit,
                        nsecue = record.nsecue,
                        clinea = record.clinea,
                        codigo = record.codigo,
                        coddef = record.coddef,
                        qcanti = record.qcanti,
                        dobser = record.dobser,
                        cgrupo = record.cgrupo,
                        cardef = record.cardef,
                        descri = record.descri,
                        defjpg = record.defjpg,
                        vimage = record.vimage,
                        vphoto = record.vphoto,
                        svigen = record.svigen,
                    };
                    App.baseDatos.Delete(cdefecto);
                }

                var user = App.baseDatos.GetUsuario();
                VariableGlobal.ctraba = user.ctraba;
                VariableGlobal.cusuar = user.cusuar;
            //}
        }
        #endregion

        #region ControlRetroceso
        protected override bool OnBackButtonPressed()
        {
            // Begin an asyncronous task on the UI thread because we intend to ask the users permission.
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (lbl_descliente.Text != null)
                {
                    if (await DisplayAlert("Alerta", "Estas seguro que quiere salir sin guardar los cambios", "Si", "No"))
                    {
                        base.OnBackButtonPressed();
                        await App.Navigator.PopAsync();
                    }
                }
                else
                {
                    base.OnBackButtonPressed();
                    await App.Navigator.PopAsync();
                }
            });
            return true;
        } 
        #endregion

        #region CargaAuditoria
        void CargaAuditoria()
        {
            //********** REAUDITORIA **********//
            if (listaCargaAuditoria.ElementAt(0).status.ToString() == "D" && listaCargaAuditoria.ElementAt(0).smodif.ToString() == "R")
            {
                if (Int32.Parse(listaCargaAuditoria.ElementAt(0).nlotes.ToString()) == Int32.Parse(listaCargaAuditoria.ElementAt(0).nmuest.ToString()))
                {
                    cbxaudittot.IsChecked = true;
                }
                else
                {
                    cbxaudittot.IsChecked = false;
                }
                nuevaAuditoria = "S";
                estadoReauditoria = "S";
                xcliref = listaCargaAuditoria.ElementAt(0).clinea.ToString();
                xfauref = DateTime.Parse(listaCargaAuditoria.ElementAt(0).faudit.ToString());
                xnseref = Int32.Parse(listaCargaAuditoria.ElementAt(0).nsecue.ToString());

                pck_bloque.SelectedItem = listaCargaAuditoria.ElementAt(0).clinea.ToString();
                //dpk_fechaauditoria.Date = DateTime.Parse(listaCargaAuditoria.ElementAt(0).faudit.ToString());
                BuscarOperario();
                //ety_op.Text = listaCargaAuditoria.ElementAt(0).nordpr.ToString();
                //BuscarOP();
                ety_lote.Text = listaCargaAuditoria.ElementAt(0).nlotes.ToString();
                ety_muestra.Text = listaCargaAuditoria.ElementAt(0).nmuest.ToString();
                ety_observ.Text = listaCargaAuditoria.ElementAt(0).dobser.ToString();                
                //ety_op.IsEnabled = false;
                //btn_buscarop.IsEnabled = false;
                btn_agregarauditoria.IsEnabled = false;
                //pck_combo.IsEnabled = false;
                img_defecto.IsEnabled = true;
                pck_defectos.IsEnabled = true;
                ety_cantdefecto.IsEnabled = true;
                ety_obsdefecto.IsEnabled = true;
                btn_agregardefecto.IsEnabled = true;
                ety_lote.IsEnabled = true;
                ety_muestra.IsEnabled = true;
                btn_guardarauditoria.IsEnabled = true;
                //using (var data = new DataAccess())
                //{
                var datosSecuencia = App.baseDatos.GetList<paudit01>(false).Where(a => a.clinea == pck_bloque.SelectedItem.ToString() && a.careas == "FC" && a.faudit== DateTime.Parse(dpk_fechaauditoria.Date.ToShortDateString())).ToList();
                if (datosSecuencia.Count > 0)
                {
                    var ultimaAuditoria = App.baseDatos.GetList<paudit01>(false).Where(a => a.clinea == pck_bloque.SelectedItem.ToString() && a.careas == "FC").OrderByDescending(x => x.nsecue).First();
                    lbl_nsecue.Text = (ultimaAuditoria.nsecue + 1).ToString();
                }
                else
                { lbl_nsecue.Text = "1"; }

                var datosId = App.baseDatos.GetList<paudit01>(false).ToList();
                if (datosId.Count > 0)
                {
                    var ultimoIdAuditoria = App.baseDatos.GetList<paudit01>(false).OrderByDescending(x => x.idaudi).First();
                    xidaudi = ultimoIdAuditoria.idaudi + 1;
                }
                else
                { xidaudi = 1; }
                //}
            }
            //********** EDITAR AUDITORIA **********//
            else
            {
                if (Int32.Parse(listaCargaAuditoria.ElementAt(0).nlotes.ToString()) == Int32.Parse(listaCargaAuditoria.ElementAt(0).nmuest.ToString()))
                {
                    cbxaudittot.IsChecked = true;
                }
                else
                {
                    cbxaudittot.IsChecked = false;
                }
                nuevaAuditoria = "N";
                estadoReauditoria = "N";
                pck_bloque.SelectedItem = listaCargaAuditoria.ElementAt(0).clinea.ToString();
                dpk_fechaauditoria.Date = DateTime.Parse(listaCargaAuditoria.ElementAt(0).faudit.ToString());
                lbl_nsecue.Text = listaCargaAuditoria.ElementAt(0).nsecue.ToString();
                BuscarOperario();
                //ety_op.Text = listaCargaAuditoria.ElementAt(0).nordpr.ToString();
                //BuscarOP();
                ety_lote.Text = listaCargaAuditoria.ElementAt(0).nlotes.ToString();
                ety_muestra.Text = listaCargaAuditoria.ElementAt(0).nmuest.ToString();
                ety_observ.Text = listaCargaAuditoria.ElementAt(0).dobser.ToString();
                if (listaCargaAuditoria.ElementAt(0).status.ToString() == "A") srb_audiaprobado.IsChecked = true;
                if (listaCargaAuditoria.ElementAt(0).status.ToString() == "D") srb_audidesaprobado.IsChecked = true;
                if (listaCargaAuditoria.ElementAt(0).status.ToString() == "E") srb_audiaprobadoext.IsChecked = true;
                //using (var data = new DataAccess())
                //{
                listaCargaDefectos = App.baseDatos.GetList<pdefec10>(false).Where(x => x.clinea == listaCargaAuditoria.ElementAt(0).clinea.ToString() && x.faudit.Date == listaCargaAuditoria.ElementAt(0).faudit.Date && x.nsecue == listaCargaAuditoria.ElementAt(0).nsecue).ToList();
                lsv_defectos.ItemsSource = listaCargaDefectos;
                //}

                //ety_op.IsEnabled = true;
                //btn_buscarop.IsEnabled = true;
                //pck_combo.IsEnabled = true;
                img_defecto.IsEnabled = true;
                pck_defectos.IsEnabled = true;
                ety_cantdefecto.IsEnabled = true;
                ety_obsdefecto.IsEnabled = true;
                btn_agregardefecto.IsEnabled = true;
                ety_lote.IsEnabled = true;
                ety_muestra.IsEnabled = true;
                btn_guardarauditoria.IsEnabled = true;
            }
        }
        #endregion

        async void GetProgramaCostura()
        {
            DependencyService.Get<IDownloader>().Show("Descargando");
        
            var response = await apiService.GetOpProgCostura<procos00>(pck_bloque.SelectedItem.ToString());
            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Aviso", response.Message);
                DependencyService.Get<IDownloader>().Hide();
                btn_agregarauditoria.IsEnabled = true;
                return;
            }

            var fichas = (List<procos00>)response.Result;
            App.baseDatos.DeleteAllProgCostura();
            //var progcos = fichas.GroupBy(x => x.nordpr).Select(x => x);
            foreach (var recordf in fichas)
            {
                var ord = new procos00()
                {
                    nordpr = recordf.nordpr,
                    ccarub = recordf.ccarub,
                    dcarub = recordf.dcarub,
                    dclien = recordf.dclien,
                    nivaql = recordf.nivaql
                };
                App.baseDatos.Insert<procos00>(ord);
                //dealerDetails.Add(ord);
            }

            var progcos = App.baseDatos.GetList<procos00>(false).GroupBy(x => x.nordpr).Select(x => x);
            foreach (var recordf in progcos)
            {
                var ord = new procos00()
                {
                    nordpr = recordf.Key
                };
                dealerDetails.Add(ord);
            }
            
            sfchip_op.ItemsSource = dealerDetails;            

            DependencyService.Get<IDownloader>().Hide();
        }

        #region NuevaAuditoria
        public void AgregarNuevaAuditoria()
        {
            if (pck_bloque.SelectedItem == null)
            {
                DisplayAlert("Aviso", "Debe seleccionar un bloque", "OK");
                return;
            }
            GetProgramaCostura();
            //using (var data = new DataAccess())
            //{
            var datosSecuencia = App.baseDatos.GetList<paudit01>(false).Where(a => a.clinea == pck_bloque.SelectedItem.ToString() && a.careas == "FC" && a.faudit.ToShortDateString() == dpk_fechaauditoria.Date.ToShortDateString()).ToList();
                if (datosSecuencia.Count > 0)
                {
                    var ultimaAuditoria = App.baseDatos.GetList<paudit01>(false).Where(a => a.clinea == pck_bloque.SelectedItem.ToString() && a.careas == "FC" && a.faudit.ToShortDateString() == dpk_fechaauditoria.Date.ToShortDateString()).OrderByDescending(x => x.nsecue).First();
                    lbl_nsecue.Text = (ultimaAuditoria.nsecue + 1).ToString();
                }
                else
                { lbl_nsecue.Text = "1"; }

                var datosId = App.baseDatos.GetList<paudit01>(false).ToList();
                if (datosId.Count > 0)
                {
                    var ultimoIdAuditoria = App.baseDatos.GetList<paudit01>(false).OrderByDescending(x => x.idaudi).First();
                    xidaudi = ultimoIdAuditoria.idaudi + 1;
                }
                else
                { xidaudi = 1; }
            //}

            //ety_op.IsEnabled = true;
            //btn_buscarop.IsEnabled = true;
            //pck_combo.IsEnabled = true;
            sf_check_op.IsEnabled = true;
            img_defecto.IsEnabled = true;
            pck_defectos.IsEnabled = true;
            ety_cantdefecto.IsEnabled = true;
            ety_obsdefecto.IsEnabled = true;
            btn_agregardefecto.IsEnabled = true;
            ety_lote.IsEnabled = true;
            ety_muestra.IsEnabled = true;
            ety_observ.IsEnabled = true;
            btn_guardarauditoria.IsEnabled = true;
            btn_agregarauditoria.IsEnabled = false;
        } 
        #endregion

        private void btn_agregarauditoria_Clicked(object sender, EventArgs e)
        {
            AgregarNuevaAuditoria();
        }

        void BuscarOperario()
        {
            //if (string.IsNullOrEmpty(ety_ctraba.Text))
            //{
            //    DisplayAlert("Error", "Ingrese un código de trabajador.", "OK");
            //    ety_ctraba.Focus();
            //    return;
            //}

            //if (ety_ctraba.Text.Length == 3)
            //{
            //    ety_ctraba.Text = "O00" + ety_ctraba.Text;
            //}

            //if (ety_ctraba.Text.Length == 4)
            //{
            //    ety_ctraba.Text = "O0" + ety_ctraba.Text;
            //}

            //if (ety_ctraba.Text.Length == 5)
            //{
            //    ety_ctraba.Text = "O" + ety_ctraba.Text;
            //}

            //using (var datos = new DataAccess())
            //{
            //    var xctraba = datos.GetOperario(ety_ctraba.Text);
            //    if (xctraba == null)
            //    {
            //        DisplayAlert("Error", "El código de trabajador no existe o no esta activo", "OK");
            //        return;
            //    }
            //    lbl_dtraba.Text = xctraba.dtraba;
            //}
        }
        private void btn_ctraba_Clicked(object sender, EventArgs e)
        {
            BuscarOperario();
        }

        #region BuscarOP
        async void BuscarOP()
        {
            if (string.IsNullOrEmpty(ety_op.Text))
            {
                await DisplayAlert("Error", "Ingrese un número de OP", "OK");
                ety_op.Focus();
                return;
            }
            App.baseDatos.DeleteProgOP(ety_op.Text);
            DependencyService.Get<IDownloader>().Show("Cargando");
            var response = await apiService.GetOP<ordprod>(ety_op.Text);
            if (!response.IsSuccess)
            {
                await DisplayAlert("Error", response.Message, "OK");
                DependencyService.Get<IDownloader>().Hide();
                ety_op.Focus();
                return;
            }

            var fichas = (List<ordprod>)response.Result;
            var progcos01 = fichas.Where(x => x.nordpr == ety_op.Text).GroupBy(x => new { x.dcarub, x.ccarub, x.nordpr,x.dclien,x.nivaql }).Select(y => new { dcarub = y.Key.dcarub.Trim(), ccarub = y.Key.ccarub, nordpr = y.Key.nordpr, dclien=y.Key.dclien.Trim(), nivaql=y.Key.nivaql });
     

            foreach (var recordf in progcos01)
            {
                var ord = new procos00()
                {
                    nordpr = recordf.nordpr,
                    ccarub = recordf.ccarub,
                    dcarub = recordf.dcarub,
                    dclien = recordf.dclien,
                    nivaql = recordf.nivaql
                };
                App.baseDatos.Insert<procos00>(ord);
                //dealerDetails.Add(ord);
            }


            //var dataop = (List<ordprod>)response.Result;
            //foreach (var record in dataop.OrderBy(x => x.nordct))
            //{
            //    lbl_descliente.Text = record.dclien;
            //}
            DependencyService.Get<IDownloader>().Hide();


            ObservableCollection<procos00> listcombo = new ObservableCollection<procos00>();

            var descli = App.baseDatos.GetList<procos00>(false).Where(x => x.nordpr == ety_op.Text).First();
            lbl_descliente.Text = descli.dclien;
            sfchipop = ety_op.Text;

            var progcos = App.baseDatos.GetList<procos00>(false).Where(x => x.nordpr == ety_op.Text).GroupBy(x => new { x.dcarub, x.ccarub, x.nordpr }).Select(y => new { dcarub = y.Key.dcarub, ccarub = y.Key.ccarub, nordpr = y.Key.nordpr });
            foreach (var recordf in progcos)
            {
                var ord = new procos00()
                {
                    dcarub = recordf.dcarub,
                    ccarub = recordf.ccarub
                };
                listcombo.Add(ord);
            }

            sfchip_combo.ItemsSource = listcombo;
            sfchipcombo = "";
            sfchipop = ety_op.Text;

            //*** Llena Picker Defectos ***//            
            var xdefec = App.baseDatos.GetList<mdefec10>(false).Where(x => x.caraud == "FC").OrderBy(a => a.coddef);

            if (xdefec == null)
            {
                await dialogService.ShowMessage("Error", "No existen registros");
                return;
            }
            pck_defectos.DataSource = xdefec.OrderBy(x => x.coddef).ToList();

            lbl_bloquedef.Text = pck_bloque.SelectedItem.ToString();
            lbl_fechadef.Text = dpk_fechaauditoria.Date.ToString("dd - MMM - yyyy");

            //*** Llena Picker Combo ***//
            //pck_combo.Items.Clear();
            //var datos = (from dat in dataop
            //             group dat by new { dat.ccarub, dat.dcarub } into g
            //             select new { ccarub = g.Key.ccarub, dcarub = g.Key.dcarub });
            //foreach (var record in datos)
            //{
            //    pck_combo.Items.Add(record.ccarub.Trim() + " - " + record.dcarub.Trim());
            //}

            //if (listaCargaAuditoria.Count != 0)
            //{
            //    pck_combo.SelectedItem = listaCargaAuditoria.ElementAt(0).ccarub.ToString().Trim() + " - " + listaCargaAuditoria.ElementAt(0).dcarub.ToString().Trim();
            //}

            //*** Llena Picker Defectos ***//            
            //using (var data = new DataAccess())
            //{
            //var xdefec = App.baseDatos.GetList<mdefec00>(false).Where(x => x.csecci == "19").OrderBy(a => a.coddef);
            //var xdefec = App.baseDatos.GetList<mdefec10>(false).Where(x => x.caraud == "FC").OrderBy(a => a.coddef);

            //if (xdefec == null)
            //{
            //    await dialogService.ShowMessage("Error", "No existen registros");
            //    return;
            //}
            //pck_defectos.DataSource = xdefec.OrderBy(x => x.coddef).ToList();
            ////}

            //lbl_bloquedef.Text = pck_bloque.SelectedItem.ToString();
            //lbl_fechadef.Text = dpk_fechaauditoria.Date.ToString("dd - MMM - yyyy");
        }
        #endregion

        private void btn_buscarop_Clicked(object sender, EventArgs e)
        {
            BuscarOP();
        }

        private async void img_operarios_Tapped(object sender, EventArgs e)
        {
            var result = await alertService.ShowMessage("Aviso", "Desea sincronizar la lista de trabajadores");
            if (result == true)
            {
                App.baseDatos.DeleteOperarios();
                var response = await apiService.Operarios<mtraba00>();

                if (!response.IsSuccess)
                {
                    await dialogService.ShowMessage("Error", response.Message);
                    return;
                }

                var opera = (List<mtraba00>)response.Result;
                int xqtraba = opera.Count();
                int xtraba = 0;

                using (IProgressDialog fooDialog = UserDialogs.Instance.Progress("Sincronizando...", null, null, true, MaskType.Black))
                {
                    foreach (var record in opera)
                    {
                        mtraba00 operario = new mtraba00
                        {
                            ctraba = record.ctraba,
                            dtraba = record.dtraba,
                            ccargo = record.ccargo,
                            dcargo = record.dcargo,
                            xsecci = record.xsecci,
                            clinea = record.clinea
                        };
                        dataService.InsertOperario(operario);
                        xtraba = xtraba + 1;
                        fooDialog.PercentComplete = xtraba;
                        fooDialog.Title = xtraba + " de " + xqtraba;
                        await Task.Delay(10);
                    }
                }
            }
            else // if it's equal to Cancel
            {
                return; // just return to the page and do nothing.
            }
        }

        private async void img_operaciones_Tapped(object sender, EventArgs e)
        {
            var result = await alertService.ShowMessage("Aviso", "Desea sincronizar la lista de operaciones");
            if (result == true)
            {
                App.baseDatos.DeleteOperaciones();
                var response = await apiService.Operaciones<topera01>();

                if (!response.IsSuccess)
                {
                    await dialogService.ShowMessage("Error", response.Message);
                    return;
                }

                var response01 = await apiService.Aql<ttcmue00>();
                if (!response01.IsSuccess)
                {
                    await dialogService.ShowMessage("Error", response01.Message);
                    return;
                }
                var opera01 = (List<ttcmue00>)response01.Result;

                var opera = (List<topera01>)response.Result;
                int xqtraba = opera.Count();
                int xtraba = 0;

                using (IProgressDialog fooDialog = UserDialogs.Instance.Progress("Sincronizando...", null, null, true, MaskType.Black))
                {
                    foreach (var record in opera)
                    {
                        topera01 operario = new topera01
                        {
                            cclave = record.cclave,
                            descri = record.descri,
                            cgrupo = record.cgrupo,
                            dopera = record.dopera,
                        };
                        dataService.InsertOperacion(operario);
                        xtraba = xtraba + 1;
                        fooDialog.PercentComplete = xtraba;
                        fooDialog.Title = xtraba + " de " + xqtraba;
                        await Task.Delay(10);
                    }
                    foreach (var record in opera01)
                    {
                        ttcmue00 muestra = new ttcmue00
                        {
                            codmue = record.codmue,
                            ntanli = record.ntanli,
                            ntanlf = record.ntanlf,
                            ntanmu = record.ntanmu,
                            nivaql = record.nivaql,
                            caprob = record.caprob
                        };
                        dataService.InsertAql(muestra);
                    }
                }
            }
            else // if it's equal to Cancel
            {
                return; // just return to the page and do nothing.
            }
        }

        private async void img_defectos_Tapped(object sender, EventArgs e)
        {
            var result = await alertService.ShowMessage("Aviso", "Desea sincronizar la lista de defectos");
            if (result == true)
            {                
                //App.baseDatos.DeleteDefectos("19");
                //var response = await apiService.Defectos<mdefec00>("19");
                App.baseDatos.DeleteListaDefectos("FC");
                var response = await apiService.ListaDefectos<mdefec10>("FC");

                if (!response.IsSuccess)
                {
                    await dialogService.ShowMessage("Error", response.Message);
                    return;
                }

//                var opera = (List<mdefec00>)response.Result;
                var opera = (List<mdefec10>)response.Result;
                int xqtraba = opera.Count();
                int xtraba = 0;

                using (IProgressDialog fooDialog = UserDialogs.Instance.Progress("Sincronizando...", null, null, true, MaskType.Black))
                {
                    foreach (var record in opera)
                    {
                        mdefec10 defecto = new mdefec10
                        {
                            caraud = record.caraud,
                            coddef = record.coddef,
                            //descri = record.descri,
                            //dgrupo = record.dgrupo,
                            //codigo = record.codigo,
                            //dapare = record.dapare,
                            csecci = record.csecci,
                            ddefec = record.ddefec,
                            codigo = record.codigo
                        };
                        //dataService.InsertDefecto(defecto);
                        dataService.InsertListaDefecto(defecto);
                        xtraba = xtraba + 1;
                        fooDialog.PercentComplete = xtraba;
                        fooDialog.Title = xtraba + " de " + xqtraba;
                        await Task.Delay(10);
                    }
                }
                //using (var data = new DataAccess())
                //{                    
                    //var xdefec = App.baseDatos.GetList<mdefec00>(false).Where(x => x.csecci == "19").OrderBy(a => a.coddef);
                    var xdefec = App.baseDatos.GetList<mdefec10>(false).Where(x => x.caraud == "FC").OrderBy(a => a.coddef);

                if (xdefec == null)
                    {
                        await dialogService.ShowMessage("Error", "No existen registros");
                        return;
                    }
                    pck_defectos.DataSource = xdefec.OrderBy(x => x.coddef).ToList();

                    App.baseDatos.DeleteAql();
                    var response01 = await apiService.Aql<ttcmue00>();
                    if (!response01.IsSuccess)
                    {
                        await dialogService.ShowMessage("Error", response01.Message);
                        return;
                    }
                    var opera01 = (List<ttcmue00>)response01.Result;

                foreach (var record in opera01)
                {
                    ttcmue00 muestra = new ttcmue00
                    {
                        codmue = record.codmue,
                        ntanli = record.ntanli,
                        ntanlf = record.ntanlf,
                        ntanmu = record.ntanmu,
                        nivaql = record.nivaql,
                        caprob = record.caprob
                    };
                    dataService.InsertAql(muestra);
                }


                //}
            }
            else // if it's equal to Cancel
            {
                return; // just return to the page and do nothing.
            }
        }

        #region AgregarDefecto
        private void AddDefecto()
        {
            if (pck_defectos.SelectedItem == null)
            {
                DisplayAlert("Aviso", "Debe seleccionar un defecto", "OK");
                return;
            }

            if (string.IsNullOrEmpty(ety_cantdefecto.Text))
            {
                DisplayAlert("Error", "Ingrese la cantidad de defectos", "OK");
                ety_cantdefecto.Focus();
                return;
            }

            try
            {
                //using (var data = new DataAccess())
                //{
                    int qdefec = App.baseDatos.GetList<pdefec10>(false).Where(x => x.careas == "FC" && x.clinea == pck_bloque.SelectedItem.ToString() && x.nsecue == Int32.Parse(lbl_nsecue.Text.ToString()) && x.coddef == xcoddef && x.faudit.ToShortDateString() == dpk_fechaauditoria.Date.ToShortDateString()).Count();
                    if (qdefec > 0)
                    {
                        DisplayAlert("Aviso", "Defecto ya fue registrado", "OK");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(ety_obsdefecto.Text))
                    {
                        obsdefecto = "";
                    }
                    else
                    {
                        obsdefecto = ety_obsdefecto.Text;
                    }
                    pdefec10 cdefecto = new pdefec10
                    {
                        careas = "FC",
                        faudit = DateTime.Parse(dpk_fechaauditoria.Date.ToString()),
                        nsecue = Int32.Parse(lbl_nsecue.Text.ToString()),
                        clinea = selbloque,
                        codigo = xcodigodef,
                        coddef = xcoddef,//pck_defectos.SelectedItem.ToString().Substring(0, 2),
                        qcanti = Int32.Parse(ety_cantdefecto.Text.ToString()),
                        dobser = obsdefecto,//ety_obsdefecto.Text = null ?? "",
                        cgrupo = "N",
                        cardef = "19",
                        descri = xdedef,//pck_defectos.SelectedItem.ToString().Substring(5, (pck_defectos.SelectedItem.ToString().Length - 5)),//pck_defectos.SelectedValue.ToString()
                        defjpg = "",
                        vimage = false,
                        vphoto = true,
                        svigen = "N",
                    };
                    App.baseDatos.Insert(cdefecto);

                    listaCargaDefectos = App.baseDatos.GetList<pdefec10>(false);
                    lsv_defectos.ItemsSource = listaCargaDefectos.Where(x => x.clinea == pck_bloque.SelectedItem.ToString() && x.nsecue == Int32.Parse(lbl_nsecue.Text.ToString()) && x.careas == "FC" && x.faudit.ToShortDateString() == dpk_fechaauditoria.Date.ToShortDateString());
                    pck_defectos.SelectedItem = null;
                    ety_obsdefecto.Text = "";
                    ety_cantdefecto.Text = "";
                    srb_audiaprobado.IsChecked = false;
                    srb_audidesaprobado.IsChecked = false;
                    srb_audiaprobadoext.IsChecked = false;
                //}
                //DisplayAlert("Aviso", "Los datos se guardaron de manera satisfactoria.","OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Aviso", ex.StackTrace, "OK");
            }
        }
        #endregion

        #region LimpiarAuditoria
        private void LimpiaAuditoria()
        {
            srb_audiaprobado.IsChecked = false;
            srb_audiaprobadoext.IsChecked = false;
            srb_audidesaprobado.IsChecked = false;
            lbl_nsecue.Text = "";
            //ety_op.Text = "";
            lbl_descliente.Text = "";
            //pck_combo.SelectedItem = null;
            ety_lote.Text = "";
            ety_muestra.Text = "";
            ety_observ.Text = "";
            lbl_bloquedef.Text = "";
            lbl_fechadef.Text = "";
            pck_defectos.SelectedItem = null;
            ety_obsdefecto.Text = "";
            ety_cantdefecto.Text = "";
            if (listaCargaDefectos != null)
            {
                listaCargaDefectos.Clear();
                lsv_defectos.ItemsSource = listaCargaDefectos;
            }

            btn_buscarop.IsEnabled = false;
            ety_op.IsEnabled = false;
            //pck_combo.IsEnabled = false;
            ety_lote.IsEnabled = false;
            ety_muestra.IsEnabled = false;
            ety_observ.IsEnabled = false;
            pck_defectos.IsEnabled = false;
            ety_obsdefecto.IsEnabled = false;
            ety_cantdefecto.IsEnabled = false;
            btn_agregardefecto.IsEnabled = false;  
            btn_guardarauditoria.IsEnabled = false;
            btn_agregarauditoria.IsEnabled = true;      
            sf_check_op.IsEnabled = false;
            sfchipdcarub = "";
            sfchipop = "";
            sfchipcombo = "";            
        }
        #endregion

        #region ResumenAuditoria
        void ActualizaResumenAuditoria()
        {
            xqaprob = 0;
            xqdesap = 0;
            xqaprex = 0;
            var getresaudit = App.baseDatos.GetResaudit("FC", DateTime.Parse(dpk_fechaauditoria.Date.ToString()), lbl_bloquedef.Text.ToString().Trim());
            if (nuevaAuditoria == "S")
            {
                if (getresaudit != null)
                {
                    xqaprob = getresaudit.qaprob;
                    xqdesap = getresaudit.qdesap;
                    xqaprex = getresaudit.qaprex;

                    raudit00 resaudit = new raudit00
                    {
                        idraud = getresaudit.idraud,
                        careas = "FC",
                        faudit = DateTime.Parse(dpk_fechaauditoria.Date.ToString()),
                        clinea = lbl_bloquedef.Text.ToString().Trim(),
                        qaprob = xqaprob + Int32.Parse(sapaud == "A" ? "1" : "0"),
                        qdesap = xqdesap + Int32.Parse(sapaud == "D" ? "1" : "0"),
                        qaprex = xqaprex + Int32.Parse(sapaud == "E" ? "1" : "0"),
                    };
                    App.baseDatos.Update(resaudit);
                }
                else
                {
                    raudit00 resaudit = new raudit00
                    {
                        careas = "FC",
                        faudit = DateTime.Parse(dpk_fechaauditoria.Date.ToString()),
                        clinea = lbl_bloquedef.Text.ToString().Trim(),
                        qaprob = xqaprob + Int32.Parse(sapaud == "A" ? "1" : "0"),
                        qdesap = xqdesap + Int32.Parse(sapaud == "D" ? "1" : "0"),
                        qaprex = xqaprex + Int32.Parse(sapaud == "E" ? "1" : "0"),
                    };
                    App.baseDatos.Insert(resaudit);
                }
            }            
        }
        #endregion

        #region GuardaAuditoria
        private void AddAuditoria()
        {
            if (string.IsNullOrEmpty(sfchipop))
            {
                DisplayAlert("Aviso", "Debe seleccionar una OP", "OK");
                return;
            }

            if (string.IsNullOrEmpty(sfchipcombo))
            {
                DisplayAlert("Aviso", "Debe seleccionar el color del combo", "OK");
                return;
            }

            if (string.IsNullOrEmpty(ety_lote.Text))
            {
                DisplayAlert("Error", "Ingrese la cantidad del lote", "OK");
                ety_lote.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ety_muestra.Text))
            {
                DisplayAlert("Error", "Ingrese la cantidad de la muestra", "OK");
                ety_muestra.Focus();
                return;
            }
            
            Int32 qdefectos;
            sapaud = "";
            if (srb_audiaprobado.IsChecked == true) sapaud = "A";
            if (srb_audiaprobadoext.IsChecked == true) sapaud = "E";
            if (srb_audidesaprobado.IsChecked == true) sapaud = "D";

            if (string.IsNullOrEmpty(sapaud))
            {
                DisplayAlert("Error", "Seleccione el estado de auditoria", "OK");                
                return;
            }


            if (string.IsNullOrWhiteSpace(ety_observ.Text))
            {
                obsaudit = "";
            }
            else
            {
                obsaudit = ety_observ.Text;
            }

            try
            {
                if (nuevaAuditoria == "S")
                {
                        qdefectos = App.baseDatos.GetList<pdefec10>(false).Where(x => x.careas == "FC" && x.clinea == pck_bloque.SelectedItem.ToString() && x.nsecue == Int32.Parse(lbl_nsecue.Text.ToString()) && x.faudit.ToShortDateString() == dpk_fechaauditoria.Date.ToShortDateString()).Sum(x => x.qcanti);
                    paudit01 nauditoria = new paudit01
                    {
                            idaudi = xidaudi,
                            careas = "FC",
                            faudit = DateTime.Parse(dpk_fechaauditoria.Date.ToShortDateString()),
                            nsecue = Int32.Parse(lbl_nsecue.Text.ToString()),
                            clinea = pck_bloque.SelectedItem.ToString().Trim(),
                            ctpord = "OP",
                            nordpr = sfchipop,
                            nordct = "",//pck_corte.SelectedItem.ToString().Trim(),
                            cmarbe = "",
                            ccarub = sfchipcombo,//pck_combo.SelectedItem.ToString().Substring(0, pck_combo.SelectedItem.ToString().IndexOf("-") - 1),
                            npieza = 0,
                            cencog = "",
                            citems = "",
                            ccolor = "",
                            npanos = 0,
                            dtalla = "",
                            qtotal = 0,
                            dlotes = "",
                            ctraba = "",// ety_ctraba.Text.ToString(),
                            copera = "",//pck_operacion.SelectedItem.ToString().Substring(0, pck_operacion.SelectedItem.ToString().IndexOf("-") - 1),
                            cprove = "",
                            cmaqui = "",
                            cturno = "",
                            cparti = "",
                            nordco = "",
                            npacki = 0,
                            nlotes = Int32.Parse(ety_lote.Text.ToString()),
                            nmuest = Int32.Parse(ety_muestra.Text.ToString()),
                            pcierr = 0,
                            nrecup = 0,
                            nsegun = 0,
                            porcen = 0,
                            flgcie = saudtot,
                            ndefec = qdefectos,
                            status = sapaud,
                            dobser = obsaudit,//ety_observ.Text = null ?? "",
                            flgrau = estadoReauditoria,
                            nreaud = 0,
                            caudit = VariableGlobal.ctraba,
                            flgext = "N",
                            cliref = xcliref,
                            fauref = xfauref,
                            nseref = xnseref,
                            cusuar = VariableGlobal.cusuar,
                            fcreac = DateTime.Parse(DateTime.Now.ToShortDateString()),//DateTime.Parse("1900-01-01 00:00:00"),
                            fmodif = DateTime.Parse("1900-01-01 00:00:00"),
                            nordpo = 0,
                            fprogr = DateTime.Parse("1900-01-01 00:00:00"),
                            caudpr = "",
                            drefpr = "",
                            ctpaud = "PR",
                            csuppl = "",
                            flgenv = "",
                            sanula = "N",
                            ndesap = 0,
                            sautab = "S",
                            senvio = "N",
                        };
                        App.baseDatos.Insert(nauditoria);

                        taudit00 auditoria = new taudit00
                        {
                            idaudi = xidaudi,
                            careas = "FC",
                            faudit = DateTime.Parse(dpk_fechaauditoria.Date.ToShortDateString()),
                            nsecue = Int32.Parse(lbl_nsecue.Text.ToString()),
                            clinea = pck_bloque.SelectedItem.ToString().Trim(),
                            nordpr = sfchipop,
                            ccarub = sfchipcombo,//pck_combo.SelectedItem.ToString().Substring(0, pck_combo.SelectedItem.ToString().IndexOf("-") - 1).Trim(),
                            dcarub = sfchipdcarub,//pck_combo.SelectedItem.ToString().Substring(pck_combo.SelectedItem.ToString().IndexOf("-") + 1, pck_combo.SelectedItem.ToString().Length - (pck_combo.SelectedItem.ToString().IndexOf("-") + 1)).Trim(),
                            ctraba = "",// ety_ctraba.Text.ToString().Trim(),
                            copera = "",//pck_operacion.SelectedItem.ToString().Substring(0, pck_operacion.SelectedItem.ToString().IndexOf("-") - 1).Trim(),
                            dopera = "",//pck_operacion.SelectedItem.ToString().Substring(pck_operacion.SelectedItem.ToString().IndexOf("-") + 1, pck_operacion.SelectedItem.ToString().Length - (pck_operacion.SelectedItem.ToString().IndexOf("-") + 1)).Trim(),
                            dclien = lbl_descliente.Text.ToString().Trim(),
                            nlotes = Int32.Parse(ety_lote.Text.ToString()),
                            nmuest = Int32.Parse(ety_muestra.Text.ToString()),
                            status = sapaud,
                            dobser = obsaudit,//ety_observ.Text,
                            nordct = "",
                            npieza = 0,
                            dpieza = "",
                            clotei = "",
                            citems = "",
                            ditems = "",
                            cencog = "",
                            dtalla = "",
                            qprend = 0,
                            npanos = 0,
                            sreaud = "N",
                            ndefec = qdefectos,
                            nseref = 0,
                            cmaqui = "",
                            cturno = "",
                        };
                        App.baseDatos.Insert(auditoria);


                        if (estadoReauditoria == "S")
                        {
                            taudit00 rauditoria = new taudit00
                            {
                                idaudi = Int32.Parse(listaCargaAuditoria.ElementAt(0).idaudi.ToString()),
                                careas = listaCargaAuditoria.ElementAt(0).careas.ToString(),
                                faudit = DateTime.Parse(listaCargaAuditoria.ElementAt(0).faudit.ToShortDateString()),
                                nsecue = Int32.Parse(listaCargaAuditoria.ElementAt(0).nsecue.ToString()),
                                clinea = listaCargaAuditoria.ElementAt(0).clinea.ToString(),
                                nordpr = listaCargaAuditoria.ElementAt(0).nordpr.ToString(),
                                ccarub = listaCargaAuditoria.ElementAt(0).ccarub.ToString(),
                                dcarub = listaCargaAuditoria.ElementAt(0).dcarub.ToString(),
                                ctraba = listaCargaAuditoria.ElementAt(0).ctraba.ToString(),
                                copera = listaCargaAuditoria.ElementAt(0).copera.ToString(),
                                dopera = listaCargaAuditoria.ElementAt(0).dopera.ToString(),
                                dclien = listaCargaAuditoria.ElementAt(0).dclien.ToString(),
                                nlotes = Int32.Parse(listaCargaAuditoria.ElementAt(0).nlotes.ToString()),
                                nmuest = Int32.Parse(listaCargaAuditoria.ElementAt(0).nmuest.ToString()),
                                status = listaCargaAuditoria.ElementAt(0).status.ToString(),
                                dobser = listaCargaAuditoria.ElementAt(0).dobser.ToString(),
                                nordct = listaCargaAuditoria.ElementAt(0).nordct.ToString(),
                                npieza = Int32.Parse(listaCargaAuditoria.ElementAt(0).npieza.ToString()),
                                dpieza = listaCargaAuditoria.ElementAt(0).dpieza.ToString(),
                                clotei = listaCargaAuditoria.ElementAt(0).clotei.ToString(),
                                citems = listaCargaAuditoria.ElementAt(0).citems.ToString(),
                                ditems = listaCargaAuditoria.ElementAt(0).ditems.ToString(),
                                cencog = listaCargaAuditoria.ElementAt(0).cencog.ToString(),
                                dtalla = listaCargaAuditoria.ElementAt(0).dtalla.ToString(),
                                qprend = Int32.Parse(listaCargaAuditoria.ElementAt(0).qprend.ToString()),
                                npanos = Int32.Parse(listaCargaAuditoria.ElementAt(0).npanos.ToString()),
                                sreaud = "S",
                                ndefec = Int32.Parse(listaCargaAuditoria.ElementAt(0).ndefec.ToString()),
                            };
                            App.baseDatos.Update(rauditoria);
                        }

                        var ldefec = App.baseDatos.GetList<pdefec10>(false).Where(x => x.svigen == "N").ToList();
                        foreach (var record in ldefec)
                        {
                            pdefec01 ndefecto = new pdefec01
                            {
                                careas = record.careas,
                                faudit = record.faudit,
                                nsecue = record.nsecue,
                                clinea = record.clinea,
                                codigo = record.codigo,
                                coddef = record.coddef,
                                qcanti = record.qcanti,
                                dobser = record.dobser,
                                cgrupo = record.cgrupo,
                                cardef = record.cardef,
                                senvio = "N",

                            };
                            App.baseDatos.Insert(ndefecto);

                            pdefec10 cdefecto = new pdefec10
                            {
                                iddefe = record.iddefe,
                                careas = record.careas,
                                faudit = record.faudit,
                                nsecue = record.nsecue,
                                clinea = record.clinea,
                                codigo = record.codigo,
                                coddef = record.coddef,
                                qcanti = record.qcanti,
                                dobser = record.dobser,
                                cgrupo = record.cgrupo,
                                cardef = record.cardef,
                                descri = record.descri,
                                defjpg = record.defjpg,
                                vimage = record.vimage,
                                vphoto = record.vphoto,
                                svigen = "S",
                            };
                            App.baseDatos.Update(cdefecto);
                        }
                    //}
                }
                else
                {
                        qdefectos = App.baseDatos.GetList<pdefec10>(false).Where(x => x.careas == "FC" && x.clinea == pck_bloque.SelectedItem.ToString() && x.nsecue == Int32.Parse(lbl_nsecue.Text.ToString()) && x.faudit.ToShortDateString() == dpk_fechaauditoria.Date.ToShortDateString()).Sum(x => x.qcanti);
                        paudit01 nauditoria = new paudit01
                        {
                            idaudi = listaCargaAuditoria.ElementAt(0).idaudi,
                            careas = "FC",
                            faudit = DateTime.Parse(dpk_fechaauditoria.Date.ToShortDateString()),
                            nsecue = Int32.Parse(lbl_nsecue.Text.ToString()),
                            clinea = pck_bloque.SelectedItem.ToString().Trim(),
                            ctpord = "OP",
                            nordpr = sfchipop,
                            nordct = "",//pck_corte.SelectedItem.ToString().Trim(),
                            cmarbe = "",
                            ccarub = sfchipcombo,//pck_combo.SelectedItem.ToString().Substring(0, pck_combo.SelectedItem.ToString().IndexOf("-") - 1),
                            npieza = 0,
                            cencog = "",
                            citems = "",
                            ccolor = "",
                            npanos = 0,
                            dtalla = "",
                            qtotal = 0,
                            dlotes = "",
                            ctraba = "",//ety_ctraba.Text.ToString(),
                            copera = "",//pck_operacion.SelectedItem.ToString().Substring(0, pck_operacion.SelectedItem.ToString().IndexOf("-") - 1),
                            cprove = "",
                            cmaqui = "",
                            cturno = "",
                            cparti = "",
                            nordco = "",
                            npacki = 0,
                            nlotes = Int32.Parse(ety_lote.Text.ToString()),
                            nmuest = Int32.Parse(ety_muestra.Text.ToString()),
                            pcierr = 0,
                            nrecup = 0,
                            nsegun = 0,
                            porcen = 0,
                            flgcie = saudtot,
                            ndefec = qdefectos,
                            status = sapaud,
                            dobser = obsaudit,//ety_observ.Text.ToString(),
                            flgrau = "N",
                            nreaud = 0,
                            caudit = VariableGlobal.ctraba,
                            flgext = "N",
                            cliref = xcliref,
                            fauref = xfauref,
                            nseref = xnseref,
                            cusuar = VariableGlobal.cusuar,
                            fcreac = DateTime.Parse(dpk_fechaauditoria.Date.ToString()),
                            fmodif = DateTime.Parse(dpk_fechaauditoria.Date.ToString()),
                            nordpo = 0,
                            fprogr = DateTime.Parse(dpk_fechaauditoria.Date.ToString()),
                            caudpr = "",
                            drefpr = "",
                            ctpaud = "PR",
                            csuppl = "",
                            flgenv = "",
                            sanula = "N",
                            ndesap = 0,
                            sautab = "S",
                            senvio = "N",
                        };
                        App.baseDatos.Update(nauditoria);

                        taudit00 auditoria = new taudit00
                        {
                            idaudi = listaCargaAuditoria.ElementAt(0).idaudi,
                            careas = "FC",
                            faudit = DateTime.Parse(dpk_fechaauditoria.Date.ToShortDateString()),
                            nsecue = Int32.Parse(lbl_nsecue.Text.ToString()),
                            clinea = pck_bloque.SelectedItem.ToString().Trim(),
                            nordpr = sfchipop,
                            ccarub = sfchipcombo,//pck_combo.SelectedItem.ToString().Substring(0, pck_combo.SelectedItem.ToString().IndexOf("-") - 1).Trim(),
                            dcarub = sfchipdcarub,//pck_combo.SelectedItem.ToString().Substring(pck_combo.SelectedItem.ToString().IndexOf("-") + 1, pck_combo.SelectedItem.ToString().Length - (pck_combo.SelectedItem.ToString().IndexOf("-") + 1)).Trim(),
                            ctraba = "",//ety_ctraba.Text.ToString().Trim(),
                            copera = "",//pck_operacion.SelectedItem.ToString().Substring(0, pck_operacion.SelectedItem.ToString().IndexOf("-") - 1).Trim(),
                            dopera = "",//pck_operacion.SelectedItem.ToString().Substring(pck_operacion.SelectedItem.ToString().IndexOf("-") + 1, pck_operacion.SelectedItem.ToString().Length - (pck_operacion.SelectedItem.ToString().IndexOf("-") + 1)).Trim(),
                            dclien = lbl_descliente.Text.ToString().Trim(),
                            nlotes = Int32.Parse(ety_lote.Text.ToString()),
                            nmuest = Int32.Parse(ety_muestra.Text.ToString()),
                            status = sapaud,
                            dobser = obsaudit,//ety_observ.Text,
                            nordct = "",
                            npieza = 0,
                            dpieza = "",
                            clotei = "",
                            citems = "",
                            ditems = "",
                            cencog = "",
                            dtalla = "",
                            qprend = 0,
                            npanos = 0,
                            sreaud = listaCargaAuditoria.ElementAt(0).sreaud,
                            ndefec = qdefectos,
                        };
                        App.baseDatos.Update(auditoria);
                    //}
                }

                DisplayAlert("Aviso", "Los datos se guardaron de manera satisfactoria.", "OK");
                ActualizaResumenAuditoria();           
                LimpiaAuditoria();
                dealerDetails = new ObservableCollection<procos00>();
                sfchip_op.ItemsSource = dealerDetails;
                sfchip_combo.ItemsSource = dealerDetails;
                sgraud = "S";
            }
            catch (Exception ex)
            {
                DisplayAlert("Aviso", ex.StackTrace, "OK");
            }
        } 
        #endregion

        private void btn_agregardefecto_Clicked(object sender, EventArgs e)
        {
            AddDefecto(); 
        }

        private async void img_capturarfoto_Tapped(object sender, TappedEventArgs e)
        {
            var seldefecto = (e.Parameter) as pdefec10;

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Test",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Rear,
                Name = "FC"+dpk_fechaauditoria.Date.Year.ToString() + dpk_fechaauditoria.Date.Month.ToString() + dpk_fechaauditoria.Date.Day.ToString() + lbl_bloquedef.Text + lbl_nsecue.Text +  seldefecto.coddef.ToString() + ".jpg",
            });

            if (file == null)
                return;
            string desfot = file.Path.Substring(82, file.Path.Length - 82); //"FC" + dpk_fechaauditoria.Date.Year.ToString() + dpk_fechaauditoria.Date.Month.ToString() + dpk_fechaauditoria.Date.Day.ToString() + lbl_bloquedef.Text + lbl_nsecue.Text + seldefecto.coddef.ToString() + ".jpg"; //pck_defectos.SelectedItem.ToString().Substring(0, pck_defectos.SelectedItem.ToString().IndexOf("-") - 1) + ".jpg";

            try
            {
                //using (var data = new DataAccess())
                //{
                    pdefec10 cdefecto = new pdefec10
                    {
                        iddefe = seldefecto.iddefe,
                        careas = "FC",
                        faudit = DateTime.Parse(dpk_fechaauditoria.Date.ToString()),
                        nsecue = Int32.Parse(lbl_nsecue.Text),
                        clinea = pck_bloque.SelectedItem.ToString(),
                        codigo = seldefecto.codigo,
                        coddef = seldefecto.coddef,//pck_defectos.SelectedItem.ToString().Substring(0, 2),
                        qcanti = seldefecto.qcanti,
                        dobser = seldefecto.dobser,
                        cgrupo = seldefecto.cgrupo,
                        cardef = seldefecto.cardef,
                        descri = seldefecto.descri,//pck_defectos.SelectedItem.ToString().Substring(5, (pck_defectos.SelectedItem.ToString().Length - 5)),
                        defjpg = desfot.ToString(),
                        vimage = true,
                        vphoto = false,
                        svigen = seldefecto.svigen,
                    };
                    App.baseDatos.Update(cdefecto);

                    listaCargaDefectos = App.baseDatos.GetList<pdefec10>(false);
                    lsv_defectos.ItemsSource = listaCargaDefectos.Where(x => x.clinea == pck_bloque.SelectedItem.ToString() && x.nsecue == Int32.Parse(lbl_nsecue.Text.ToString()) && x.careas == "FC" && x.faudit.ToShortDateString() == dpk_fechaauditoria.Date.ToShortDateString());
                //}
                //await DisplayAlert("Aviso", "La imagen se guardo de manera satisfactoria.", "OK");
                //using (var data = new DataAccess())
                //{
                    //listaCargaDefectos = App.baseDatos.GetList<pdefec10>(false);
                    //lsv_defectos.ItemsSource = listaCargaDefectos.Where(x => x.clinea == pck_bloque.SelectedItem.ToString() && x.nsecue == Int32.Parse(lbl_nsecue.Text.ToString()) && x.careas == "FC");
                //}

                var result = await alertService.ShowMessage("Aviso", "Desea editar la imagen.");
                if (result == true)
                {
                    await App.Navigator.PushAsync(new ImageEditorPage(desfot));
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", ex.StackTrace, "OK");
            }
        }

        private void lsv_defectos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            int lvindex = listaCargaDefectos.IndexOf(e.SelectedItem as pdefec10);
        }

        private async void img_deletejpg_Tapped(object sender, TappedEventArgs e)
        {
            var result = await alertService.ShowMessage("Aviso", "Desea eliminar la imagen.");
            if (result == true)
            {
                var seldefecto = (e.Parameter) as pdefec10;
                string filename = seldefecto.defjpg;
                try
                {
                    //using (var data = new DataAccess())
                    //{
                        pdefec10 cdefecto = new pdefec10
                        {
                            iddefe = seldefecto.iddefe,
                            careas = "FC",
                            faudit = dpk_fechaauditoria.Date,
                            nsecue = seldefecto.nsecue,
                            clinea = pck_bloque.SelectedItem.ToString(),
                            codigo = seldefecto.codigo,
                            coddef = seldefecto.coddef,//pck_defectos.SelectedItem.ToString().Substring(0, 2),
                            qcanti = seldefecto.qcanti,
                            dobser = seldefecto.dobser,
                            cgrupo = seldefecto.cgrupo,
                            cardef = seldefecto.cardef,
                            descri = seldefecto.descri,//pck_defectos.SelectedItem.ToString().Substring(5, (pck_defectos.SelectedItem.ToString().Length - 5)),
                            defjpg = "",
                            vimage = false,
                            vphoto = true,
                            svigen = seldefecto.svigen,
                        };
                        App.baseDatos.Update(cdefecto);
                        
                        listaCargaDefectos = App.baseDatos.GetList<pdefec10>(false).Where(x => x.careas == seldefecto.careas && x.clinea == seldefecto.clinea && x.nsecue == seldefecto.nsecue && x.faudit == seldefecto.faudit).ToList();
                        lsv_defectos.ItemsSource = listaCargaDefectos;
                    //}         

                    IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync("/storage/emulated/0/Pictures/Test/");
                    ExistenceCheckResult folderexist = await rootFolder.CheckExistsAsync(filename);
                    if (folderexist == ExistenceCheckResult.FileExists)
                    {
                        DependencyService.Get<IFileManager>().DeleteFile("/storage/emulated/0/Pictures/test/" + filename);
                    }

                    await DisplayAlert("Aviso", "La imagen se elimino de manera satisfactoria.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Aviso", ex.StackTrace, "OK");
                }
            }
            else
            {
                return;
            }
        }

        private async void img_viewjpg_Tapped(object sender, TappedEventArgs e)
        {
            var seldefecto = (e.Parameter) as pdefec10;

            string filename = seldefecto.defjpg;
            IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync("/storage/emulated/0/Pictures/Test/");
            ExistenceCheckResult folderexist = await rootFolder.CheckExistsAsync(filename);
            if (folderexist == ExistenceCheckResult.FileExists)
            {
                DependencyService.Get<IFileManager>().OpenFile("/storage/emulated/0/Pictures/test/" + filename);
            }
        }

        private void btn_guardarauditoria_Clicked(object sender, EventArgs e)
        {
            AddAuditoria();
        }

        private void fab_nuevaauditoria_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Aviso", "Seleccionado", "OK");
        }


        //private async void PickPhoto_Clicked(object sender, EventArgs e)
        //{
        //    await CrossMedia.Current.Initialize();

        //    if (!CrossMedia.Current.IsPickPhotoSupported)
        //    {
        //        await DisplayAlert("No PickPhoto", ":( No PickPhoto available.", "OK");
        //        return;
        //    }

        //    _mediaFile = await CrossMedia.Current.PickPhotoAsync();

        //    if (_mediaFile == null)
        //        return;

        //    LocalPathLabel.Text = _mediaFile.Path;

        //    FileImage.Source = ImageSource.FromStream(() =>
        //    {
        //        return _mediaFile.GetStream();
        //    });
        //}

        //private async void TakePhoto_Clicked(object sender, EventArgs e)
        //{
        //    await CrossMedia.Current.Initialize();

        //    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
        //    {
        //        await DisplayAlert("No Camera", ":( No camera available.", "OK");
        //        return;
        //    }

        //    _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
        //    {
        //        Directory = "Sample",
        //        Name = "myImage.jpg"
        //    });

        //    if (_mediaFile == null)
        //        return;
        //    LocalPathLabel.Text = _mediaFile.Path;

        //    FileImage.Source = ImageSource.FromStream(() =>
        //    {
        //        return _mediaFile.GetStream();
        //    });
        //}

        //private async void UploadFile_Clicked(object sender, EventArgs e)
        //{

        //    using (var data = new DataAccess())
        //    {
        //        var ldefec = data.GetList<pdefec10>(false).Where(x => x.svigen == "S").ToList();
        //        foreach (var record in ldefec)
        //        {
        //            var content = new MultipartFormDataContent();
        //            var path = "/storage/emulated/0/Pictures/test/"+record.defjpg;

        //            content.Add(new ByteArrayContent(File.ReadAllBytes(path)), "file", record.defjpg);

        //            var httpClient = new HttpClient();

        //            var uploadServiceBaseAddress = "http://192.168.0.26:7030/api/Upload";

        //            var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);

        //            RemotePathLabel.Text = await httpResponseMessage.Content.ReadAsStringAsync();
        //        }
        //    }

        //}

        private void ety_lote_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(ety_lote.Text))
            {
                //using (var data = new DataAccess())
                //{
                    var xnivaql = App.baseDatos.GetList<procos00>(false).Where(x => x.nordpr == sfchipop).First();
                    var xlotes = App.baseDatos.GetList<ttcmue00>(false).Where(x => Int32.Parse(ety_lote.Text) >= x.ntanli && Int32.Parse(ety_lote.Text) <= x.ntanlf && x.nivaql.Trim()== xnivaql.nivaql);              
                    foreach (var record in xlotes)
                    {
                        ety_muestra.Text = record.ntanmu.ToString();
                        lbl_aql.Text = "Muestreo Normal AQL : " + xnivaql.nivaql;
                        lbl_aql_defectos.Text = "Acepta / Rechaza : "+record.caprob;
                    }
                //}

                if (cbxaudittot.IsChecked == true)
                {
                    ety_muestra.Text = ety_lote.Text;
                }
            }
        }

        private void ety_ctraba_Unfocused(object sender, FocusEventArgs e)
        {
            //BuscarOperario();
        }

        public string Valida_Aql()
        {
            int qdefectos = App.baseDatos.GetList<pdefec10>(false).Where(x => x.careas == "FC" && x.clinea == pck_bloque.SelectedItem.ToString() && x.nsecue == Int32.Parse(lbl_nsecue.Text.ToString()) && x.faudit.ToShortDateString() == dpk_fechaauditoria.Date.ToShortDateString()).Sum(x => x.qcanti);
            var xnivaql = App.baseDatos.GetList<procos00>(false).Where(x => x.nordpr == sfchipop).First();            
            var caprob = App.baseDatos.GetList<ttcmue00>(false).Where(x => x.nivaql.Trim() == xnivaql.nivaql.ToString().Trim() && Int32.Parse(ety_lote.Text) >= x.ntanli && Int32.Parse(ety_lote.Text) <= x.ntanlf).First();
            int qaprob = Int32.Parse(caprob.caprob.Replace("/", "0").Trim().Substring(caprob.caprob.Replace("/", "0").Trim().Length - 2, 2));

            if (qdefectos >= qaprob)
            {
                return "D";
            }
            else
            {
                return "A";
            }
        }

        private void Srb_audiaprobado_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(ety_lote.Text))
            {
                DisplayAlert("Error", "Ingrese la cantidad del lote", "OK");
                srb_audiaprobado.IsChecked = false;
                ety_lote.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ety_muestra.Text))
            {
                DisplayAlert("Error", "Ingrese la cantidad de la muestra", "OK");
                srb_audiaprobado.IsChecked = false;
                ety_muestra.Focus();
                return;
            }
            if (srb_audiaprobado.IsChecked==true)
            {
                if (Valida_Aql() == "D") {
                    DisplayAlert("Aviso", "No puede aprobar ya que la cantidad de defectos supera el permitido en el AQL.", "OK");
                    srb_audiaprobado.IsChecked = false;
                }                
            }            
        }

        private void Srb_audidesaprobado_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(ety_lote.Text))
            {
                DisplayAlert("Error", "Ingrese la cantidad del lote", "OK");
                srb_audidesaprobado.IsChecked = false;
                ety_lote.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ety_muestra.Text))
            {
                DisplayAlert("Error", "Ingrese la cantidad de la muestra", "OK");
                srb_audidesaprobado.IsChecked = false;
                ety_muestra.Focus();
                return;
            }
            if (srb_audidesaprobado.IsChecked == true)
            {
                if (Valida_Aql() == "A")
                {
                    DisplayAlert("Aviso", "No puede rechazar porque los defectos estan por debajo del permitido en el AQL.", "OK");
                    srb_audidesaprobado.IsChecked = false;
                }
            }
        }

        private void Sf_check_op_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            lbl_descliente.Text = "";
            ety_op.Text = "";
            sfchipop = "";
            if (e.IsChecked == true)
            {
                ety_op.IsEnabled = true;
                btn_buscarop.IsEnabled = true;
                sfchip_op.IsEnabled = false;
           
                var dealerDetails = new ObservableCollection<procos00>();                
                sfchip_op.ItemsSource = dealerDetails;
                sfchip_combo.ItemsSource = dealerDetails;

                ObservableCollection<procos00> dealerDetails01 = new ObservableCollection<procos00>();
                var progcos = App.baseDatos.GetList<procos00>(false).GroupBy(x => x.nordpr).Select(x => x);
                foreach (var recordf in progcos)
                {
                    var ord = new procos00()
                    {
                        nordpr = recordf.Key
                    };
                    dealerDetails01.Add(ord);
                }
                sfchip_op.ItemsSource = dealerDetails01;
            }
            else
            {
                ety_op.IsEnabled = false;
                btn_buscarop.IsEnabled = false;
                sfchip_op.IsEnabled = true;                                            
            }
        }

        private void Srb_audiaprobadoext_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(ety_lote.Text))
            {
                DisplayAlert("Error", "Ingrese la cantidad del lote", "OK");
                srb_audiaprobadoext.IsChecked = false;
                ety_lote.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ety_muestra.Text))
            {
                DisplayAlert("Error", "Ingrese la cantidad de la muestra", "OK");
                srb_audiaprobadoext.IsChecked = false;
                ety_muestra.Focus();
                return;
            }
            if (srb_audiaprobadoext.IsChecked == true)
            {
                if (string.IsNullOrWhiteSpace(ety_observ.Text))
                {
                    DisplayAlert("Aviso", "Ingrese un motivo válido en Observaciones para la aprobación extraordinaria", "OK");
                    srb_audiaprobadoext.IsChecked = false;
                }
            }
        }

        private void Pck_bloque_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;            
            selbloque= picker.SelectedItem.ToString();
        }

        private void Dpk_fechaauditoria_DateSelected(object sender, DateChangedEventArgs e)
        {
            var datosSecuencia = App.baseDatos.GetList<paudit01>(false).Where(a => a.careas == "FC" && a.faudit.ToShortDateString() == dpk_fechaauditoria.Date.ToShortDateString()).ToList();
            if (datosSecuencia.Count > 0)
            {
                var ultimaAuditoria = App.baseDatos.GetList<paudit01>(false).Where(a => a.careas == "FC" && a.faudit.ToShortDateString() == dpk_fechaauditoria.Date.ToShortDateString()).OrderByDescending(x => x.nsecue).First();
                lbl_nsecue.Text = (ultimaAuditoria.nsecue + 1).ToString();
            }
            else
            { lbl_nsecue.Text = "1"; }
        }

        private void Sfchip_combo_SelectionChanged(object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)
        {
            var chip = e.AddedItem as procos00;           
            sfchipcombo = chip.ccarub;
            sfchipdcarub = chip.dcarub;            
        }

        private void Sfchip_op_SelectionChanged(object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)
        {            
            var chip = e.AddedItem as procos00;           
            //DisplayAlert("Error", , "OK");

            ObservableCollection<procos00> listcombo = new ObservableCollection<procos00>();

            var descli = App.baseDatos.GetList<procos00>(false).Where(x => x.nordpr == chip.nordpr).First();
            lbl_descliente.Text = descli.dclien;
            sfchipop = chip.nordpr;
            //DisplayAlert("Error","", sfchipop);
            var progcos = App.baseDatos.GetList<procos00>(false).Where(x => x.nordpr == chip.nordpr).GroupBy(x => new { x.dcarub, x.ccarub, x.nordpr }).Select(y => new { dcarub = y.Key.dcarub, ccarub = y.Key.ccarub, nordpr = y.Key.nordpr });
            foreach (var recordf in progcos)
            {
                var ord = new procos00()
                {
                    dcarub = recordf.dcarub,
                    ccarub = recordf.ccarub
                };
                listcombo.Add(ord);
            }

            sfchip_combo.ItemsSource = listcombo;
            sfchipcombo = "";

            //*** Llena Picker Defectos ***//            
            var xdefec = App.baseDatos.GetList<mdefec10>(false).Where(x => x.caraud == "FC").OrderBy(a => a.coddef);

            if (xdefec == null)
            {
                DisplayAlert("Error", "No existen registros", "OK");
                return;
            }
            pck_defectos.DataSource = xdefec.OrderBy(x => x.coddef).ToList();

                lbl_bloquedef.Text = pck_bloque.SelectedItem.ToString();
                lbl_fechadef.Text = dpk_fechaauditoria.Date.ToString("dd - MMM - yyyy");
        }

        //private void Entry_Completed(object sender, EventArgs e)
        //{
        //    Entry entry = sender as Entry;
        //    if (entry != null && !string.IsNullOrEmpty(entry.Text))
        //    {
        //        dealerDetails.Add(new procos00() { nordpr = entry.Text });
        //        sfchip_op.ItemsSource = dealerDetails;
        //    }
        //}

        //private void ety_op_Unfocused(object sender, FocusEventArgs e)
        //{
        //    //BuscarOP();
        //}

        private void Cbxaudittot_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(ety_lote.Text))
            {
                DisplayAlert("Error", "Ingrese la cantidad del lote", "OK");
                ety_lote.Focus();
                return;
            }

            //using (var data = new DataAccess())
            //{
                var xlotes = App.baseDatos.GetList<ttcmue00>(false).Where(x => Int32.Parse(ety_lote.Text) >= x.ntanli && Int32.Parse(ety_lote.Text) <= x.ntanlf && x.nivaql.Trim() == "2.5");
                foreach (var record in xlotes)
                {
                    ety_muestra.Text = record.ntanmu.ToString();
                }
            //}

            if (cbxaudittot.IsChecked == true)
            {
                ety_muestra.Text = ety_lote.Text;
                saudtot = "S";
            }
            else
            {
                saudtot = "N";
            }

        }

        private void Pck_defectos_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            //var seledefecto = (e.Value) as mdefec00;
            var seledefecto = (e.Value) as mdefec10;
            xcoddef = seledefecto.coddef.ToString().Trim();
            xdedef = seledefecto.ddefec.ToString().Trim();
            xcodigodef=seledefecto.codigo.ToString().Trim();
        }
    }
}

