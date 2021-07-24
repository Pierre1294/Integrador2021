using AppPFashions.Data;
using AppPFashions.Interfaces;
using AppPFashions.Models;
using AppPFashions.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

// SERVICIO DE NAVEGACION
namespace AppPFashions.Services
{
    public class NavigationService
    {
        #region Attributes
        private DataService dataService;
        private DialogService dialogService;
        private DataAccess dataAccess;
        string xccargo = "";
        string xcfunci = "";
        string xcusuar = "";
        Usuario user =new Usuario();        
        #endregion

        public NavigationService()
        {
            dialogService = new DialogService();
            dataAccess = new DataAccess();
            dataService = new DataService();
        }

        public async Task Navigate(string pageName)
        {
            App.Master.IsPresented = false;   
            switch (pageName)
            {
                case "RecordsPage":
                    await App.Navigator.PushAsync(new RecordsPage());
                    break;
                case "LoginPage":
                    await App.Navigator.PushAsync(new LoginPage());
                    break;
                case "UserPage":
                    await App.Navigator.PushAsync(new UserPage());
                    break;
                case "LogoutPage":
                    Logout();
                    break;
            }
        }

        public async Task NavigateUser(string pageName)
        {
            App.UserMaster.IsPresented = false;
            switch (pageName)
            {
                case "UserPage":
                    await App.Navigator.PushAsync(new UserPage());
                    break;
                case "QualityPage":
                    await App.Navigator.PushAsync(new QualityPage());
                    break;
                case "SewingPage":
                    user = App.baseDatos.GetUsuario();
                    xccargo = user.ccargo;
                    xcfunci = user.cfunci;

                    if (xccargo == "06" || xccargo == "07" || xccargo == "08" || xcfunci == "99" || xccargo == "43")
                    {
                        await App.Navigator.PushAsync(new MenuCosturaPage());
                    }          
                    else
                    {
                        await dialogService.ShowMessage("Aviso", "No tiene acceso a la opción seleccionada");
                        return;
                    }
                    //await App.Navigator.PushAsync(new SewingPage());
                    //await App.Navigator.PushAsync(new EficienciaBihorarioPage());
                    
                    break;             
                case "LogoutPage":
                    Logout();
                    break;
                case "AuditoriaCortePage":
                    //using (var data = new DataAccess())
                    //{
                        user = App.baseDatos.GetUsuario();
                        xccargo = user.ccargo;
                        xcfunci = user.cfunci;                 
                        
                        if (xccargo == "05" || xcfunci == "99")
                        {
                            await App.Navigator.PushAsync(new ResumenAuditoriasPage("16"));                            
                        }     
                        else
                        {
                            await dialogService.ShowMessage("Aviso", "No tiene acceso a la auditoria seleccionada");
                            return;
                        }
                    //}                    
                    break;

                    case "CosturaProcesoPage":
                    //using (var data = new DataAccess())
                    //{
                        user = App.baseDatos.GetUsuario();
                        xccargo = user.ccargo;
                        xcfunci = user.cfunci;
                        if (xccargo == "34" || xccargo == "06" || xccargo == "53" || xcfunci == "99" || xccargo == "43")
                        {                            
                            await App.Navigator.PushAsync(new ResumenAuditoriasPage("19"));                            
                        }
                        else
                        {
                            await dialogService.ShowMessage("Aviso", "No tiene acceso a la auditoria seleccionada");
                            return;
                        }
                    //}                    
                    break;

                    case "CosturaFinalPage":                    
                    //using (var data = new DataAccess())
                    //{
                        user = App.baseDatos.GetUsuario();
                        xccargo = user.ccargo;
                        xcfunci = user.cfunci;
                        if (xccargo == "34" || xccargo == "06"  || xccargo == "53" || xcfunci == "99" || xccargo == "43")
                        {
                            await App.Navigator.PushAsync(new ResumenAuditoriasPage("FC"));
                        }
                        else
                        {
                            await dialogService.ShowMessage("Aviso", "No tiene acceso a la auditoria seleccionada");
                            return;
                        }
                    //}                    
                    break;   
            }
        }

        //public async Task NavigateCosturaUser(string pageName)
        //{
        //    App.UserCosturaMaster.IsPresented = false;
        //    switch (pageName)
        //    {
        //        case "EficienciaBihorarioPage":
        //            await App.Navigator.PushAsync(new EficienciaBihorarioPage());
        //            break;
        //        case "EficienciaSemanalPage":
        //            await App.Navigator.PushAsync(new QualityPage());
        //            break;
        //    }
        //}

        async void Acceso()
        {
            using (var data = new DataAccess())
            {
                var user = data.GetUsuario();
                string xccargo = user.ccargo;
                if (xccargo != "34" || xccargo != "99")
                {
                    await dialogService.ShowMessage("Aviso", "No tiene acceso a la auditoria seleccionada");
                    return;
                }
            }
        }

        private void Logout()
        {
            App.CurrentUser.IsRemembered = false;
            dataService.UpdateUser(App.CurrentUser);
            App.Current.MainPage = new MasterPage();
        }

        internal void SetMainPage(Usuario user)
        {
            App.CurrentUser = user;
            App.Current.MainPage = new MasterUserPage();
        }
    }
}
