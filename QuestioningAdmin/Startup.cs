using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuestioningAdmin.Startup))]
namespace QuestioningAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
