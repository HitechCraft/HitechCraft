namespace HitechCraft.DAL.Domain.Extentions
{
    using System;
    using System.Linq;

    public static class StringExtentions
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
