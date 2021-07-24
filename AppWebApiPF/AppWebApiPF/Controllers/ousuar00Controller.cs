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
    public class ousuar00Controller : ApiController
    {
        // GET api/ousuar00
        public ousuar00 Get(string cusuar, string cclave)
        {
            ousuar00 getuser = new ousuar00();
            DataSet ds = SqlComandos.EjecutarSel(@"SELECT 1 userid, rtrim(a.cusuar)cusuar, rtrim(a.dusuar)dusuar, rtrim(a.cclave)cclave, a.cfunci, a.demail,a.ctraba,b.ccargo,b.clnprd as cbloqu
                                                    FROM ousuar00 a 
                                                    LEFT JOIN mtraba_view01 b on a.ctraba=b.ctraba and b.svigen='S' and b.straba='A' WHERE rtrim(a.cusuar) = '" + cusuar + "' AND rtrim(a.cclave) = '" + cclave + "'");
           
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                getuser.userid = ds.Tables[0].Rows[i]["userid"].ToString();
                getuser.cusuar = ds.Tables[0].Rows[i]["cusuar"].ToString();
                getuser.dusuar = ds.Tables[0].Rows[i]["dusuar"].ToString();
                getuser.cclave = ds.Tables[0].Rows[i]["cclave"].ToString();
                getuser.cfunci = ds.Tables[0].Rows[i]["cfunci"].ToString();
                getuser.demail = ds.Tables[0].Rows[i]["demail"].ToString();
                getuser.ctraba = ds.Tables[0].Rows[i]["ctraba"].ToString();
                getuser.ccargo = ds.Tables[0].Rows[i]["ccargo"].ToString();
                getuser.cbloqu = ds.Tables[0].Rows[i]["cbloqu"].ToString();
            }
            return getuser;
        }
    }
}
