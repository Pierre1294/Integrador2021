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
    public class padcor00Controller : ApiController
    {
        // GET api/padcor00
        public IEnumerable<padcor00> Get(string nordpr, string nordct)
        {
            IList<padcor00> getcorte = new List<padcor00>();
            DataSet ds = SqlComandos.EjecutarSel(@"select top 1 a.nordpr,a.nordct,b.ccarub,b.dcarub from padcor00 a
                                                    inner join pcoxes00 b on a.ccarub=b.ccarub and a.nordpr=b.nordpr
                                                    where a.nordpr='"+nordpr+"' and nordct='"+nordct+"'");

            padcor00 corte = null;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                corte = new padcor00();
                corte.nordpr = ds.Tables[0].Rows[i]["nordpr"].ToString();
                corte.nordct = ds.Tables[0].Rows[i]["nordct"].ToString();
                corte.ccarub = ds.Tables[0].Rows[i]["ccarub"].ToString();
                corte.dcarub = ds.Tables[0].Rows[i]["dcarub"].ToString();

                getcorte.Add(corte);
            }
            return getcorte;
        }
    }
}
