using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace PERMWebSolution.Models
{
    /// <summary>
    /// Session management class
    /// </summary>
    public class SessionManager
    {
        private string sessionID = "";
        private int userID;
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="uID">User ID</param>
        public SessionManager (int uID)
        {
            userID = uID;
        }

        public string getUserSession()
        {
            deleteOldSessions();
            deleteOldUserSession();
            createSession();

            return sessionID;

        }
        /// <summary>
        /// Deletes old sessions
        /// </summary>
        private void deleteOldSessions()
        {
            string sql = "DELETE FROM [UserSessions] WHERE [sessionExpires] > @CurrentDate";
            var param = new Dictionary<string, object>();
            param.Add("@CurrentDate", DateTime.Now);

            var result = DBContext.ExecuteQueryTable(sql, param);

        }
        /// <summary>
        /// Deletes old sessions
        /// </summary>
        private void deleteOldUserSession()
        {
            string sql = "DELETE FROM [UserSessions] WHERE [userId] = @userID";
            var param = new Dictionary<string, object>();
            param.Add("@userID", userID);

            var result = DBContext.ExecuteQueryTable(sql, param);

        }

       
        /// <summary>
        /// Saves session to the session table
        /// </summary>
        /// <param name="uID">user id</param>
        /// <returns>True if session saves succesfully</returns>
        private  bool createSession()
        {
            var now = DateTime.Now;
            now.AddDays(1);

            string sql = "INSERT INTO [userSession] VALUES (@userID, @sessionID, @expTime)";
            var param = new Dictionary<string, object>();
            param.Add("@userID", userID); param.Add("@sessionID", HelperMethods.generateRandomString()); param.Add("@expTime", now);

            var result = DBContext.ExecuteQueryTable(sql, param);
            return true;

           /* using (SqlConnection connection = new SqlConnection(DBContext.conString))
            {
                try
                {
                    string q = "INSERT INTO UserSessions VALUES (" + userID.ToString() + "," + sessionID + "," + now.ToString() + ");";
                    connection.Open();
                    SqlCommand command = new SqlCommand(q, connection);
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }*/
        }

        public bool checkSession()
        {
           // DataTable result = new DataTable();
            string sql = "SELECT [userSession] FROM [UserSessions] WHERE [userID] = @CurrentUserId";
            var param = new Dictionary<string, object>();
            param.Add("@CurrentUserId", userID);

            var result = DBContext.ExecuteQueryTable(sql, param);

            if (result.Rows.Count > 0)
            {
                sessionID = result.Rows[0][0].ToString();
                return true;
            }
            else
                return false;

        }
    }
}