using AppPFashions.Data;
using AppPFashions.Models;
using AppPFashions.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Input;

namespace AppPFashions.ViewModels
{
    public class MenuItemViewModel
    {
        #region Attributes
        private DialogService dialogService;
        private DataAccess dataAccess;
        private NavigationService navigationService;
        #endregion

        #region Properties  
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        #endregion

        #region Constructors        
        public MenuItemViewModel()
        {
            dialogService = new DialogService();
            dataAccess = new DataAccess();
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands 
        public ICommand NavigateCommand { get { return new RelayCommand(Navigate); } }
        public ICommand NavigateCommandUser { get { return new RelayCommand(NavigateUser); } }       

        private async void Navigate()
        {
            await navigationService.Navigate(PageName);
        }

        private async void NavigateUser()
        {                        
            await navigationService.NavigateUser(PageName);
        }

        #endregion
    }


}
