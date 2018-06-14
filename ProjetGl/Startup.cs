using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjetGl.Startup))]
namespace ProjetGl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
