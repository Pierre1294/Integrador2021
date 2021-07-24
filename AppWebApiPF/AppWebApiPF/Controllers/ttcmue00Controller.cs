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
    public class ttcmue00Controller : ApiController
    {
        // GET api/ttcmue00
        public IEnumerable<ttcmue00> Get()
        {
            IList<ttcmue00> getoperac = new List<ttcmue00>();
            DataSet ds = SqlComandos.EjecutarSel(@"Select codmue,ntamli,ntamlf,ntammu,nivaql,caprob 
                                                    From ttcmue00 
                                                    Where svigen='S'
                                                    Order by codmue");

            ttcmue00 muestra = null;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                muestra = new ttcmue00();
                muestra.codmue = ds.Tables[0].Rows[i]["codmue"].ToString();
                muestra.ntanli = Int32.Parse(ds.Tables[0].Rows[i]["ntamli"].ToString());
                muestra.ntanlf = Int32.Parse(ds.Tables[0].Rows[i]["ntamlf"].ToString());
                muestra.ntanmu = Int32.Parse(ds.Tables[0].Rows[i]["ntammu"].ToString());
                muestra.nivaql = ds.Tables[0].Rows[i]["nivaql"].ToString();
                muestra.caprob = ds.Tables[0].Rows[i]["caprob"].ToString();

                getoperac.Add(muestra);
            }
            return getoperac;
        }
    }
}
