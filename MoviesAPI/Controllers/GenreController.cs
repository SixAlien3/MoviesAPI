using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Movies.Data.Repositories;

namespace MoviesAPI.Controllers
{
    public class GenreController : ApiController
    {
        private readonly IMovieRepository _movieRepository;

        // GET: api/Genre
        public GenreController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Genre/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Genre
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Genre/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Genre/5
        public void Delete(int id)
        {
        }
    }
}
