using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Movies.Data.Repositories;
using Movies.Models;

namespace MoviesAPI.Controllers
{
    [RoutePrefix("api/movies")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MoviesController : ApiController
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUserRepository _userRepository;
        public MoviesController( IUserRepository userRepository, IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("{page:int?}")]
        public HttpResponseMessage GetAllMovies(int? page = 0, string title = "")
        {
            int totalCount = 0;
            int pageSize = 25;
            int skip;
            if (page.HasValue && page > 1)
            {
                skip = page.Value * pageSize;
            }
            else
            {
                skip = 0;
            }

            Expression<Func<Movie, bool>> filter = movie => movie.Title.Contains(title);

            var movies = _movieRepository.GetQueryableData(out totalCount, filter, OrderBy, null, skip,
                25);
            var response = movies.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, movies)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Movies Found");
            return response;
        }

        private IOrderedQueryable<Movie> OrderBy(IQueryable<Movie> queryable)
        {
            return queryable.OrderByDescending(b => b.Popularity);
        }
    }
}