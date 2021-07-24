using AppWebApiPF.Models;
using DataAccessPF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppWebApiPF.Controllers
{
    public class paudit10Controller : ApiController
    {
        // GET api/paudit10
        public IEnumerable<paudit10> Get(string careas,DateTime faudit)
        {
            IList<paudit10> getauditoria = new List<paudit10>();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@careas", SqlDbType.Char, 2);
            param[0].Value = careas;
            param[1] = new SqlParameter("@faudit", SqlDbType.DateTime);
            param[1].Value = faudit;

            DataTable dt = SqlComandos.ExecuteDataTable("sp_consulta_auditoria_1", param);

            paudit10 auditoria = null;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                auditoria = new paudit10();
                auditoria.clinea = dt.Rows[i]["clinea"].ToString();
                auditoria.qprime = Int32.Parse(dt.Rows[i]["qprime"].ToString());
                auditoria.qrecha = Int32.Parse(dt.Rows[i]["qrecha"].ToString());
                auditoria.qreaud = Int32.Parse(dt.Rows[i]["qreaud"].ToString());
                auditoria.qrerea = Int32.Parse(dt.Rows[i]["qrerea"].ToString());
                auditoria.cturno = dt.Rows[i]["cturno"].ToString();

                getauditoria.Add(auditoria);
            }
            return getauditoria;
        }

    }
}
