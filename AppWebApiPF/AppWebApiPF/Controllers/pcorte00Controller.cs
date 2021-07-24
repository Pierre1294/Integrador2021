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
    public class pcorte00Controller : ApiController
    {
        // GET api/pcorte00
        public IEnumerable<pcorte00> Get(string nordpr, string nordct, string npieza)
        {
            IList<pcorte00> getcorte = new List<pcorte00>();
            DataSet ds = SqlComandos.EjecutarSel(@"select nordpr,nordct,npieza,clotei,nmodul,cencog,qprend,npanos,dtalla,citems,ditems from (
                                                    select a.nordpr,   
                                                    a.nordct,   
                                                    a.npieza,   
                                                    a.clotei, 
                                                    isnull(a.nmodul,'')nmodul,
                                                    isnull(a.cencog,'')cencog,
                                                    a.qprend,
                                                    isnull(a.npanos,0)npanos,
                                                    dbo.fn_dame_talla_corte_pieza(a.nordpr,a.nordct,a.npieza)dtalla,
                                                    b.citems,
                                                    c.ditems
                                                    from pcorte00 a inner join 
	                                                    ppzxes00 b on b.ctpord = a.ctpord  and  
				                                                    b.nordpr = a.nordpr  and  
				                                                    b.npieza = a.npieza  and  
				                                                    b.nalter = a.nalter 
	                                                    left join mitems00 c on b.citems=c.citems
                                                    where b.ngrupo='1' AND a.nordpr='"+ nordpr + "' and nordct='"+nordct+"' and a.npieza='"+npieza+"'"
                                                    +" union "
                                                    + Environment.NewLine
                                                   + @"select b.nordpr,b.nordct,a.npieza,b.clotei,b.nmodul,b.cencog,b.qprend,b.npanos,b.dtalla,b.citems,b.ditems
                                                    from ppzxes00 a 
                                                    left join (select a.nordpr,   
			                                                    a.nordct,   
			                                                    a.npieza,   
			                                                    a.clotei, 
			                                                    isnull(a.nmodul,'')nmodul,
			                                                    isnull(a.cencog,'')cencog,
			                                                    a.qprend,
			                                                    isnull(a.npanos,0)npanos,
			                                                    dbo.fn_dame_talla_corte_pieza(a.nordpr,a.nordct,a.npieza)dtalla,
			                                                    b.citems,
			                                                    c.ditems
			                                                    from pcorte00 a inner join 
				                                                    ppzxes00 b on b.ctpord = a.ctpord  and  
							                                                    b.nordpr = a.nordpr  and  
							                                                    b.npieza = a.npieza  and  
							                                                    b.nalter = a.nalter 
				                                                    left join mitems00 c on b.citems=c.citems
			                                                    where b.ngrupo='1' AND a.nordpr='"+ nordpr + "' and nordct='"+ nordct + "') b on a.citems=b.citems"                                                                 
                                                     +@" where a.nordpr='"+ nordpr + "' and ngrupo=1 and not exists (select npieza from pcorte00 where nordpr=a.nordpr and npieza=a.npieza)"
                                                    +@" )t where npieza='"+ npieza + "'");
            //DataSet ds = SqlComandos.EjecutarSel(@"select a.nordpr,   
            //                                        a.nordct,   
            //                                        a.npieza,   
            //                                        a.clotei, 
            //                                  isnull(a.nmodul,'')nmodul,
            //                                     isnull(a.cencog,'')cencog,
            //                                     a.qprend,
            //                                     isnull(a.npanos,0)npanos,
            //                                  dbo.fn_dame_talla_corte_pieza(a.nordpr,a.nordct,a.npieza)dtalla,
            //                                  b.citems,
            //                                  c.ditems
            //                                  from pcorte00 a inner join 
            //                                   ppzxes00 b on b.ctpord = a.ctpord  and  
            //                                      b.nordpr = a.nordpr  and  
            //                                      b.npieza = a.npieza  and  
            //                                      b.nalter = a.nalter 
            //                                   left join mitems00 c on b.citems=c.citems
            //                                  where a.nordpr='" + nordpr+"' and nordct='"+nordct+"' and a.npieza='"+npieza+"'");

            pcorte00 corte = null;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                corte = new pcorte00();
                corte.nordpr = ds.Tables[0].Rows[i]["nordpr"].ToString();
                corte.nordct = ds.Tables[0].Rows[i]["nordct"].ToString();
                corte.npieza = Int32.Parse(ds.Tables[0].Rows[i]["npieza"].ToString());
                corte.clotei = ds.Tables[0].Rows[i]["clotei"].ToString();
                corte.nmodul = ds.Tables[0].Rows[i]["nmodul"].ToString();
                corte.cencog = ds.Tables[0].Rows[i]["cencog"].ToString();
                corte.qprend = Int32.Parse(ds.Tables[0].Rows[i]["qprend"].ToString());
                corte.npanos = Int32.Parse(ds.Tables[0].Rows[i]["npanos"].ToString());
                corte.dtalla = ds.Tables[0].Rows[i]["dtalla"].ToString();
                corte.citems = ds.Tables[0].Rows[i]["citems"].ToString();
                corte.ditems = ds.Tables[0].Rows[i]["ditems"].ToString();

                getcorte.Add(corte);
            }
            return getcorte;
        }
    }
}
