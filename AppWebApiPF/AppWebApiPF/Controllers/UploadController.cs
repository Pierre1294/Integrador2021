using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AppWebApiPF.Controllers
{
    public class UploadController : ApiController
    {
        public async Task<string> Post()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;                

                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];

                        var fileName = postedFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
                        
                        var filePath = @"\\192.168.2.55\sistex\defectos\" + fileName;

                        postedFile.SaveAs(filePath);

                        return "/Uploads/" + fileName;
                    }
                }
            }
            catch (Exception exception)
            {
                return exception.Message;
            }

            return "no files";
        }
    }
}
