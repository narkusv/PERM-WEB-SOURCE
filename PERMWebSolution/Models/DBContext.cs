using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Configuration;

namespace PERMWebSolution.Models
{
    /// <summary>
    /// Class for DB connection
    /// </summary>
    public static class DBContext
    {
        /// <summary>
        /// static variable holds connection string
        /// </summary>
        private static string conString = ConfigurationManager.ConnectionStrings["PERMLOCALServer"].ConnectionString;


        public static DataTable ExecuteQueryTable(string query)
        {
            return ExecuteQueryTable(query, null);
        }

        public static DataTable ExecuteQueryTable(string query, Dictionary<string, object> parameters)
        {
            DataTable tbl = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        if (parameters != null)
                        {
                            foreach (string parameter in parameters.Keys)
                            {
                                cmd.Parameters.AddWithValue(parameter, parameters[parameter]);
                            }
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(tbl);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null; // Console.WriteLine(ex.Message);
            }
            return tbl;
        }
    }
}