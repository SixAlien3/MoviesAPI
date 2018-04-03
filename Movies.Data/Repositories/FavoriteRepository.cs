using Movies.Data.Common;
using Movies.Models;

namespace Movies.Data.Repositories
{
    public class FavoriteRepository : Repository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(MovieDbContext context) : base(context)
        {
        }
    }

    public interface IFavoriteRepository : IRepository<Favorite>
    {
    }
}