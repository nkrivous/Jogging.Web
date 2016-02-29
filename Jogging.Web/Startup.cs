using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Jogging.Web.Startup))]

namespace Jogging.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
