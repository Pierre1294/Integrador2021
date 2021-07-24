using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebApiPF.Models
{
    public class ResumenDespacho
    {
        public string dclien { get; set; }
        public Int32 cant_ops { get; set; }
        public Int32 cant_ci { get; set; }
        public Int32 cant_despac { get; set; }
        public Int32 qdespa { get; set; }
        public Int32 qprdas { get; set; }
        public Decimal iprecio { get; set; }
        public Decimal margen { get; set; }
        public Decimal margen_neto { get; set; }
        public Decimal pprseg { get; set; }
        public Decimal segundas { get; set; }
        public Decimal qmixpr { get; set; }
        public Decimal qporde { get; set; }
        public Decimal qpropd { get; set; }
    }
}