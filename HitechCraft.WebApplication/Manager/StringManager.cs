namespace HitechCraft.DAL.Domain.Extentions
{
    #region Using Directives

    using System;
    using System.Linq;

    #endregion

    public static class StringManager
    {
        public static string Limit(this string str, int stringLength)
        {
            var words = str.Split(' ').ToList();
            var newString = String.Empty;

            foreach (var word in words)
            {
                if (newString.Length < stringLength) newString += word + " ";
                else return newString;
            }

            return newString;
        }
    }
}