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

        
    }
}
