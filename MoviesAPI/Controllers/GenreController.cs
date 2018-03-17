using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Movies.Data.Repositories;
using MoviesAPI.Filters;

namespace MoviesAPI.Controllers
{
    [RoutePrefix("api/genres")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GenreController : ApiController
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;

        // GET: api/Genre
        public GenreController(IMovieRepository movieRepository, IGenreRepository genreRepository)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GellAllGenres()
        {
            var genres = _genreRepository.GetAll().OrderBy(g=>g.Name).ToList();
            var response = genres.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, genres)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Genres Found");
            return ResponseMessage(response) ;
        }
    }
}
