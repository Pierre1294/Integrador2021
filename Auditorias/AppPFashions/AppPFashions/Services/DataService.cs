using AppPFashions.Data;
using AppPFashions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppPFashions.Services
{
    public class DataService
    {
        public Response InsertUser(Usuario user)
        {
            try
            {
                //using (var da = new DataAccess())
                //{
                    var oldUser = App.baseDatos.First<Usuario>(false);
                    if (oldUser != null)
                    {
                    App.baseDatos.Delete(oldUser);
                    }
                    App.baseDatos.Insert(user);               
                //}
                return new Response
                {
                    IsSuccess = true,
                    Message = "Usuario Insertado OK",
                    Result = user,
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

        public Usuario GetUser()
        {
            //using (var da = new DataAccess())
            //{                
                return App.baseDatos.First<Usuario>(false);
                //return App.DataAccess.FirstUser();
            //}
        }

        public Response UpdateUser(Usuario user)
        {
            try
            {
                //using (var da = new DataAccess())
                //{
                App.baseDatos.Update(user);               
                //}
                return new Response
                {
                    IsSuccess = true,
                    Message = "Usuario Actualizado OK",
                    Result = user,
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

        public Response InsertOperario(mtraba00 opera)
        {
            try
            {
                //using (var da = new DataAccess())
                //{                    
                App.baseDatos.Insert(opera);
                //}
                return new Response
                {
                    IsSuccess = true,
                    Message = "Usuario Insertado OK",
                    Result = opera,
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

        public Response InsertOperacion(topera01 opera)
        {
            try
            {
                //using (var da = new DataAccess())
                //{
                App.baseDatos.Insert(opera);
                //}
                return new Response
                {
                    IsSuccess = true,
                    Message = "Operación Insertado OK",
                    Result = opera,
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

        public Response InsertDefecto(mdefec00 defecto)
        {
            try
            {
                //using (var da = new DataAccess())
                //{
                App.baseDatos.Insert(defecto);
                //}
                return new Response
                {
                    IsSuccess = true,
                    Message = "Operación Insertado OK",
                    Result = defecto,
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

        public Response InsertListaDefecto(mdefec10 defecto)
        {
            try
            {
                //using (var da = new DataAccess())
                //{
                App.baseDatos.Insert(defecto);
                //}
                return new Response
                {
                    IsSuccess = true,
                    Message = "Operación Insertado OK",
                    Result = defecto,
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

        public Response InsertAql(ttcmue00 muestra)
        {
            try
            {
                //using (var da = new DataAccess())
                //{
                App.baseDatos.Insert(muestra);
                //}
                return new Response
                {
                    IsSuccess = true,
                    Message = "Usuario Insertado OK",
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

        public Response InsertObservacionAuditoria(paudob00 observ)
        {
            try
            {
                //using (var da = new DataAccess())
                //{                    
                App.baseDatos.Insert(observ);
                //}
                return new Response
                {
                    IsSuccess = true,
                    Message = "Observación Insertada OK",
                    Result = observ,
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

        public T InsertOrUpdate<T>(T model) where T : class
        {
            try
            {
                //using (var da = new DataAccess())
                //{
                    var oldRecord = App.baseDatos.Find<T>(model.GetHashCode(), false);
                    if (oldRecord != null)
                    {
                        App.baseDatos.Update(model);
                    }
                    else
                    {
                        App.baseDatos.Insert(model);
                    }
                    return model;
                //}
            }
            catch (Exception ex)
            {
                ex.ToString();
                return model;

            }
        }

        public void Save<T>(List<T> list) where T : class
        {
            //using (var da = new DataAccess())
            //{
                foreach (var record in list)
                {
                    InsertOrUpdate(record);
                }
            //}
        }


    }
}
