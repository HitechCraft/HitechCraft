using System.Web.Script.Serialization;

namespace WebApplication.Core
{
    public static class JsonManager
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