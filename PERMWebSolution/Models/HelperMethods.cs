using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PERMWebSolution.Models
{
    public static class HelperMethods
    {
        /// <summary>
        /// Method for generating new session id
        /// </summary>
        /// <returns>string sessionID</returns>
        public static string generateRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return  new string(Enumerable.Repeat(chars, 24)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        /// <summary>
        /// Encrypts string with SHA256 algorithm
        /// </summary>
        /// <param name="value">String to be encrypted</param>
        /// <returns>SHA256 encrypted string</returns>
        public static string sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}