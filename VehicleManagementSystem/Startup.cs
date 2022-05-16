using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VehicleManagementSystem.Startup))]
namespace VehicleManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
