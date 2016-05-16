namespace WebApplication.Managers
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    
    public static class Md5Manager
    {
        /// <summary>
        /// Method returns hash md5
        /// </summary>
        /// <param name="md5Hash">Md5 hash instance</param>
        /// <param name="input">Input string</param>
        /// <returns></returns>
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            
            return sBuilder.ToString();
        }

        /// <summary>
        /// Verify md5 hash input string
        /// </summary>
        /// <param name="md5Hash">Md5 hash instance</param>
        /// <param name="input">Input string</param>
        /// <param name="hash">Md5 hash of Input string</param>
        /// <returns></returns>
        public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            string hashOfInput = GetMd5Hash(md5Hash, input);
            
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }

        /// <summary>
        /// Get uuid from input string
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns></returns>
        public static string UuidFromString(string input)
        {
            return GetMd5Hash(MD5.Create(), input);
        }
    }
}