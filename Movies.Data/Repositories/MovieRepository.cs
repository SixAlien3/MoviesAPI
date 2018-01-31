using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Data.Common;
using Movies.Models;

namespace Movies.Data.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        protected MovieRepository(MovieDbContext context) : base(context)
        {
        }


        public void InsertMovieWithGenres(Movie movie)
        {
            var genreIds = movie.Genres.Select(g => g.Id).ToList();
            movie.Genres = new List<Genre>();
            foreach (var genreId in genreIds)
            {
                Genre genre = new Genre() {Id = genreId};
                Context.Genres.Attach(genre);
                movie.Genres.Add(genre);
            }

            Context.Movies.Add(movie);
            Context.SaveChanges();
        }
    }

    public interface IMovieRepository : IRepository<Movie>
    {
        void InsertMovieWithGenres(Movie movie);
    }
}