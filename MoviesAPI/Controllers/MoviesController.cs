using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Movies.Data.Repositories;
using Movies.Models;
using MoviesAPI.Filters;
using MoviesAPI.Infrastructure;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MoviesAPI.Controllers
{
    [RoutePrefix("api/movies")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[JwtAuthentication]
    public class MoviesController : ApiController
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUserRepository _userRepository;

        public MoviesController(IUserRepository userRepository, IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("{page:int?}")]
        public IHttpActionResult GetAllMovies(int? page = 1, string title = "")
        {
            const int pageSize = 24;
            int skip;

            if (page.HasValue && page > 1)
            {
                skip = (page.Value - 1) * pageSize;
            }
            else
            {
                skip = 0;
            }

            Expression<Func<Movie, bool>> filter = movie => movie.Title.Contains(title);

            var movies =
                _movieRepository.GetQueryableData(out int totalCount, filter, OrderBy, "Genres", skip, pageSize);
            var response = movies.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, movies)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Movies Found");
            response.Headers.Add("X-Total-Count", totalCount.ToString());

            return ResponseMessage(response);
        }


        [HttpGet]
        [Route("genre/{genreid}/{page:int?}")]
        public IHttpActionResult GetAllMoviesBygenre(int genreId, int? page = 1)
        {
            var totalCount = 0;
            const int pageSize = 24;
            int skip;
            if (page.HasValue && page > 1)
            {
                skip = (page.Value - 1) * pageSize;
            }
            else
            {
                skip = 0;
            }

            totalCount = _movieRepository.GetQueryable().Count(m => m.Genres.Any(g => g.Id == genreId));
            var movies = totalCount > 0
                ? _movieRepository.GetQueryable().Where(m => m.Genres.Any(g => g.Id == genreId)).Include(b => b.Genres)
                    .OrderByDescending(o => o.Popularity)
                    .Skip(skip).Take(pageSize)
                : null;
            var response = movies != null && movies.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, movies)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Movies Found");
            response.Headers.Add("X-Total-Count", totalCount.ToString());

            return ResponseMessage(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage GetMovieById(int id)
        {
            var movie = _movieRepository.FindByInclude(m => m.Id == id, m => m.Genres).FirstOrDefault();
            var response = movie != null
                ? Request.CreateResponse(HttpStatusCode.OK, movie)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Movie was found");
            return response;
        }

        [HttpGet]
        [Route("details/{tmdbId:int}")]
        public async Task<IHttpActionResult> GetMovieByTmdbId(int tmdbId)
        {
            var movie = await _movieRepository.GetMovieDetailsFromTmdb(tmdbId);
            var response = movie != null
                ? Request.CreateResponse(HttpStatusCode.OK, AutoMapper.Mapper.Map<TMDbLib.Objects.Movies.Movie, Movie>(movie))
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Movie on tmdb was found");
            
            return ResponseMessage(response);
        }

        [HttpGet]
        [Route("nowplaying")]
        public async Task<IList<Movie>> GetNowPlayingMovies()
        {
            var tmdbMovies = await _movieRepository.GetNowPlaying();
            var movies = AutoMapper.Mapper.Map<IList<SearchMovie>, IList<Movie>>(tmdbMovies.Results);
            return movies;
        }

        [HttpGet]
        [Route("upcoming")]
        public async Task<IList<Movie>> GetUpcomingMovies()
        {
            var tmdbMovies = await _movieRepository.GetUpComing();
            var movies = AutoMapper.Mapper.Map<IList<SearchMovie>, IList<Movie>>(tmdbMovies.Results);
            return movies;
        }

        [HttpGet]
        [Route("top")]
        public async Task<IList<Movie>> GetTopMovies()
        {
            var tmdbMovies = await _movieRepository.GetTopRatedMovies();
            var movies = AutoMapper.Mapper.Map<IList<SearchMovie>, IList<Movie>>(tmdbMovies.Results).Where(m => m.OriginalLanguage == "en").ToList();
            
            return movies;
        }
        [HttpGet]
        [Route("popular")]
        public async Task<IList<Movie>> GetPopularMovies()
        {
            var tmdbMovies = await _movieRepository.GetPopularMovies();
            var movies = AutoMapper.Mapper.Map<IList<SearchMovie>, IList<Movie>>(tmdbMovies.Results).Where(m => m.OriginalLanguage == "en").ToList();
            
            return movies;
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post()
        {
            throw new NotImplementedException("This method is not implemented");
        }

        private static IOrderedQueryable<Movie> OrderBy(IQueryable<Movie> queryable)
        {
            return queryable.OrderByDescending(b => b.Popularity);
        }
    }
}