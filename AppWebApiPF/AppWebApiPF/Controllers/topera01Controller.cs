using AppWebApiPF.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace AppWebApiPF.Controllers
{
    public class topera01Controller : ApiController
    {
        // GET api/topera01
        public IEnumerable<topera01> Get()
        {
            IList<topera01> getoperac = new List<topera01>();
            DataSet ds = SqlComandos.EjecutarSel(@"SELECT topera01.cclave,   
                                                            topera01.descri,   
                                                            topera01.cgrupo,
                                                            rtrim(topera01.cclave) +' - '+ rtrim(topera01.descri) as dopera"
                                                     + Environment.NewLine
                                                    +"FROM topera01"
                                                     + Environment.NewLine
                                                   +"WHERE topera01.sopera = 'S' and cgrupo<>'DF'");

            topera01 operac = null;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                operac = new topera01();
                operac.cclave = ds.Tables[0].Rows[i]["cclave"].ToString();
                operac.descri = ds.Tables[0].Rows[i]["descri"].ToString();
                operac.cgrupo = ds.Tables[0].Rows[i]["cgrupo"].ToString();
                operac.dopera = ds.Tables[0].Rows[i]["dopera"].ToString();

                getoperac.Add(operac);
            }
            return getoperac;
        }
    
    }
}
