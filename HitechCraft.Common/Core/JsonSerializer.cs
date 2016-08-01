namespace HitechCraft.Common.Core
{
    #region Using Directives

    using System.Web.Script.Serialization;

    #endregion

    public static class JsonSerializer
    {
        public static T Deserialize<T>(string value)
        {
            return new JavaScriptSerializer().Deserialize<T>(value);
        }

        public static string Serialize(object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }
    }
}