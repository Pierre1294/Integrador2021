    using System;
    using System.IO;

    namespace AppPFashions.Droid.Helpers
    {
        public class FileHelper
        {
            public static string ObtenerRutaLocal(string archivo)
            {
                //string ruta = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string ruta="/storage/emulated/0/DB";
            return Path.Combine(ruta, archivo);
            }
        }
    }