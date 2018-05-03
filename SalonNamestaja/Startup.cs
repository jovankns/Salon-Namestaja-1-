using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SalonNamestaja.Startup))]
namespace SalonNamestaja
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
