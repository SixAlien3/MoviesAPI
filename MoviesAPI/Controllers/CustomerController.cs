using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Movies.Data.Repositories;
using MoviesAPI.Filters;

namespace MoviesAPI.Controllers
{
    [RoutePrefix("api/Customers")]
    [JwtAuthentication]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomerController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IFavoriteRepository _favoriteRepository;

        public CustomerController(IUserRepository userRepository, IMovieRepository movieRepository,
            IFavoriteRepository favoriteRepository)
        {
            _userRepository = userRepository;
            _movieRepository = movieRepository;
            _favoriteRepository = favoriteRepository;
        }

        [HttpGet]
        [Route("{userid}/movies")]
        public IHttpActionResult GetAllFavoriteMoviesByCustomer(string userid)
        {
            var favoriteMovies =
                _favoriteRepository.GetQueryable().Where(f => f.CustomerId == userid).Include(f => f.Movie)
                    .Include(f => f.Movie.Genres);

            var response = favoriteMovies.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, favoriteMovies)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Favorite Movies Found for this Customer");
            response.Headers.Add("X-Total-Count", favoriteMovies.Count().ToString());

            return ResponseMessage(response);
        }
    }
}