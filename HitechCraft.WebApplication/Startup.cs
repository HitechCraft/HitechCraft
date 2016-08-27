using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HitechCraft.WebApplication.Startup))]
namespace HitechCraft.WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
