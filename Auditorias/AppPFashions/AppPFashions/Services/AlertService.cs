using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppPFashions.Services
{
    public class AlertService
    {
        public async Task<bool> ShowMessage(string title, string message)
        {            
          var result = await App.Current.MainPage.DisplayAlert(title, message, "Aceptar","Cancelar");
          return result;
        }
    }
}
