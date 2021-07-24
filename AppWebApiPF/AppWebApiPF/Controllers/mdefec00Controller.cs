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
    public class mdefec00Controller : ApiController
    {
        // GET api/mdefec00
        public IEnumerable<mdefec00> Get()
        {
            IList<mdefec00> getdefec = new List<mdefec00>();
            DataSet ds = SqlComandos.EjecutarSel(@"SELECT  d.coddef, dbo.fn_initcap(d.descri) descri, dbo.fn_initcap(nullif(c.descri,'VARIOS')) dgrupo, c.codigo, dbo.fn_initcap(d.dapare)dapare,d.csecci,rtrim(d.coddef)+' - '+rtrim(d.descri) as ddefec
                                                    FROM mdefec00 d 
                                                    left join mcalid00 c on d.csecci=c.csecci and d.codigo=c.codigo 
                                                    WHERE d.csecci in ('19','16','31','30','33','32','29') and d.svigen='S'");

            mdefec00 defec = null;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                defec = new mdefec00();
                defec.coddef = ds.Tables[0].Rows[i]["coddef"].ToString();
                defec.descri = ds.Tables[0].Rows[i]["descri"].ToString();
                defec.dgrupo = ds.Tables[0].Rows[i]["dgrupo"].ToString();
                defec.codigo = ds.Tables[0].Rows[i]["codigo"].ToString();
                defec.dapare = ds.Tables[0].Rows[i]["dapare"].ToString();
                defec.csecci = ds.Tables[0].Rows[i]["csecci"].ToString();
                defec.ddefec = ds.Tables[0].Rows[i]["ddefec"].ToString();

                getdefec.Add(defec);
            }
            return getdefec;
        }

        [HttpGet]
        [Route("api/DefectosSeccion")]
        public IEnumerable<mdefec00> GetDefectosSeccion(string csecci)
        {
            IList<mdefec00> getdefec = new List<mdefec00>();
            DataSet ds = SqlComandos.EjecutarSel(@"SELECT  d.coddef, dbo.fn_initcap(d.descri) descri, dbo.fn_initcap(nullif(c.descri,'VARIOS')) dgrupo, c.codigo, dbo.fn_initcap(d.dapare)dapare,d.csecci,rtrim(d.coddef)+' - '+rtrim(d.descri) as ddefec
                                                    FROM mdefec00 d 
                                                    left join mcalid00 c on d.csecci=c.csecci and d.codigo=c.codigo 
                                                    WHERE d.csecci='" + csecci + "' and d.svigen='S'");

            mdefec00 defec = null;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                defec = new mdefec00();
                defec.coddef = ds.Tables[0].Rows[i]["coddef"].ToString();
                defec.descri = ds.Tables[0].Rows[i]["descri"].ToString();
                defec.dgrupo = ds.Tables[0].Rows[i]["dgrupo"].ToString();
                defec.codigo = ds.Tables[0].Rows[i]["codigo"].ToString();
                defec.dapare = ds.Tables[0].Rows[i]["dapare"].ToString();
                defec.csecci = ds.Tables[0].Rows[i]["csecci"].ToString();
                defec.ddefec = ds.Tables[0].Rows[i]["ddefec"].ToString();

                getdefec.Add(defec);
            }
            return getdefec;
        }
    }
}
