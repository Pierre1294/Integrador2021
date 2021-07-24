using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppPFashions.Models
{
    public class pdefec10
    {
        [PrimaryKey, AutoIncrement]
        public int iddefe { get; set; }
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
        public string descri { get; set; }
        public string defjpg { get; set; }
        public bool vimage { get; set; }
        public bool vphoto { get; set; }
        public string svigen { get; set; }
    }
}
