using System;
using System.Collections.Generic;
using System.Linq;
using AppPFashions.Models;
using SQLite.Net;
using SQLite.Net.Interop;
using SQLiteNetExtensions.Extensions;


namespace AppPFashions.Data
{
    public class BaseDatos
    {
        static object locker = new object();
        readonly ISQLitePlatform _plataforma;
        string _rutaBD;

        public SQLiteConnection Conexion { get; set; }

        public BaseDatos(ISQLitePlatform plataforma, string rutaBD)
        {
            _plataforma = plataforma;
            _rutaBD = rutaBD;
        }

        public void Conectar()
        {
            Conexion = new SQLiteConnection(_plataforma, _rutaBD, 
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create 
                | SQLiteOpenFlags.FullMutex, true);

            Conexion.CreateTable<Usuario>();
            Conexion.CreateTable<mtraba00>();
            Conexion.CreateTable<topera01>();
            Conexion.CreateTable<mdefec00>();
            Conexion.CreateTable<mdefec10>();
            Conexion.CreateTable<pdefec10>();
            Conexion.CreateTable<pdefec01>();
            Conexion.CreateTable<paudit01>();
            Conexion.CreateTable<taudit00>();
            Conexion.CreateTable<taudit10>();
            Conexion.CreateTable<raudit00>();
            Conexion.CreateTable<ttcmue00>();            
            Conexion.CreateTable<paudob00>();
            Conexion.CreateTable<procos00>();
            Conexion.CreateTable<auditoriafinal>();
        }

        public mtraba00 GetOperario(string ctraba)
        {
            lock (locker)
            {
                return Conexion.Table<mtraba00>().FirstOrDefault(c => c.ctraba == ctraba);
            }
        }

        public Usuario GetUsuario()
        {
            lock (locker)
            {
                return Conexion.Table<Usuario>().FirstOrDefault();
            }
        }

        public raudit00 GetResaudit(string careas, DateTime faudit, string clinea)
        {
            lock (locker)
            {
                return Conexion.Table<raudit00>().Where(x => x.careas == careas && x.faudit == faudit && x.clinea == clinea).FirstOrDefault();
            }
        }

        public taudit00 CargaAuditoria(string careas, DateTime faudit, string clinea, Int32 nsecue)
        {
            lock (locker)
            {
                return Conexion.Table<taudit00>().Where(x => x.careas == careas && x.faudit == faudit && x.clinea == clinea && x.nsecue==nsecue).FirstOrDefault();
            }
        }

        public void Insert<T>(T model)
        {
            lock (locker)
            {
                Conexion.Insert(model);
            }
        }

        public void Update<T>(T model)
        {
            lock (locker)
            {
                Conexion.Update(model);
            }
        }

        public void Deletes<T>(List<T> model)
        {
            lock (locker)
            {
                Conexion.Delete(model);
            }
        }

        public void Delete<T>(T model)
        {
            lock (locker)
            {
                Conexion.Delete(model);
            }
        }

        public void DeleteOperarios()
        {
            lock (locker)
            {
                Conexion.DeleteAll<mtraba00>();
            }
        }

        public void DeleteOperaciones()
        {
            lock (locker)
            {
                Conexion.DeleteAll<topera01>();
            }
        }

        public void DeleteProgOP(string xnordpr)
        {
            lock (locker)
            {
                Conexion.Table<procos00>().Delete(x=>x.nordpr==xnordpr);
            }
        }
        public void DeleteAql()
        {
            lock (locker)
            {
                Conexion.DeleteAll<ttcmue00>();
            }
        }

        public void DeleteDefectos(string xcsecci)
        {
            lock (locker)
            {
                //Conexion.DeleteAll<mdefec00>();
                Conexion.Table<mdefec00>().Delete(x=>x.csecci== xcsecci);
            }
        }

