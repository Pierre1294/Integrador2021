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
    public class mdefec10Controller : ApiController
    {
        // GET api/mdefec10
        public IEnumerable<mdefec10> Get(string caraud)
        {
            IList<mdefec10> getdefec = new List<mdefec10>();
            DataSet ds = SqlComandos.EjecutarSel(@"SELECT  a.caraud,
		                                                a.coddef, 
		                                                a.csecci,
		                                                rtrim(a.coddef)+' - '+rtrim(b.descri) as ddefec,
                                                        a.codigo
                                                FROM mdefec10 a
                                                left join mdefec00 b on a.coddef=b.coddef and a.csecci=b.csecci and a.codigo=b.codigo
                                                WHERE a.caraud='"+ caraud + "' and b.svigen='S'");

            mdefec10 defec = null;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                defec = new mdefec10();
                defec.caraud = ds.Tables[0].Rows[i]["caraud"].ToString();
                defec.coddef = ds.Tables[0].Rows[i]["coddef"].ToString();
                defec.csecci = ds.Tables[0].Rows[i]["csecci"].ToString();
                defec.ddefec = ds.Tables[0].Rows[i]["ddefec"].ToString();
                defec.codigo = ds.Tables[0].Rows[i]["codigo"].ToString();

                getdefec.Add(defec);
            }
            return getdefec;
        }
    }
}
