using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Movies.Data.Common;
using Movies.Models;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Genre = Movies.Models.Genre;

namespace Movies.Data.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private const string TmdbApiKey = "f260170a65522e5006559539ef75a2c2";

        public MovieRepository(MovieDbContext context) : base(context)
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

        public async Task<IList<Movie>> GetNowPlaying()
        {
            var client = new TMDbClient(TmdbApiKey);
            var movies = await client.GetMovieNowPlayingListAsync();

            var tmdbMovies = movies.Results;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<SearchMovie, Movie>());
            IMapper iMapper = config.CreateMapper();
            var source = tmdbMovies;
            var destination = iMapper.Map<IList<SearchMovie>, IList<Movie>>(source);

            return destination;
        }
    }

    public interface IMovieRepository : IRepository<Movie>
    {
        void InsertMovieWithGenres(Movie movie);
        Task<IList<Movie>> GetNowPlaying();
    }
}