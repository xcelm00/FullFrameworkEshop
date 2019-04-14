using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FullFrameworkEshop.Startup))]
namespace FullFrameworkEshop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
