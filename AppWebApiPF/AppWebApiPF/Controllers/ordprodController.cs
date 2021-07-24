using AppWebApiPF.Models;
using DataAccessPF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppWebApiPF.Controllers
{
    public class ordprodController : ApiController
    {
        // GET api/ordprod
        public IEnumerable<ordprod> Get(string nordpr)
        {
            using (bdtexEntities db = new bdtexEntities())
            {
                //return 
                var data = (from p in db.padcor00
                            join pc in db.pcoxes00 on
                            new { p.nordpr, p.ccarub }
                            equals
                            new { pc.nordpr, pc.ccarub }
                            join pg in db.pdgorp00 on p.nordpr equals pg.nordpr
                            into result1
                            from newresult in result1.DefaultIfEmpty()
                            join cl in db.mclien00 on newresult.cclien equals cl.cclien
                            into result2
                            from newresult2 in result2.DefaultIfEmpty()
                            select new ordprod{ nordpr=p.nordpr, nordct=p.nordct, ccarub=p.ccarub, dcarub=pc.dcarub, dclien=newresult2.drzsoc,nivaql= newresult2.nivaql }
                            ).Where(x => x.nordpr == nordpr).ToList();

                return data;                        
            }
        }

        [HttpGet]
        [Route("api/getauditoriafinal")]
        public IEnumerable<auditoriafinal> GetAuditoriaFinal()
        {
            IList<auditoriafinal> getauditoriafinal = new List<auditoriafinal>();
            DataSet ds = SqlComandos.EjecutarSel(@" select t.nordpr,t.nordco,t.ccarub,t.dcarub,t.qtiter,t.qpacki,t.npacki,t.norden,n.drzsoc as dclien,o.dpaise
                                                     from tmp_paufin t
                                                     left join ( select nordpr,nordpo,nordco,ccarub,Case status when 'A' then 'Aprob' when 'D' then 'Desap' when 'E' then 'Aprb.Extr' end stafin,faudit ,
                                                                ROW_NUMBER() OVER (PARTITION BY nordpr,nordco,nordpo,ccarub ORDER BY nordpr,nordco,nordpo,ccarub,faudit desc,nsecue desc) as nfilas    
			                                                    from paudit01 a
			                                                       where careas='FI' and flgext='S'
			                                                       )l on t.nordpr=l.nordpr and t.nordco=l.nordco and t.norden=l.nordpo and t.ccarub=l.ccarub and l.nfilas=1
                                                    left join ( select nordpr,nordpo,nordco,ccarub,Case status when 'A' then 'Aprob' when 'D' then 'Desap' when 'E' then 'Aprb.Extr' end stafin,faudit ,
                                                                ROW_NUMBER() OVER (PARTITION BY nordpr,nordco,nordpo,ccarub ORDER BY nordpr,nordco,nordpo,ccarub,faudit desc,nsecue desc) as nfilas    
			                                                    from paudit01 a
			                                                       where careas='FI' and flgext='N'
			                                                       )m on t.nordpr=m.nordpr and t.nordco=m.nordco and t.norden=m.nordpo and t.ccarub=m.ccarub and m.nfilas=1
                                                    left join mclien00 n on t.cclien=n.cclien
                                                    left join tpaise00 o on t.cpaise=o.cpaise
                                                    where isnull(m.stafin,'Pend')='Pend' and isnull(l.stafin,'Pend')='Pend' and t.flgcie='S'
                                                    order by t.nordpr");

            auditoriafinal muestra = null;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                muestra = new auditoriafinal();
                muestra.nordpr = ds.Tables[0].Rows[i]["nordpr"].ToString().Trim();
                muestra.nordco = ds.Tables[0].Rows[i]["nordco"].ToString().Trim();
                muestra.ccarub = ds.Tables[0].Rows[i]["ccarub"].ToString().Trim();
                muestra.dcarub = ds.Tables[0].Rows[i]["dcarub"].ToString().Trim();
                muestra.qtiter = Int32.Parse(ds.Tables[0].Rows[i]["qtiter"].ToString().Trim());
                muestra.qpacki = Int32.Parse(ds.Tables[0].Rows[i]["qpacki"].ToString().Trim());
                muestra.npacki = Int32.Parse(ds.Tables[0].Rows[i]["npacki"].ToString().Trim());
                muestra.norden = Int32.Parse(ds.Tables[0].Rows[i]["norden"].ToString().Trim());
                muestra.dclien = ds.Tables[0].Rows[i]["dclien"].ToString().Trim();
                muestra.dpaise = ds.Tables[0].Rows[i]["dpaise"].ToString().Trim();

                getauditoriafinal.Add(muestra);
            }
            return getauditoriafinal;
        }
    }
}
