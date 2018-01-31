using System.Data.Entity;
using System.Web.Http;
using Movies.Data.Common;
using Movies.Data.Infrastructure;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using Swashbuckle.Application;

namespace MoviesAPI
{
    public partial class Startup
    {
        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            var webApiConfiguration = new HttpConfiguration();

            webApiConfiguration.EnableSwagger();

            WebApiConfig.Register(webApiConfiguration);

            app.CreatePerOwinContext(MovieDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            Database.SetInitializer<MovieDbContext>(null);
            // SwaggerConfig.Register(webApiConfiguration);

            app.UseNinjectMiddleware(NinjectConfig.CreateKernel);
            app.UseNinjectWebApi(webApiConfiguration);
        }
    }
}