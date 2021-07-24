using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppPFashions.Models
{
    public class pdefec01
    {
        [PrimaryKey, AutoIncrement]
        public Int32 iddefe { get; set; }
        public string careas { get; set; }
        public DateTime faudit { get; set; }
        public Int32 nsecue { get; set; }
        public string clinea { get; set; }
        public string codigo { get; set; }
        public string coddef { get; set; }
        public Int32 qcanti { get; set; }
        public string dobser { get; set; }
        public string cgrupo { get; set; }
        public string cardef { get; set; }
        public string senvio { get; set; }
        public string imgdef { get; set; }
    }
}
