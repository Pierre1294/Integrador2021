using AppWebApiPF.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace AppWebApiPF.Controllers
{
    public class OrdenProduccionController : ApiController
    {
        [HttpGet]
        [Route("OrdenP")]
        public IEnumerable<OrdenProduccion> OrdenP(string nordpr)
        {
            IList<OrdenProduccion> getordenp = new List<OrdenProduccion>();
            DataSet ds = SqlComandos.EjecutarSel(@"select a.nordpr,
                                                    case when cdaxes='61' then 'Ficha Tecnica de Costura'
			                                            when cdaxes='63' then 'Ficha Tecnica de Corte' end dficha,rtrim(datext)drutaf,b.dclien,''dcolor,a.cdaxes
                                            from pdaxop00 a
                                            inner join (select a.nordpr,b.drzsoc dclien from pdgorp00 a
			                                            inner join mclien00 b on a.cclien=b.cclien and b.svigen='S' and b.sclien='A'
			                                            )b on a.nordpr=b.nordpr
                                            where a.nordpr='" + nordpr + "' and cdaxes<>'01'"
                                              + Environment.NewLine
                                            + "union"
                                              + Environment.NewLine
                                            + @"select b.nordpr,
                                                    case when cdaxes='53' then 'Avios'
			                                            when cdaxes='62' then 'Embalaje' end dficha,rtrim(datext) ,c.dclien,'',a.cdaxes
                                            from pdaxes00 a
                                            inner join (select distinct nordpr,cestil from pcoxes00) b on a.cestil=b.cestil
                                            left join (select a.nordpr,b.drzsoc dclien from pdgorp00 a
			                                            inner join mclien00 b on a.cclien=b.cclien and b.svigen='S' and b.sclien='A'
			                                            )c on b.nordpr=c.nordpr
                                            where a.cdaxes not in ('61','63') and b.nordpr='" + nordpr + "'"
                                            + "union"
                                                + Environment.NewLine
                                            + @"select a.nordpr,
                                            case when cdaxes = '01' then 'Medidas de Molde'
                                                when cdaxes = '02' then 'Medidas Antes de Proceso'                                                                                                 
                                                when cdaxes = '03' then 'Medidas de Acabado' 
                                                when cdaxes = '04' then 'Hoja de Diseño'
                                                when cdaxes = '05' then 'Hoja de Diseño Prenda' 
                                                when cdaxes = '06' then 'Orden de Producción General'
	                                            when cdaxes = '07' then 'Orden de Producción Adicionales'
	                                            when cdaxes = '08' then 'Orden de Producción por Destinos' 
                                                when cdaxes = '17' then 'Ficha de Corte'
	                                            when cdaxes = '18' then 'Control de Calidad - Tela Acabada' end dficha, rtrim(datext)drutaf, b.dclien,
                                                case when cdaxes in ('01','02') then 'Partida '+ right(rtrim(datext),6)+ ' - Color '+(select top 1 tcolor00.dcolor
												from pdglot00,tcolor00
												where pdglot00.ccolor = tcolor00.ccolor and
													tcolor00.scolor = 'A' and
													pdglot00.clotei = right(rtrim(datext),6)) end,a.cdaxes
                                            from poppdf00 a
                                            inner
                                            join (select a.nordpr, b.drzsoc dclien from pdgorp00 a
                                            inner join mclien00 b on a.cclien = b.cclien and b.svigen = 'S' and b.sclien = 'A'
                                                )b on a.nordpr = b.nordpr
                                            where a.nordpr = '" + nordpr + "'");
            
            OrdenProduccion ordenp = null;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                ordenp = new OrdenProduccion();
                ordenp.nordpr = ds.Tables[0].Rows[i]["nordpr"].ToString();
                ordenp.dficha = ds.Tables[0].Rows[i]["dficha"].ToString();
                ordenp.drutaf = ds.Tables[0].Rows[i]["drutaf"].ToString();
                ordenp.dclien = ds.Tables[0].Rows[i]["dclien"].ToString();
                ordenp.dcolor = ds.Tables[0].Rows[i]["dcolor"].ToString();
                ordenp.cdaxes = ds.Tables[0].Rows[i]["cdaxes"].ToString();
                getordenp.Add(ordenp);
            }
            return getordenp;
        }

        [HttpGet]
        [Route("api/File/GetFile")]
        public HttpResponseMessage GetFile(string fileName)
        {
            //Create HTTP Response.
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            //Set the File Path.
            string filePath = HttpContext.Current.Server.MapPath(@"\\192.168.2.55\sistex\textopdf\") + fileName;

            //Check whether File exists.
            if (!File.Exists(filePath))
            {
                //Throw 404 (Not Found) exception if File not found.
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", fileName);
                throw new HttpResponseException(response);
            }

            //Read the File into a Byte Array.
            byte[] bytes = File.ReadAllBytes(filePath);

            //Set the Response Content.
            response.Content = new ByteArrayContent(bytes);

            //Set the Response Content Length.
            response.Content.Headers.ContentLength = bytes.LongLength;

            //Set the Content Disposition Header Value and FileName.
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = fileName;

            //Set the File Content Type.
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(fileName));
            return response;
        }
    }
}
