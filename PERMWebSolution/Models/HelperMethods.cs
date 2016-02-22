using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PERMWebSolution.Models
{
    public static class HelperMethods
    {
        /// <summary>
        /// Method for generating new session id
        /// </summary>
        /// <returns>string sessionID</returns>
        public static string generateSessionID()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return  new string(Enumerable.Repeat(chars, 24)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}