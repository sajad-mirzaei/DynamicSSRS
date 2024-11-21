using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SampleWebForm.Startup))]
namespace SampleWebForm
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
