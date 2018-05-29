using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AsociatieProprietari.Startup))]
namespace AsociatieProprietari
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
