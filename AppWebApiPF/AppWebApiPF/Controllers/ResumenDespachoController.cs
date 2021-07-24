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
    public class ResumenDespachoController : ApiController
    {
        // GET api/ResumenDespacho
        public IEnumerable<ResumenDespacho> Get()
        {
            string fdesde = "2019-04-15";
            string fhasta = "2019-04-21";
            IList<ResumenDespacho> getcorte = new List<ResumenDespacho>();
            DataSet ds = SqlComandos.EjecutarSel(@"select drzsoc as dclien,sum(cantidad_ops) cant_ops,sum(cantidad_ci)cant_ci, sum(cantidad_despac) cant_despac , 
	                                                    sum(qdespa) qdespa, sum(qprdas) qprdas, sum(iprecio) iprecio, sum(margen) margen, sum(margen_neto) margen_neto, sum(pprseg) pprseg,
	                                                    sum(segundas)segundas,sum(qmixpr)qmixpr,sum(qdespa)*1.0/sum(qprdas)*1.0  as qporde,sum(qdespa)*1.0/sum(cantidad_ops)*1.0  as qpropd
                                                  
                                                    from (SELECT tgener00.cclave cclien,tgener00.dclave drzsoc,
		                                                    count(pdgorp00.nordpr) cantidad_despac,
		                                                    count(distinct pdgorp00.nordpr) cantidad_ops, 
		                                                    sum(pdgaop00.qprdes) qdespa, 
		                                                    sum(pdgaop00.qitere) qprdas,
		                                                    sum( case when pdgaop00.qprdes > 0 then pdgaop00.iprdes*pdgaop00.qprdes else
		                                                    pdgaop00.iprdes*pdgaop00.qitems end) iprecio,  
		                                                    sum( case when smgcon='N' then 0 else case when pdgaop00.qprdes > 0 then pdgorp00.imgcon*pdgaop00.qprdes else
		                                                    case when pdgaop00.qprcer > 0 then pdgorp00.imgcon*pdgaop00.qprcer else
		                                                    pdgorp00.imgcon*pdgaop00.qitems end end end) margen  ,
		                                                    sum( case when smgcbr='N' then 0 else case when pdgaop00.qprdes > 0 then pdgorp00.imgcbr*pdgaop00.qprdes else
		                                                    case when pdgaop00.qprcer > 0 then pdgorp00.imgcbr*pdgaop00.qprcer else
		                                                    pdgorp00.imgcbr*pdgaop00.qitems end end end) margen_neto  ,
		                                                    0000.0000 pprseg ,0000.0000 segundas,
		                                                    sum(pdgaop00.qprdes*coalesce(tsecop99.qmixpr,0)) qmixpr,
		                                                    case when row_number() over(partition by pdgorp00.cestil order by (select null))=1 then 1 else 0 end cantidad_ci   
	                                                    FROM pdgorp00
		                                                    inner join pdgaop00 on pdgorp00.nordpr = pdgaop00.nordpr
		                                                    inner join mclien00 on pdgorp00.cclien = mclien00.cclien and mclien00.svigen = 'S'
		                                                    left  join tgener00 on tgener00.cclave = mclien00. cclmas and tgener00.ctabla='A6'
		                                                    left  join magent00 on pdgorp00.cagent = magent00.cagent and magent00.svigen = 'S'
		                                                    left  join (select cestil,nordpr, sum(p.qmixpr) qmixpr 
					                                                    from psecop00 p, tsecop00 q, tmaqui00 t 
					                                                    where p.cproce='08' and	p.copera=q.copera and q.cmaqui=t.cmaqui and smanua='N' 
					                                                    group by cestil,nordpr) tsecop99 on pdgorp00.nordpr = tsecop99.nordpr and pdgorp00.cestil = tsecop99.cestil
	                                                    WHERE ( pdgorp00.sordpr <> 'A' ) AND ( pdgaop00.sdespa not in ('X') ) AND  
		                                                    ( pdgaop00.sdespa <> 'N' ) AND ( pdgaop00.fembac >= '" + fdesde + "' ) AND ( pdgaop00.fembac <= '" + fhasta + "' )   GROUP BY tgener00.cclave ,tgener00.dclave,pdgorp00.cestil) a group by drzsoc");

            ResumenDespacho corte = null;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                corte = new ResumenDespacho();
                corte.dclien = ds.Tables[0].Rows[i]["dclien"].ToString();
                corte.cant_ops = Int32.Parse(ds.Tables[0].Rows[i]["cant_ops"].ToString());
                corte.cant_ci = Int32.Parse(ds.Tables[0].Rows[i]["cant_ci"].ToString());
                corte.cant_despac = Int32.Parse(ds.Tables[0].Rows[i]["cant_despac"].ToString());
                corte.qdespa = Int32.Parse(ds.Tables[0].Rows[i]["qdespa"].ToString());
                corte.qprdas = Int32.Parse(ds.Tables[0].Rows[i]["qprdas"].ToString());
                corte.iprecio = Decimal.Parse(ds.Tables[0].Rows[i]["iprecio"].ToString());
                corte.margen = Decimal.Parse(ds.Tables[0].Rows[i]["margen"].ToString());
                corte.margen_neto = Decimal.Parse(ds.Tables[0].Rows[i]["margen_neto"].ToString());
                corte.pprseg = Decimal.Parse(ds.Tables[0].Rows[i]["pprseg"].ToString());
                corte.segundas = Decimal.Parse(ds.Tables[0].Rows[i]["segundas"].ToString());
                corte.qmixpr = Decimal.Parse(ds.Tables[0].Rows[i]["qmixpr"].ToString());
                corte.qporde = Decimal.Parse(ds.Tables[0].Rows[i]["qporde"].ToString());
                corte.qpropd = Decimal.Parse(ds.Tables[0].Rows[i]["qpropd"].ToString());


                getcorte.Add(corte);
            }
            return getcorte;
        }
    }
}
