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
    public class paudit02Controller : ApiController
    {
        // GET api/paudit02
        public paudit02 Get(string careas, string faudit, string clinea)
        {
            paudit02 ultreg = new paudit02();
            DataSet ds = SqlComandos.EjecutarSel(@"select clinea,isnull(max(nsecue),0) as utlreg from paudit01 a where careas='" + careas + "' and faudit='" + faudit + "' and clinea='" + clinea + "' group by clinea");

            ultreg.clinea = ds.Tables[0].Rows[0]["clinea"].ToString();
            ultreg.ultreg = Int32.Parse(ds.Tables[0].Rows[0]["utlreg"].ToString());

            return ultreg;
        }

        [HttpGet]
        [Route("api/UltimoRegistroCorte")]
        public paudit02 GetUltimoRegistroCorte(string careas, string faudit)
        {
            paudit02 ultreg = new paudit02();
            DataSet ds = SqlComandos.EjecutarSel(@"select isnull(max(nsecue),0) as utlreg from paudit01 a where careas='" + careas + "' and faudit='" + faudit + "'");
            
            ultreg.ultreg = Int32.Parse(ds.Tables[0].Rows[0]["utlreg"].ToString());

            return ultreg;
        }
    }
}
