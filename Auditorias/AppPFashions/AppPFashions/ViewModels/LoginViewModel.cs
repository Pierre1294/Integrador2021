using AppPFashions.Interfaces;
using AppPFashions.Models;
using AppPFashions.Services;
using GalaSoft.MvvmLight.Command;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppPFashions.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Attributes
        IDownloader downloader = DependencyService.Get<IDownloader>();
        private NavigationService navigationService;
        private DialogService dialogService;
        private ApiService apiService;
        private DataService dataService;
        private bool isRunning;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged; 
        #endregion

   

        #region Properties
        public string User { get; set; }
        public string Password { get; set; }
        public bool IsRemembered { get; set; }
        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
            apiService = new ApiService();
            dataService = new DataService();
            IsRemembered = true;
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(User))
            {
                await dialogService.ShowMessage("Error", "Ingrese un usuario.");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "Ingrese una contraseña.");
                return;
            }

            IsRunning = true;
            var response = await apiService.Login(User, Password);
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }  

            var user = (Usuario)response.Result;
            user.IsRemembered = IsRemembered;
            user.Password = Password;
            VariableGlobal.ctraba = user.ctraba;
            VariableGlobal.ccargo = user.ccargo;
            VariableGlobal.cusuar = user.cusuar;

            User = "";
            Password = "";

            string xctraba = "";
            if (Int32.Parse(user.ctraba.Substring(1, 5)) > 10000)
            {
                xctraba = user.ctraba;
            }
            else
            {
                xctraba = user.ctraba.Substring(0, 1) + user.ctraba.Substring(2, 4);
            }

            //DependencyService.Get<IDownloader>().Show("Descargando");
            ////********** INICIO DESCARGA DE ARCHIVOS DESDE FTP **********//            

            //IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync("/storage/emulated/0/Fotos/");
            //ExistenceCheckResult folderexist = await rootFolder.CheckExistsAsync(xctraba);
            //if (folderexist == ExistenceCheckResult.FileExists)
            //{
            //    IFile file = await rootFolder.GetFileAsync(xctraba + ".bmp");
            //    await file.DeleteAsync();
            //}

            //string rutapdf = "ftp://192.168.2.55:22/" + xctraba + ".bmp";
            //downloader.DownloadFile(rutapdf, "Fotos");

            //DependencyService.Get<IDownloader>().Hide();

            dataService.InsertUser(user);

            navigationService.SetMainPage(user);
        }

        #endregion
    }
}
