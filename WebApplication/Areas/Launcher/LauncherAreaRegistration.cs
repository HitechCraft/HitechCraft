using System.Web.Mvc;

namespace WebApplication.Areas.Launcher
{
    public class LauncherAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Launcher";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Launcher_default",
                "Launcher/{controller}/{action}/{id}",
                new { controller = "Launcher", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}