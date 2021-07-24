using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebApiPF.Models
{
    public class PlanDespacho
    {
        public string nordpr { get; set; }
        public string nordco { get; set; }
        public string dclien { get; set; }
        public int qitems { get; set; }
        public DateTime fembac { get; set; }
        public DateTime fembar { get; set; }
    }

    public class Items
    {
        public string nordpr { get; set; }
        public string nordco { get; set; }
        public string dclien { get; set; }
        public int qitems { get; set; }
        public DateTime fembac { get; set; }
        public DateTime fembar { get; set; }
    }


}