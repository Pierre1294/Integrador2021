using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppPFashions.Models
{
    public class paudob00
    {
        [PrimaryKey, AutoIncrement]
        public int idobse  { get; set; }
        public DateTime fregis { get; set; }
        public string clinea { get; set; }
        public string dobser { get; set; }
        public string cusuar { get; set; }
        public string senvio { get; set; }
    }
}