        public void DeleteListaDefectos(string xcsecci)
        {
            lock (locker)
            {
                //Conexion.DeleteAll<mdefec00>();
                Conexion.Table<mdefec10>().Delete(x => x.caraud == xcsecci);
            }
        }

        public void DeleteAuditoriaDefectos()
        {
            lock (locker)
            {
                Conexion.DeleteAll<pdefec01>();
                Conexion.DeleteAll<pdefec10>();
            }
        }

        public void DeleteAllAuditoria()
        {
            lock (locker)
            {
                Conexion.DeleteAll<pdefec01>();
                Conexion.DeleteAll<pdefec10>();
                //Conexion.DeleteAll<caudit00>();
                Conexion.DeleteAll<paudit01>();
                Conexion.DeleteAll<taudit00>();
                Conexion.DeleteAll<raudit00>();
            }
        }

        public void DeleteAllProgCostura()
        {
            lock (locker)
            {
                Conexion.DeleteAll<procos00>();
            }
        }

        public void DeleteAllAuditoriaFinal()
        {
            lock (locker)
            {
                Conexion.DeleteAll<auditoriafinal>();
            }
        }

        public void DeleteAllAuditoriaAprobados(DateTime xfaudit)
        {
            lock (locker)
            {
                Conexion.Table<pdefec01>().Delete(x => x.faudit != xfaudit);
                Conexion.Table<pdefec10>().Delete(x => x.faudit != xfaudit);
                Conexion.Table<paudit01>().Delete(x => x.faudit != xfaudit && x.status != "D");
                Conexion.Table<taudit00>().Delete(x => x.faudit != xfaudit && x.status != "D");
                Conexion.Table<raudit00>().Delete(x => x.faudit != xfaudit);
            }
        }

        public void DeleteAuditoria(DateTime xfaudit, string clinea, string careas)
        {
            lock (locker)
            {
                Conexion.Table<paudit01>().Delete(x => x.faudit == xfaudit && x.clinea == clinea && x.careas == careas);
                Conexion.Table<taudit00>().Delete(x => x.faudit == xfaudit && x.clinea == clinea && x.careas == careas);
                Conexion.Table<pdefec01>().Delete(x => x.faudit == xfaudit && x.clinea == clinea && x.careas == careas);
                Conexion.Table<pdefec10>().Delete(x => x.faudit == xfaudit && x.clinea == clinea && x.careas == careas);
                Conexion.Table<raudit00>().Delete(x => x.faudit == xfaudit && x.clinea == clinea && x.careas == careas);
            }
        }

        public void DeleteDefectoTemp()
        {
            lock (locker)
            {                
                Conexion.Table<pdefec10>().Delete(x => x.svigen == "N");                
            }
        }

        public void DeleteAudiDefecTemp()
        {
            lock (locker)
            {
                Conexion.DeleteAll<pdefec10>();
            }
        }

     



        public T First<T>(bool WithChildren) where T : class
        {
            lock (locker)
            {
                if (WithChildren)
                {
                    return Conexion.GetAllWithChildren<T>().FirstOrDefault();
                }
                else
                {
                    return Conexion.Table<T>().FirstOrDefault();
                }
            }
        }    

        public List<T> GetList<T>(bool WithChildren) where T : class
        {
            lock (locker)
            {
                if (WithChildren)
                {
                    return Conexion.GetAllWithChildren<T>().ToList();
                }
                else
                {
                    return Conexion.Table<T>().ToList();
                }
            }
        }

        public T Find<T>(int pk, bool WithChildren) where T : class
        {
            lock (locker)
            {
                if (WithChildren)
                {
                    return Conexion.GetAllWithChildren<T>().FirstOrDefault(m => m.GetHashCode() == pk);
                }
                else
                {
                    return Conexion.Table<T>().FirstOrDefault(m => m.GetHashCode() == pk);
                }
            }
        }

        public void Dispose()
        {
            Conexion.Dispose();
        }

    }
}
