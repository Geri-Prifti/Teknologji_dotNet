using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projekt_Teknologji_dotNet.Startup))]
namespace Projekt_Teknologji_dotNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
