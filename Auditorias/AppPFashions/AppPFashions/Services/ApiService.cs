using AppPFashions.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppPFashions.Services
{
    public class ApiService
    {
        string xcrutapi = "http://192.168.0.26:7030";

        public async Task<Response> Login(string user, string password)
        {
            Usuario usuario = new Usuario();
            try
            {
                
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "/api/ousuar00?cusuar=" + user + "&cclave=" + password;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Usuario o contraseña incorrectos",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                usuario = JsonConvert.DeserializeObject<Usuario>(result);
            
                return new Response
                {
                    IsSuccess = true,                    
                    Result = usuario,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Usuario o contraseña incorrectos",
                    //    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Operarios<mtraba00>()
        {
            List<mtraba00> operarios = new List<mtraba00>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "/api/ListaOperarios";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Hubo problemas con la sincronización",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                operarios = JsonConvert.DeserializeObject<List<mtraba00>>(result);

                return new Response
                {
                    IsSuccess = true,                    
                    Result = operarios,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Operaciones<topera01>()
        {
            List<topera01> operaciones = new List<topera01>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "/api/topera01";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Hubo problemas con la sincronización",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                operaciones = JsonConvert.DeserializeObject<List<topera01>>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = operaciones,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Defectos<mdefec00>(string xcsecci)
        {
            List<mdefec00> defectos = new List<mdefec00>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "/api/DefectosSeccion?csecci=" + xcsecci;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Hubo problemas con la sincronización",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                defectos = JsonConvert.DeserializeObject<List<mdefec00>>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = defectos,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> ListaDefectos<mdefec10>(string xcsecci)
        {
            List<mdefec10> defectos = new List<mdefec10>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "/api/mdefec10?caraud=" + xcsecci;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Hubo problemas con la sincronización",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                defectos = JsonConvert.DeserializeObject<List<mdefec10>>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = defectos,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetOP<ordprod>(string nordpr)
        {
            List<ordprod> fichas = new List<ordprod>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "/api/ordprod?nordpr=" + nordpr;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                fichas = JsonConvert.DeserializeObject<List<ordprod>>(result);

                if (fichas.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La OP " + nordpr + " no existe",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = fichas,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetAuditoriaFinal<auditoriafinal>()
        {
            List<auditoriafinal> fichas = new List<auditoriafinal>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "/api/getauditoriafinal";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                fichas = JsonConvert.DeserializeObject<List<auditoriafinal>>(result);

                if (fichas.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La OP no existe",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = fichas,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetAuditoriaResumen<paudit10>(string careas,DateTime faudit)
        {
            List<paudit10> fichas = new List<paudit10>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "/api/paudit10?careas=" + careas+ "&faudit=2018-11-10";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                fichas = JsonConvert.DeserializeObject<List<paudit10>>(result);

                if (fichas.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La OP " + careas + " no existe",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = fichas,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> Aql<ttcmue00>()
        {
            List<ttcmue00> muestra = new List<ttcmue00>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "/api/ttcmue00";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Hubo problemas con la sincronización",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                muestra = JsonConvert.DeserializeObject<List<ttcmue00>>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = muestra,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetPiezas<ppzxes00>(string nordpr,string ccarub)
        {
            List<ppzxes00> piezas = new List<ppzxes00>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "/api/ppzxes00?nordpr=" + nordpr+"&ccarub="+ccarub;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                piezas = JsonConvert.DeserializeObject<List<ppzxes00>>(result);

                if (piezas.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La OP " + nordpr + " no existe",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = piezas,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetCorte<pcorte00>(string nordpr, string nordct,string npieza)
        {
            List<pcorte00> corte = new List<pcorte00>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "/api/pcorte00?nordpr=" + nordpr + "&nordct=" + nordct + "&npieza=" + npieza;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                corte = JsonConvert.DeserializeObject<List<pcorte00>>(result);

                if (corte.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La OP " + nordpr + " no existe",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = corte,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetCorteColor<padcor00>(string nordpr, string nordct)
        {
            List<padcor00> corte = new List<padcor00>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "/api/padcor00?nordpr=" + nordpr + "&nordct=" + nordct ;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                corte = JsonConvert.DeserializeObject<List<padcor00>>(result);

                if (corte.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La OP " + nordpr + " no existe",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = corte,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetUltRegistro(string careas, string faudit, string clinea)
        {
            paudit02 ultregistro = new paudit02();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "/api/paudit02?careas=" + careas + "&faudit=" + faudit + "&clinea=" + clinea;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer el ultimo registro de auditoria",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                ultregistro = JsonConvert.DeserializeObject<paudit02>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = ultregistro,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetUltRegistroCorte(string careas, string faudit)
        {
            paudit02 ultregistro = new paudit02();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "/api/UltimoRegistroCorte?careas=" + careas + "&faudit=" + faudit ;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer el ultimo registro de auditoria",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                ultregistro = JsonConvert.DeserializeObject<paudit02>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = ultregistro,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetFichasPdf<OrdenProduccion>(string nordpr)
        {
            List<OrdenProduccion> corte = new List<OrdenProduccion>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "/OrdenP?nordpr=" + nordpr;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                corte = JsonConvert.DeserializeObject<List<OrdenProduccion>>(result);

                if (corte.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La OP " + nordpr + " no existe",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = corte,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<string> GetFechaApk()
        {            
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "api/GetApk";
                string response = await client.GetStringAsync(url);

                 return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> GetAuditoriaCortePdf<AuditoriaCorte>(string nordpr)
        {
            List<AuditoriaCorte> corte = new List<AuditoriaCorte>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "api/GetFileCorte?nordpr=" + nordpr;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                corte = JsonConvert.DeserializeObject<List<AuditoriaCorte>>(result);

                if (corte.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "La OP " + nordpr + " no existe",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = corte,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetEficienciaBihorarioBloque<EficienciaBihorario>(string cbloqu)
        {
            List<EficienciaBihorario> efibihor = new List<EficienciaBihorario>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "api/EficienciaBihorarioBloque?cbloqu=" + cbloqu;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                efibihor = JsonConvert.DeserializeObject<List<EficienciaBihorario>>(result);

                if (efibihor.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Aun no existen lecturas de tickets",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = efibihor,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetEficienciaSemanalBloque<EficienciaSemanal>(string cbloqu)
        {
            List<EficienciaSemanal> efisemanal = new List<EficienciaSemanal>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "api/EficienciaSemanalBloque?cbloqu=" + cbloqu;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                efisemanal = JsonConvert.DeserializeObject<List<EficienciaSemanal>>(result);

                if (efisemanal.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al traer la información",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = efisemanal,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetEficienciaErrorSemanalBloque<EficienciaErrorSemanal>(string cbloqu)
        {
            List<EficienciaErrorSemanal> efisemanal = new List<EficienciaErrorSemanal>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "api/EficienciaErrorSemanalBloque?cbloqu=" + cbloqu;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                efisemanal = JsonConvert.DeserializeObject<List<EficienciaErrorSemanal>>(result);

                if (efisemanal.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al traer la información",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = efisemanal,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetDashboardEficiencia<DashboardEficiencia>(string cbloqu)
        {
            List<DashboardEficiencia> efisemanal = new List<DashboardEficiencia>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "api/DashboardEficiencia?cbloqu=" + cbloqu;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                efisemanal = JsonConvert.DeserializeObject<List<DashboardEficiencia>>(result);

                if (efisemanal.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al traer la información",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = efisemanal,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetDashboardEficienciaLinea<DashboardEficienciaLinea>(string cbloqu)
        {
            List<DashboardEficienciaLinea> efisemanal = new List<DashboardEficienciaLinea>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "api/DashboardEficienciaLinea?cbloqu=" + cbloqu;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                efisemanal = JsonConvert.DeserializeObject<List<DashboardEficienciaLinea>>(result);

                if (efisemanal.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al traer la información",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = efisemanal,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetEficienciaDestajeroDetalle<EficienciaDestajeroDetalle>(string cbloqu,string ctraba,string fproce)
        {
            List<EficienciaDestajeroDetalle> efidetalle = new List<EficienciaDestajeroDetalle>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "api/EficienciaDestajeroDetalle?cbloqu="+ cbloqu + "&ctraba=" + ctraba + "&fproce=" + fproce;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                efidetalle = JsonConvert.DeserializeObject<List<EficienciaDestajeroDetalle>>(result);

                if (efidetalle.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al traer la información",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = efidetalle,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetAuditoriaDefectos<AuditoriaDefectos>(string cbloqu)
        {
            List<AuditoriaDefectos> efisemanal = new List<AuditoriaDefectos>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "api/AuditoriaDefectos?cbloqu=" + cbloqu;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                efisemanal = JsonConvert.DeserializeObject<List<AuditoriaDefectos>>(result);

                if (efisemanal.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al traer la información",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = efisemanal,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetReporteAuditoriaDefectos<AuditoriaDefectos>(string cbloqu)
        {
            List<AuditoriaDefectos> efisemanal = new List<AuditoriaDefectos>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "api/ReporteAuditoriaDefectos?cbloqu=" + cbloqu;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                efisemanal = JsonConvert.DeserializeObject<List<AuditoriaDefectos>>(result);

                if (efisemanal.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al traer la información",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = efisemanal,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetProgramaCostura<ProgramaCostura>(string cbloqu,string wfecini,string wfecfin)
        {
            List<ProgramaCostura> progcostura = new List<ProgramaCostura>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "api/ProgramaCosturaBloqueLinea?cbloque=" + cbloqu+ "&wfecini="+ wfecini+ "&wfecfin="+ wfecfin;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                progcostura = JsonConvert.DeserializeObject<List<ProgramaCostura>>(result);

                if (progcostura.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al traer la información",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = progcostura,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetIngresoPrendasCostura<IngresoPrendasCostura>(string ctipos, string wfecini, string wfecfin, string nordpr, string sbloqc)
        {
            List<IngresoPrendasCostura> progcostura = new List<IngresoPrendasCostura>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "api/IngresoPrendasCostura?ctipos=" + ctipos + "&wfecini=" + wfecini + "&wfecfin=" + wfecfin+ "&nordpr="+ nordpr+ "&sbloqc="+ sbloqc;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                progcostura = JsonConvert.DeserializeObject<List<IngresoPrendasCostura>>(result);

                if (progcostura.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al traer la información",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = progcostura,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetBalanceLineaCostura<BalanceLineaCostura>(string nordpr, string cbloqu)
        {
            List<BalanceLineaCostura> progcostura = new List<BalanceLineaCostura>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "api/BalanceLineaCostura?nordpr=" + nordpr + "&cbloqu=" + cbloqu; ;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                progcostura = JsonConvert.DeserializeObject<List<BalanceLineaCostura>>(result);

                if (progcostura.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al traer la información",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = progcostura,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }

        public async Task<Response> GetOpProgCostura<ProgramaCostura>(string cbloqu)
        {
            List<procos00> progcostura = new List<procos00>();
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                client.BaseAddress = new Uri(xcrutapi);
                var url = "api/GetOpProgCostura?cbloqu=" + cbloqu ;
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al intentar traer datos de la OP",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                progcostura = JsonConvert.DeserializeObject<List<procos00>>(result);

                if (progcostura.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Error al traer la información",
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = progcostura,
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No se encuentra dentro de la red de Perú Fashions",
                };
            }
        }
    }
}
