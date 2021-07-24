using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebApiPF.Models
{
    public class EficienciaBihorario
    {
        public string ctraba { get; set; }
        public string dtraba { get; set; }
        public string clinea { get; set; }
        public DateTime fproce { get; set; }
        public Int32 qmindi01 { get; set; }
        public Int32 qmindi02 { get; set; }
        public Int32 qmindi03 { get; set; }
        public Int32 qmindi04 { get; set; }
        public Int32 qmindi05 { get; set; }
        public Int32 qmindi06 { get; set; }
        public Int32 pefici01 { get; set; }
        public Int32 pefici02 { get; set; }
        public Int32 pefici03 { get; set; }
        public Int32 pefici04 { get; set; }
        public Int32 pefici05 { get; set; }
        public Int32 pefici06 { get; set; }
        public Int32 tqmindi { get; set; }
        public Int32 tpefici { get; set; }
    }
}