using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Perchero.Startup))]
namespace Perchero
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
