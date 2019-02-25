using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DauGiaTrucTuyen.Startup))]
namespace DauGiaTrucTuyen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
