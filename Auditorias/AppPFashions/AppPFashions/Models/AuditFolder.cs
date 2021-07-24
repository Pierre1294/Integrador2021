using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace AppPFashions.Models
{
    [Preserve(AllMembers = true)]
    public class AuditFolder : INotifyPropertyChanged
    {
        #region Feilds

        private string folderName;
        private string imageName;
        private int auditCount;
        private int fontBold;        
        private ObservableCollection<SubFolder> subFolders;

        #endregion

        #region Constructor
        public AuditFolder()
        {
        }

        #endregion

        #region Properties
        public ObservableCollection<SubFolder> SubFolder
        {
            get { return subFolders; }
            set
            {
                subFolders = value;
                RaisedOnPropertyChanged("SubFolder");
            }
        }

        public string FolderName
        {
            get { return folderName; }
            set
            {
                folderName = value;
                RaisedOnPropertyChanged("FolderName");
            }
        }

        public string ImageName
        {
            get { return imageName; }
            set
            {
                imageName = value;
                RaisedOnPropertyChanged("ImageName");
            }
        }

        public int AuditCount
        {
            get { return auditCount; }
            set
            {
                auditCount = value;
                RaisedOnPropertyChanged("AuditCount");
            }
        }
        public int FontBold
        {
            get { return fontBold; }
            set
            {
                fontBold = value;
                RaisedOnPropertyChanged("FontBold");
            }
        }

        #endregion

        #region INotifyPropertyChanged

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

    [Preserve(AllMembers = true)]
    public class SubFolder : INotifyPropertyChanged
    {
        #region Feilds

        private string folderName;
        private string imageName;
        private int auditCount;
        private string subFolderName;
        private string subimageName;
        private string clinea;
        private DateTime faudit;
        private string careas;
        private int nsecue;
        private ObservableCollection<DetFolder> detFolders;

        #endregion

        #region Constructor
        public SubFolder()
        {
        }

        #endregion

        #region Properties
        public ObservableCollection<DetFolder> DetFolder
        {
            get { return detFolders; }
            set
            {
                detFolders = value;
                RaisedOnPropertyChanged("DetFolder");
            }
        }
        public string FolderName
        {
            get { return folderName; }
            set
            {
                folderName = value;
                RaisedOnPropertyChanged("FolderName");
            }
        }

        public string ImageName
        {
            get { return imageName; }
            set
            {
                imageName = value;
                RaisedOnPropertyChanged("ImageName");
            }
        }

        public int AuditCount
        {
            get { return auditCount; }
            set
            {
                auditCount = value;
                RaisedOnPropertyChanged("AuditCount");
            }
        }

        public string SubFolderName
        {
            get { return subFolderName; }
            set
            {
                subFolderName = value;
                RaisedOnPropertyChanged("SubFolderName");
            }
        }

        public string SubImageName
        {
            get { return subimageName; }
            set
            {
                subimageName = value;
                RaisedOnPropertyChanged("SubImageName");
            }
        }

        public string Clinea
        {
            get { return clinea; }
            set
            {
                clinea = value;
                RaisedOnPropertyChanged("Clinea");
            }
        }
        public DateTime Faudit
        {
            get { return faudit; }
            set
            {
                faudit = value;
                RaisedOnPropertyChanged("Faudit");
            }
        }
        public int Nsecue
        {
            get { return nsecue; }
            set
            {
                nsecue = value;
                RaisedOnPropertyChanged("Nsecue");
            }
        }
        public string Careas
        {
            get { return careas; }
            set
            {
                careas = value;
                RaisedOnPropertyChanged("Careas");
            }
        }

        #endregion

        #region INotifyPropertyChanged

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

    [Preserve(AllMembers = true)]
    public class DetFolder : INotifyPropertyChanged
    {
        #region Feilds

        private string folderName;
        private string imageName;
        private int auditCount;
        private string subimageName;
        private string careas;
        private string status;
        private string clinea;
        private string faudit;

        #endregion

        #region Constructor
        public DetFolder()
        {
        }

        #endregion

        #region Properties

        public string FolderName
        {
            get { return folderName; }
            set
            {
                folderName = value;
                RaisedOnPropertyChanged("FolderName");
            }
        }

        public string ImageName
        {
            get { return imageName; }
            set
            {
                imageName = value;
                RaisedOnPropertyChanged("ImageName");
            }
        }

        public int AuditCount
        {
            get { return auditCount; }
            set
            {
                auditCount = value;
                RaisedOnPropertyChanged("AuditCount");
            }
        }
        public string Careas
        {
            get { return careas; }
            set
            {
                careas = value;
                RaisedOnPropertyChanged("Careas");
            }
        }
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                RaisedOnPropertyChanged("Status");
            }
        }
        public string Clinea
        {
            get { return clinea; }
            set
            {
                clinea = value;
                RaisedOnPropertyChanged("Clinea");
            }
        }
        public string Faudit
        {
            get { return faudit; }
            set
            {
                faudit = value;
                RaisedOnPropertyChanged("Faudit");
            }
        }
        public string SubImageName
        {
            get { return subimageName; }
            set
            {
                subimageName = value;
                RaisedOnPropertyChanged("SubImageName");
            }
        }

        #endregion

        #region INotifyPropertyChanged

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
