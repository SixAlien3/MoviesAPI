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
using MoviesAPI.Filters;

namespace MoviesAPI.Controllers
{
    [RoutePrefix("api/movies")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [JwtAuthentication]
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
            int totalCount = 0;
            const int pageSize = 25;
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

            var movies = _movieRepository.GetQueryableData(out totalCount, filter, OrderBy, "Genres", skip, pageSize);
            var response = movies.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, movies)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Movies Found");
            response.Headers.Add("X-total-count", totalCount.ToString());

            return ResponseMessage(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage GetMovieById(int id)
        {
            var movie = _movieRepository.GetById(id);

            var response = movie != null
                ? Request.CreateResponse(HttpStatusCode.OK, movie)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Movie was found");
            return response;
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post()
        {
            throw new NotImplementedException("This method is not implemented");
        }

        private IOrderedQueryable<Movie> OrderBy(IQueryable<Movie> queryable)
        {
            return queryable.OrderByDescending(b => b.Popularity);
        }
    }
}