using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace PERMWebSolution.Models
{
    public class User
    {
        public int userID { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }
        public string salt { get; set; }
        public string sessionID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        //keep reference to user at class level
        User user = null;

        /// <summary>
        /// Method checks if user is registered.
        /// </summary>
        /// <param name="user">user data from POST request</param>
        /// <returns>On success returns session</returns>
        public bool logInUser(User user)
        {
           // this.user.MemberwiseClone();
            this.user = (User)user.MemberwiseClone();

            if (isUserRegistered())
            {
               // this.user.userID = 1;
                SessionManager sM = new SessionManager(this.user.userID);
                sessionID = sM.getUserSession();
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Checks user credentials in database
        /// </summary>
        /// <returns>True if user exists</returns>
        private bool isUserRegistered()
        {
            string sql = "SELECT * FROM [User] WHERE [Id] = @uID";
            var param = new Dictionary<string, object>();
            param.Add("@uID", user.userID);

            var result = DBContext.ExecuteQueryTable(sql, param);

            if (result.Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// User add method
        /// </summary>
        /// <param name="user">variable type of user</param>
        /// <returns>bool result if user sucessfully saved in database</returns>
        public string saveUser(User user)
        {
            string sql = "SELECT * FROM [User] WHERE [userName] = @userName";
            var param = new Dictionary<string, object>();
            param.Add("@userName", user.userName);
            param.Add("@userPasswd",);
            param.Add("@salt", );
            param.Add("@name",);
            param.Add("@surname", );

            var result = DBContext.ExecuteQueryTable(sql, param);
            if (result.Rows.Count == 0) //meens no such user thus we can save it
            {
                sql = "INSERT INTO [User] (userName, userPassword, salt, name, surname) VALUES (@userID, @sessionID, @expTime)";
            }
            string result = ""; //mocup data needs to be implemented           

            return result;
        }
    }
}