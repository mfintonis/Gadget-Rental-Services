using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gadget_Rental_Services___Web_Forms.Startup))]
namespace Gadget_Rental_Services___Web_Forms
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
