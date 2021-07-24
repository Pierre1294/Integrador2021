using AppWebApiPF.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppWebApiPF.Controllers
{
    public class ppzxes00Controller : ApiController
    {
        // GET api/ppzxes00
        public IEnumerable<ppzxes00> Get(string nordpr,string ccarub)
        {
            IList<ppzxes00> getpiezas = new List<ppzxes00>();
            DataSet ds = SqlComandos.EjecutarSel(@"SELECT ppzxes00.nordpr,   
                                                     ppzxes00.npieza,   
                                                     ppzxes00.dpieza,  
                                                     ppzxes00.cdixcl,
                                                     ppzxes00.citems
                                                FROM ppzxes00, pcoxes00, pccxes00, tcolor00  
                                               WHERE ( ppzxes00.ctpord = pcoxes00.ctpord ) and
                                                     ( ppzxes00.nordpr = pcoxes00.nordpr ) and
			                                            ( ppzxes00.nordpr = pccxes00.nordpr ) and
			                                            ( ppzxes00.cdixcl = pccxes00.cdixcl ) and
			                                            ( pcoxes00.ccarub = pccxes00.ccarub ) and
                                                     ( ppzxes00.ngrupo = pcoxes00.ngrupo ) and
			                                            ( pccxes00.ccolor = tcolor00.ccolor ) and
                                                     ( ppzxes00.ctpord = 'OP' ) AND  
                                                     ( ppzxes00.nordpr = '" + nordpr + "' ) AND ( pcoxes00.ccarub = '" + ccarub + "' ) and ( ppzxes00.nalter in (1,2))");

            ppzxes00 muestra = null;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                muestra = new ppzxes00();
                muestra.nordpr = ds.Tables[0].Rows[i]["nordpr"].ToString();
                muestra.npieza = ds.Tables[0].Rows[i]["npieza"].ToString();
                muestra.dpieza = ds.Tables[0].Rows[i]["dpieza"].ToString();
                muestra.cdixcl = ds.Tables[0].Rows[i]["cdixcl"].ToString();
                muestra.citems = ds.Tables[0].Rows[i]["citems"].ToString();

                getpiezas.Add(muestra);
            }
            return getpiezas;
        }
    }
}
