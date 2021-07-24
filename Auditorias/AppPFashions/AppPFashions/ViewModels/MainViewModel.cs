using AppPFashions.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;


// CLASE CONTENEDORA DEL MENU LATERAL

namespace AppPFashions.ViewModels
{
    public class MainViewModel
    {
        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public ObservableCollection<MenuItemViewModel> MenuUser { get; set; }
        public ObservableCollection<Auditorias> ListaAuditorias { get; set; }
        public ObservableCollection<MenuCostura> ListaMenuCostura { get; set; }
        public LoginViewModel NewLogin { get; set; }                
        #endregion

        #region Constructors
        public MainViewModel()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();
            MenuUser = new ObservableCollection<MenuItemViewModel>();
            ListaAuditorias = new ObservableCollection<Auditorias>();
            ListaMenuCostura = new ObservableCollection<MenuCostura>();
            NewLogin = new LoginViewModel();            
            //CosProVieMod = new CosturaProcesoViewModel();
            LoadMenu();
            LoadMenuUser();
            LoadAuditorias();
            
        }
        #endregion



        #region Methods
        private void LoadMenu()
        {
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_home_pf.png",
                PageName = "HomePage",
                Title = "Página principal",  
                
            });
 
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_user.png",
                PageName = "LoginPage",
                Title = "Cuenta",

            });
        }

        private void LoadMenuUser()
        {
            MenuUser.Add(new MenuItemViewModel
            {
                Icon = "ic_home_pf.png",
                PageName = "UserPage",
                Title = "Página principal",

            });

            MenuUser.Add(new MenuItemViewModel
            {
                Icon = "ic_ccalidad.png",
                PageName = "QualityPage",
                Title = "Control de calidad",

            });

            MenuUser.Add(new MenuItemViewModel
            {
                Icon = "ic_csesion.png",
                PageName = "LogoutPage",
                Title = "Cerrar Sesión",

            });

        }

        private void LoadAuditorias()
        {
            ListaAuditorias.Add(new Auditorias
            {
                AuditoriaImage = "costuraf.png",
                AuditoriaName = "Costura Final",
                AuditoriaPage = "CosturaFinalPage",

            });     
        }
  
            #endregion
        }
}
