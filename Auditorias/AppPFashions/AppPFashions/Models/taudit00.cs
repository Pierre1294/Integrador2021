
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppPFashions.Models
{
    public class taudit00
    {
        [PrimaryKey]
        public int idaudi { get; set; }
        public string careas { get; set; } 
        public DateTime faudit { get; set; }   
        public Int32 nsecue { get; set; }
        public string clinea { get; set; }
        public string nordpr { get; set; }
        public string ccarub { get; set; }
        public string dcarub { get; set; }
        public string ctraba { get; set; }
        public string copera { get; set; }
        public string dopera { get; set; }
        public string dclien { get; set; }
        public Int32 nlotes { get; set; }
        public Int32 nmuest { get; set; }
        public string status { get; set; }
        public string dobser { get; set; }
        public string smodif { get; set; }
        public string nordct { get; set; }
        public Int32 npieza { get; set; }
        public string dpieza { get; set; }
        public string clotei { get; set; }
        public string citems { get; set; }
        public string ditems { get; set; }
        public string cencog { get; set; }
        public string dtalla { get; set; }
        public Int32 qprend { get; set; }
        public Int32 npanos { get; set; }
        public string sreaud { get; set; }
        public Int32 ndefec { get; set; }
        public Int32 nseref { get; set; }
        public string cmaqui { get; set; }
        public string cturno { get; set; }
    }
}
