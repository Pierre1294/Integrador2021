using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppPFashions.Models
{
    public class raudit00
    {
        [PrimaryKey, AutoIncrement]
        public int idraud { get; set; }
        public string careas { get; set; }
        public DateTime faudit { get; set; }
        public string clinea { get; set; }
        public int qaprob { get; set; }
        public int qdesap { get; set; }
        public int qaprex { get; set; }
        public int qprapr { get; set; }
    }
}
