using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Data.Common;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using Genre = Movies.Models.Genre;
using Movie = Movies.Models.Movie;

namespace Movies.Data.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private readonly TMDbClient _client;
        private const string TmdbApiKey = "f260170a65522e5006559539ef75a2c2";

        public MovieRepository(MovieDbContext context) : base(context)
        {
            _client = new TMDbClient(TmdbApiKey) {DefaultCountry = "us"};
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

        public async Task<SearchContainerWithDates<SearchMovie>> GetNowPlaying()
        {
            var movies = await _client.GetMovieNowPlayingListAsync(region: "us");
            return movies;
        }

        public async Task<SearchContainerWithDates<SearchMovie>> GetUpComing()
        {
            var movies = await _client.GetMovieUpcomingListAsync(region:"us");
            return movies;
        }

        public async Task<SearchContainer<SearchMovie>> GetTopRatedMovies()
        {
            var movies = await _client.GetMovieTopRatedListAsync(region: "us");
            return movies;
        }

        public async Task<SearchContainer<SearchMovie>> GetPopularMovies()
        {
            var movies = await _client.GetMoviePopularListAsync(region: "us");
            return movies;
        }

        public async Task<TMDbLib.Objects.Movies.Movie> GetMovieDetailsFromTmdb(int tmdbId)
        {
            var movie = await _client.GetMovieAsync(tmdbId, TMDbLib.Objects.Movies.MovieMethods.Keywords | TMDbLib.Objects.Movies.MovieMethods.Similar | TMDbLib.Objects.Movies.MovieMethods.Videos |
                                                            TMDbLib.Objects.Movies.MovieMethods.Images);
            return movie;
        }

        public async Task<ResultContainer<Video>> GetMovieVideosAsync(int tmdbId)
        {
            var movie = await _client.GetMovieVideosAsync(tmdbId);
            return movie;
        }

        public async Task<Credits> GetCastsForMovie(int tmdbId)
        {
            var credits = await _client.GetMovieCreditsAsync(tmdbId);
            return credits;
        }
    }

    public interface IMovieRepository : IRepository<Movie>
    {
        void InsertMovieWithGenres(Movie movie);
        Task<SearchContainerWithDates<SearchMovie>> GetNowPlaying();
        Task<SearchContainerWithDates<SearchMovie>> GetUpComing();
        Task<SearchContainer<SearchMovie>> GetTopRatedMovies();
        Task<SearchContainer<SearchMovie>> GetPopularMovies();
        Task<TMDbLib.Objects.Movies.Movie> GetMovieDetailsFromTmdb(int tmdbId);
        Task<ResultContainer<Video>> GetMovieVideosAsync(int tmdbId);
        Task<TMDbLib.Objects.Movies.Credits> GetCastsForMovie(int tmdbId);
    }
}