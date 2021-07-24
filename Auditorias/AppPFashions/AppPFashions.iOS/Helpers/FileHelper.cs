using System;
using System.IO;

namespace AppPFashions.iOS.Helpers
{
    public class FileHelper
    {
        public static string ObtenerRutaLocal(string archivo)
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libreria = Path.Combine(folder, "..", "Library");

            if (!Directory.Exists(libreria))
                Directory.CreateDirectory(libreria);

            return Path.Combine(libreria, archivo);
        }
    }
}