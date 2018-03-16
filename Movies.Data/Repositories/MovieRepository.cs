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

        public async Task<SearchContainerWithDates<SearchMovie>> GetNowPlaying()
        {
            var client = new TMDbClient(TmdbApiKey);
            var movies = await client.GetMovieNowPlayingListAsync();
            return movies;
        }

        public async Task<SearchContainerWithDates<SearchMovie>> GetUpComing()
        {
            var client = new TMDbClient(TmdbApiKey);
            var movies = await client.GetMovieUpcomingListAsync();
            return movies;
        }

        public async Task<SearchContainer<SearchMovie>> GetTopRatedMovies()
        {
            var client = new TMDbClient(TmdbApiKey);
            var movies = await client.GetMovieTopRatedListAsync();
            return movies;
        }
        public async Task<SearchContainer<SearchMovie>> GetPopularMovies()
        {
            var client = new TMDbClient(TmdbApiKey);
            var movies = await client.GetMoviePopularListAsync();
            return movies;
        }

        public async Task<TMDbLib.Objects.Movies.Movie> GetMovieDetailsFromTmdb(int tmdbId)
        {
            var client = new TMDbClient(TmdbApiKey);
            var movie = await client.GetMovieAsync(tmdbId);
            return movie;
        }

        public async Task<ResultContainer<Video>> GetMovieVideosAsync(int tmdbId)
        {
            var client = new TMDbClient(TmdbApiKey);
            var movie = await client.GetMovieVideosAsync(tmdbId);
            return movie;
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
    }
}