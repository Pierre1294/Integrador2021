using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebApiPF.Models
{
    public class AuditoriaCorte
    {
        public string nordpr { get; set; }
        public string nordct { get; set; }
        public string ccarub { get; set; }
        public string dcarub { get; set; }
        public int nsecue { get; set; }
        public DateTime faudit { get; set; }
        public string dclien { get; set; }
        public int nlotes { get; set; }
        public int nmuest { get; set; }
        public string clinea { get; set; }
        public string status { get; set; }
    }
}