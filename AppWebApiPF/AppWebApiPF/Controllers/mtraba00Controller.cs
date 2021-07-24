using AppWebApiPF.Models;
using DataAccessPF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace AppWebApiPF.Controllers
{
    public class mtraba00Controller : ApiController
    {
        // GET api/mtraba00
        public IEnumerable<mtraba10> Get()
        {
            var sinfac = new string[] { "16", "17", "19", "29", "31", "33","25" };
            using (bdtexEntities db = new bdtexEntities())
            {
                return db.mtraba00.Where(y => y.straba == "A" && sinfac.Contains(y.xsecci) && y.ctptra == "O")
                                    .Select(x => new mtraba10
                                                    {
                                                        ctraba = x.ctraba,
                                                        dtraba = x.dtraba,
                                                        ccargo = x.ccargo,
                                                        dcargo = x.dcargo,
                                                        xsecci = x.xsecci,
                                                        clinea = x.clinea
                                                    }
                                                    ).ToList();         
            }
        }

        [HttpGet]
        [Route("api/ListaOperarios")]
        public IEnumerable<mtraba10> GetListaOperarios()
        {
            IList<mtraba10> gettrabaj = new List<mtraba10>();
            DataSet ds = SqlComandos.EjecutarSel(@"select b.ctraba,rtrim(b.dappat)+' '+rtrim(b.dapmat)+' '+rtrim(b.dnombr) as dtraba,b.ccargo,c.dcargo,b.csecci, b.clnprd as clinea
                                                    from pmarca00 a
                                                    inner join mtraba00 b on a.ctraba=b.ctraba
                                                    left join tcargo00 c on b.ccargo=c.ccargo and b.csecci=c.csecci
                                                    where cast(a.fmarca as date) between cast(getdate()-7 as date) and cast(getdate() as date) and b.svigen='S' and b.straba='A' and b.ctptra='O' and b.csecci in ('16', '17', '19', '29', '31', '33','25')
                                                    --and b.clnprd='01' and b.csecci='19'
                                                    group by b.ctraba,rtrim(b.dappat)+' '+rtrim(b.dapmat)+' '+rtrim(b.dnombr),b.ccargo,c.dcargo,b.csecci, b.clnprd
                                                    order by b.ctraba");

            mtraba10 trabaj = null;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                trabaj = new mtraba10();
                trabaj.ctraba = ds.Tables[0].Rows[i]["ctraba"].ToString().Trim();
                trabaj.dtraba = ds.Tables[0].Rows[i]["dtraba"].ToString().Trim();
                trabaj.ccargo = ds.Tables[0].Rows[i]["ccargo"].ToString().Trim();
                trabaj.dcargo = ds.Tables[0].Rows[i]["dcargo"].ToString().Trim();
                trabaj.xsecci = ds.Tables[0].Rows[i]["csecci"].ToString().Trim();
                trabaj.clinea = ds.Tables[0].Rows[i]["clinea"].ToString().Trim();

                gettrabaj.Add(trabaj);
            }
            return gettrabaj;
        }

    }
}
