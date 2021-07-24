using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppPFashions.Models
{
    public class paudit01
    {
        [PrimaryKey]
        public int idaudi { get; set; }        
        public string careas { get; set; }        
        public DateTime faudit { get; set; }        
        public Int32 nsecue { get; set; }        
        public string clinea { get; set; }
        public string ctpord { get; set; }
        public string nordpr { get; set; }
        public string nordct { get; set; }
        public string cmarbe { get; set; }
        public string ccarub { get; set; }
        public Int32 npieza { get; set; }
        public string cencog { get; set; }
        public string citems { get; set; }
        public string ccolor { get; set; }
        public Int32 npanos { get; set; }
        public string dtalla { get; set; } 
        public Int32 qtotal { get; set; }
        public string dlotes { get; set; }
        public string ctraba { get; set; }
        public string copera { get; set; }
        public string cprove { get; set; }
        public string cmaqui { get; set; }
        public string cturno { get; set; }
        public string cparti { get; set; }
        public string nordco { get; set; }
        public Int32 npacki { get; set; }
        public Int32 nlotes { get; set; }
        public Int32 nmuest { get; set; }
        public Int32 pcierr { get; set; }
        public Int32 nrecup { get; set; }
        public Int32 nsegun { get; set; }
        public decimal porcen { get; set; }
        public string flgcie { get; set; }
        public Int32 ndefec { get; set; }
        public string status { get; set; }
        public string dobser { get; set; }
        public string flgrau { get; set; }
        public Int32 nreaud { get; set; }
        public string caudit { get; set; }
        public string flgext { get; set; }
        public string cliref { get; set; }        
        public DateTime fauref { get; set; }
        public Int32 nseref { get; set; }
        public string cusuar { get; set; }
        public DateTime fcreac { get; set; }
        public DateTime fmodif { get; set; }
        public Int32 nordpo { get; set; }
        public DateTime fprogr { get; set; }
        public string caudpr { get; set; }
        public string drefpr { get; set; }
        public string ctpaud { get; set; }
        public string csuppl { get; set; }
        public string flgenv { get; set; }
        public string sanula { get; set; }
        public Int32 ndesap { get; set; }
        public string sautab { get; set; }
        public string senvio { get; set; }
    }
}
