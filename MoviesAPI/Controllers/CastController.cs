using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Movies.Data.Repositories;
using Movies.Models;
using MoviesAPI.Models;
using TMDbLib.Objects.General;

namespace MoviesAPI.Controllers
{
    [RoutePrefix("api/cast")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CastController : ApiController
    {
        private readonly ICastRepository _castRepository;
        private readonly IUserRepository _userRepository;

        public CastController(IUserRepository userRepository, ICastRepository castRepository)
        {
            _castRepository = castRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("movie/{tmdbId:int}")]
        public async Task<IHttpActionResult> GetMovieVideosByTmdbId(int tmdbId)
        {
            var credits = await _castRepository.GetCastsForMovie(tmdbId);
            var response = credits != null
                ? Request.CreateResponse(HttpStatusCode.OK,
                    AutoMapper.Mapper.Map<TMDbLib.Objects.Movies.Credits, Credits>(credits))
                : Request.CreateResponse(HttpStatusCode.NotFound, "No casts on tmdb was found");

            return ResponseMessage(response);
        }
    }
}
