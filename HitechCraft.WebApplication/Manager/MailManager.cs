using System.Web.Configuration;

namespace HitechCraft.WebApplication.Manager
{
    using System.Text;

    public static class MailManager
    {
        private static string _mailBody;

        static MailManager()
        {
            StringBuilder sb = new StringBuilder();

            #region Styles

            sb.Append("<style type='text/css'>");

            sb.Append("a { text-decoration: none; color: #1bffd5; }");
            sb.Append("a:hover { text-decoration: none; color: #bcff1b; }");

            sb.Append("</style>");

            #endregion
            
            sb.Append("<div style='background: url(http://"+ WebConfigurationManager.AppSettings["domain"] + "/Content/Images/mailbg.jpg) no-repeat; width: 753px; height: 545px; background-size: auto 100%; background-position-x: 50%; margin: 0 auto; padding: 30px;'>");
            sb.Append("<div style='background: url(http://" + WebConfigurationManager.AppSettings["domain"] + "/Content/Images/logo.png) no-repeat; width: 465px; height: 66px; margin: 0 auto; margin-top: 30px;'></div>");
            sb.Append("<div style='width: 100%; height: 70%; background: rgba(67, 228, 221, 0.38); margin-top: 35px; border: 2px solid;position: relative;'>");
            sb.Append("<p style='margin: 21px; font-size: 22px; color: #fff; font-weight: bold; text-shadow: 2px 0 0 #000, -2px 0 0 #000, 0 2px 0 #000, 0 -2px 0 #000, 1px 1px #000, -1px -1px 0 #000, 1px -1px 0 #000, -1px 1px 0 #000;'>");

            sb.Append("Доброго времени суток! ");

            sb.Append("[body]");

            sb.Append("</p>");
            sb.Append("<p style='position: absolute; text-shadow: 2px 0 0 #000, -2px 0 0 #000, 0 2px 0 #000, 0 -2px 0 #000, 1px 1px #000, -1px -1px 0 #000, 1px -1px 0 #000, -1px 1px 0 #000; color: #fff; text-align: justify; margin: 25px; bottom: 0;'>");
            sb.Append("Данное письмо было отправлено с проекта HitechCraft - Multiserver Complex, так как вы указали свой email. Если это не имеет к вам никакого отношения - просто проигнорируйте это письмо!");
            sb.Append("</p>");
            sb.Append("</div>");
            sb.Append("</div>");

            _mailBody = sb.ToString();
        }

        public static string GetMail(string message)
        {
            return _mailBody.Replace("[body]", message);
        }
    }
}