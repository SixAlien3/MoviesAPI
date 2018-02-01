using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MoviesAPI.Startup))]

namespace MoviesAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}