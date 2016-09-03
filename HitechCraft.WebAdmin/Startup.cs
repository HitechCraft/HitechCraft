using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HitechCraft.WebAdmin.Startup))]
namespace HitechCraft.WebAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
