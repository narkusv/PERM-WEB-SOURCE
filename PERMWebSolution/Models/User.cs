using System.Collections.Generic;

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
     /*   public bool logInUser(User user) //------------------------ not yet implemented
        {
           // this.user.MemberwiseClone();
            this.user = (User)user.MemberwiseClone();
            if (true)
            {
               // this.user.userID = 1;
                SessionManager sM = new SessionManager(this.user.userID);
                sessionID = sM.getUserSession();
                return true;
            }
            else
                return false;
        }*/
        /// <summary>
        /// Checks userName is already present in database
        /// </summary>
        /// <returns>True if username i.e. email is already used</returns>
        public bool isUserNameExists(string uName)
        {
            string sql = "SELECT [userName] FROM [User] WHERE UPPER([userName]) = UPPER(@uName)"; //compare strings regardless string case
            var param = new Dictionary<string, object>();
            param.Add("@uName", uName);

            var result = DBContext.ExecuteQueryTable(sql, param);

            if (result != null && result.Rows.Count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// User add method
        /// </summary>
        /// <param name="user">variable type of user</param>
        /// <returns>bool result if user sucessfully saved in database</returns>
        public bool saveUser(User user)
        {
            string sql = "SELECT * FROM [User] WHERE UPPER([userName]) = UPPER(@userName)";
            var param = new Dictionary<string, object>();
            param.Add("@userName", user.userName);

            var result = DBContext.ExecuteQueryTable(sql, param);

            if (result != null && result.Rows.Count == 0) //DB returs result and if no such user found (count rows in tble is 0) then we can save it
            {
                param.Clear(); //remove all entries from Dictionary
                var salt = HelperMethods.generateRandomString(); //get random string from hgelper method
                var passwd = user.userPassword + salt; //ad random sting to user password
                var encrypted = HelperMethods.sha256Encrypt(passwd); //hash user passwod and pass to db - further we would not know old password, to reset - neet to generate new one

                sql = "INSERT INTO [User] (userName, userPassword, salt, name, surname) VALUES (@userName, @userPasswd, @salt, @name, @surname)";

                param.Add("@userName", user.userName);
                param.Add("@userPasswd", encrypted);
                param.Add("@salt", salt);
                param.Add("@name", user.Name);
                param.Add("@surname", user.Surname);
                              
                result = DBContext.ExecuteQueryTable(sql, param); // run insert
                if (result != null)
                    return true; //if not null then succeeded
            }         
            return false; //else just return false -  something went wrong while saving in database
        }
    }
}