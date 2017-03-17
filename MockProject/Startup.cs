using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MockProject.Startup))]
namespace MockProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
