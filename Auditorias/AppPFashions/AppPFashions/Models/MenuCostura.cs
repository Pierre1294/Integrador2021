using AppPFashions.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AppPFashions.Models
{
    public class MenuCostura
    {
        #region Attributes
        private NavigationService navigationService;
        #endregion

        #region Properties  
        public string MenuCosturaName { get; set; }
        public string MenuCosturaImage { get; set; }
        public string MenuCosturaPage { get; set; }
        #endregion

        #region Constructors        
        public MenuCostura()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands 
        public ICommand NavigateCommandUser { get { return new RelayCommand(NavigateUser); } }

        private async void NavigateUser()
        {
            await navigationService.NavigateUser(MenuCosturaPage);
        }
        #endregion
    }
}
