using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AppPFashions.Models
{
    public class caudit00 : INotifyPropertyChanged
    {
        #region Fields 
        [PrimaryKey, AutoIncrement]
        public Int32 idcaudit { get; set; }
        private string clinea;
        private bool isVisible = false;
        private Int32 qprime;
        private Int32 qapext;
        private Int32 qrecha;
        private string daudit;
        private string careas;


        #endregion

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                this.RaisedOnPropertyChanged("IsVisible");
            }
        }

        public string Clinea
        {
            get { return clinea; }
            set
            {
                if (clinea != value)
                {
                    clinea = value;
                    this.RaisedOnPropertyChanged("Clinea");
                }
            }
        }

        public Int32 Qprime
        {
            get { return qprime; }
            set
            {
                if (qprime != value)
                {
                    qprime = value;
                    this.RaisedOnPropertyChanged("Qprime");
                }
            }
        }

        public Int32 Qapext
        {
            get { return qapext; }
            set
            {
                if (qapext != value)
                {
                    qapext = value;
                    this.RaisedOnPropertyChanged("Qapext");
                }
            }
        }

        public Int32 Qrecha
        {
            get { return qrecha; }
            set
            {
                if (qrecha != value)
                {
                    qrecha = value;
                    this.RaisedOnPropertyChanged("Qrecha");
                }
            }
        }
        public string Daudit
        {
            get { return daudit; }
            set
            {
                if (daudit != value)
                {
                    daudit = value;
                    this.RaisedOnPropertyChanged("Daudit");
                }
            }
        }

        public string Careas
        {
            get { return careas; }
            set
            {
                if (careas != value)
                {
                    careas = value;
                    this.RaisedOnPropertyChanged("Careas");
                }
            }
        }

        #region Interface Member

        public caudit00()
        {

        }

        public caudit00(string Clinea)
        {
            clinea = Clinea;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisedOnPropertyChanged(string _PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_PropertyName));
            }
        }

        #endregion
    }
}
