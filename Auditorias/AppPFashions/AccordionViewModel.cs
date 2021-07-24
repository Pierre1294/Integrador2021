using AppPFashions.Data;
using AppPFashions.Models;
using AppPFashions.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace AppPFashions
{
    public class AccordionViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        List<caudit00> xoperac;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        #region Properties

        ObservableCollection<caudit00> _ContactsInfo;

        public ObservableCollection<caudit00> ContactsInfo
        {
            get
            {
                return _ContactsInfo;
            }
            set
            {
                _ContactsInfo = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructor

        public AccordionViewModel()
        {
            using (var data = new DataAccess())
            {
                xoperac = data.GetList<caudit00>(false);
            }
                ContactsInfo = new ObservableCollection<caudit00>(xoperac);
        }

        #endregion

        //#region Interface Member

        //public event PropertyChangedEventHandler PropertyChanged;

        //public void OnPropertyChanged(string name)
        //{
        //    if (this.PropertyChanged != null)
        //        this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        //}

        //#endregion
    }
}
