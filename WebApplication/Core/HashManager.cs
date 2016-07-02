namespace WebApplication.Managers
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class HashManager
    {
        /// <summary>
        /// Method returns hash md5
        /// </summary>
        /// <param name="inputString">Input string</param>
        /// <returns></returns>
        public static string GetMd5Hash(string inputString)
        {
            return BuildMd5(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(inputString)));
        }

        /// <summary>
        /// Method returns hash md5
        /// </summary>
        /// <param name="bytes">Bytes for hash</param>
        /// <returns></returns>
        public static string GetMd5Hash(byte[] bytes)
        {
            return BuildMd5(MD5.Create().ComputeHash(bytes));
        }

        /// <summary>
        /// Md5 bytes
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        internal static byte[] GetMd5Bites(string inputString)
        {
            return MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        /// <summary>
        /// Returns base64 hash of string
        /// </summary>
        /// <param name="str">String</param>
        /// <returns></returns>
        internal static string GetBase64Hash(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Returns bas64 hash of bytes
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns></returns>
        internal static string GetBase64Hash(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
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

        #region Private Methods

        private static string BuildMd5(byte[] data)
        {
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        #endregion
    }
}