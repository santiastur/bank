using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GetDataFromBank.Startup))]
namespace GetDataFromBank
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
