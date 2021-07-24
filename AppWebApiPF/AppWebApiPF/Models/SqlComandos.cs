using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppWebApiPF.Models
{
    public static class SqlComandos
    {
        public static SqlConnection ConexionBD()
        {
            SqlConnection con = new SqlConnection(@"Data Source=RICMIJ;Initial Catalog=auditoria;Persist Security Info=True; User=sa;Password=1");            
            SqlConnection.ClearAllPools();
            return con;
        }

        static string connectionString = @"Data Source=RICMIJ;Initial Catalog=auditoria;Persist Security Info=True; User=sa;Password=1";

        public static DataSet EjecutarSel(string query)
        {
            ConexionBD().Open();
            SqlDataAdapter da = new SqlDataAdapter(query, ConexionBD());
            DataSet ds = new DataSet();
            da.Fill(ds);
            ConexionBD().Close();
            return ds;
        }

        public static DataTable ExecuteDataTable(String NombreStore, params SqlParameter[] SqlParametros)
        {
            return ExecuteDataTableProcedure(CommandType.StoredProcedure, NombreStore, SqlParametros);
        }

        public static DataTable ExecuteDataTableProcedure(CommandType SqlType, String Query, params SqlParameter[] SqlParametros)
        {
            SqlConnection oCn = new SqlConnection(connectionString);
            try
            {
                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCn;
                oCmd.CommandType = SqlType;
                oCmd.CommandText = Query;

                foreach (SqlParameter param in SqlParametros)
                    oCmd.Parameters.Add(param);

                SqlDataAdapter oDa = new SqlDataAdapter(oCmd);
                DataTable oDt = new DataTable();
                oDa.Fill(oDt);

                return oDt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

    }
}