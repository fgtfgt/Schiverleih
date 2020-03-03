using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shiverleih.Startup))]
namespace Shiverleih
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
