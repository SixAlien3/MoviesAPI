using Movies.Data.Common;
using Movies.Models;

namespace Movies.Data.Repositories
{
    public class GenreRepository: Repository<Genre>, IGenreRepository
    {
        public GenreRepository(MovieDbContext context) : base(context)
        {
        }
    }

   public interface IGenreRepository: IRepository<Genre>
    {
        
    }
}
