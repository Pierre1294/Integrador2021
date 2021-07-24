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
    public class AuditoriaCorteController : ApiController
    {
        [HttpGet]
        [Route("api/GetFileCorte")]
        public IEnumerable<AuditoriaCorte> Get(string nordpr)
        {
            IList<AuditoriaCorte> getcorte = new List<AuditoriaCorte>();
            DataSet ds = SqlComandos.EjecutarSel(@"select a.nordpr,a.nordct,a.ccarub,f.dcarub,a.nsecue,a.faudit,c.drzsoc as dclien,a.nlotes,a.nmuest,a.clinea,a.status
                                                    from paudit01 a
                                                    left join pdgorp00 b on  a.nordpr=b.nordpr
                                                    left join mclien00 c on b.cclien=c.cclien
                                                    left join pcoxes00 f on a.ccarub=f.ccarub and a.nordpr=f.nordpr
                                                    where a.careas='16' and a.nordpr='"+ nordpr + "' order by a.nordct,a.ccarub,a.faudit desc");

            AuditoriaCorte muestra = null;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                muestra = new AuditoriaCorte();
                muestra.nordpr = ds.Tables[0].Rows[i]["nordpr"].ToString().Trim();
                muestra.nordct = ds.Tables[0].Rows[i]["nordct"].ToString().Trim();
                muestra.ccarub = ds.Tables[0].Rows[i]["ccarub"].ToString().Trim();
                muestra.dcarub = ds.Tables[0].Rows[i]["dcarub"].ToString().Trim();
                muestra.nsecue = Int32.Parse(ds.Tables[0].Rows[i]["nsecue"].ToString());
                muestra.faudit = DateTime.Parse(ds.Tables[0].Rows[i]["faudit"].ToString().Trim());
                muestra.dclien = ds.Tables[0].Rows[i]["dclien"].ToString().Trim();
                muestra.nlotes = Int32.Parse(ds.Tables[0].Rows[i]["nlotes"].ToString());
                muestra.nmuest = Int32.Parse(ds.Tables[0].Rows[i]["nmuest"].ToString());
                muestra.clinea = ds.Tables[0].Rows[i]["clinea"].ToString();
                muestra.status = ds.Tables[0].Rows[i]["status"].ToString();

                getcorte.Add(muestra);
            }
            return getcorte;
        }
    }
}
