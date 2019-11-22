using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EnterwellBills.Startup))]
namespace EnterwellBills
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
