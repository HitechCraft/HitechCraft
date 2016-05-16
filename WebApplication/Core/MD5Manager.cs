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
        /// Get uuid from input string (ебать говнокод, но хуле делать?)
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns></returns>
        public static string UuidFromString(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            hash[6] &= 0x0f;
            hash[6] |= 0x30;
            hash[8] &= 0x3f;
            hash[8] |= 0x80;

            string hex = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

            return hex.Insert(8, "-").Insert(13, "-").Insert(18, "-").Insert(23, "-");
        }
        
        /// <summary>
        /// Get hex string without "-" chars
        /// </summary>
        /// <param name="hex">Uuid hex</param>
        /// <returns></returns>
        public static string StringFromUuid(string hex)
        {
            return hex.Replace(@"-", String.Empty);
        }
    }
}