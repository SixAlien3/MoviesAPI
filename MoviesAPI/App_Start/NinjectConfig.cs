using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Movies.Data.Common;
using Movies.Data.Repositories;
using Movies.Models;
using Ninject;

namespace MoviesAPI
{
    public class NinjectConfig
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            //Create the bindings
           // kernel.Load(Assembly.GetExecutingAssembly());


            kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>();
            kernel.Bind<UserManager<ApplicationUser>>().ToSelf();
            kernel.Bind<IRoleStore<IdentityRole, string>>().To<RoleStore<IdentityRole>>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IMovieRepository>().To<MovieRepository>();
            kernel.Bind<IGenreRepository>().To<GenreRepository>();
            kernel.Bind<IFavoriteRepository>().To<FavoriteRepository>();
            kernel.Bind<ICastRepository>().To<CastRepository>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();


            //kernel.Bind(typeof(IUserStore<>)).To(typeof(UserStore<>)).InRequestScope();
            kernel.Bind<DbContext>().To<MovieDbContext>();
            return kernel;
        }
    }
}