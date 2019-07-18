using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EHECD.FirePatrolInspection.Web.Startup))]
namespace EHECD.FirePatrolInspection.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
