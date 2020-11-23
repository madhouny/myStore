using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(myStore.Startup))]
namespace myStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
