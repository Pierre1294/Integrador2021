using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppWebApiPF.Controllers
{
    public class VariosController : ApiController
    {
        [HttpGet]
        [Route("api/GetApk")]
        public string GetFechaApk()
        {
            //FileInfo fi = new FileInfo(@"\\perufashions2\sistex\textopdf\Auditoria.apk");
            DateTime dtapk = File.GetLastWriteTime(@"\\perufashions2\sistex\textopdf\Auditoria.apk");
            return dtapk.ToString("MM/dd/yyyy HH:mm:ss");
        }
    }
}
