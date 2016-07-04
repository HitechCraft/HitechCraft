using System;
using log4net.Config;

namespace WebApplication
{
    #region Using Directives

    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using DAL.NHibernate;

    #endregion

    public class MvcApplication : System.Web.HttpApplication
    {
        protected SessionManager sessionManager;

        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // start log4net
            XmlConfigurator.Configure();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            sessionManager = new SessionManager();
            sessionManager.OpenSession();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            sessionManager = new SessionManager();
            sessionManager.CloseSession();
        }
    }
}
