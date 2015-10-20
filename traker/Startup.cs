using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(traker.Startup))]
namespace traker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
