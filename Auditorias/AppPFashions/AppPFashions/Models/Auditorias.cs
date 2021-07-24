using AppPFashions.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppPFashions.Models
{
    public class Auditorias 
    {        

        #region Attributes
        private NavigationService navigationService;
        #endregion

        #region Properties  
        public string AuditoriaName { get; set; }
        public string AuditoriaImage { get; set; }
        public string AuditoriaPage { get; set; }
        #endregion

        #region Constructors        
        public Auditorias()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands 
        public ICommand NavigateCommandUser { get { return new RelayCommand(NavigateUser); } }

        private async void NavigateUser()
        {
            await navigationService.NavigateUser(AuditoriaPage);
        }
        #endregion

    }
}
