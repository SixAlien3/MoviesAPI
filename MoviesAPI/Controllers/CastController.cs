using System.Web.Http;
using System.Web.Http.Cors;
using Movies.Data.Repositories;

namespace MoviesAPI.Controllers
{
    [RoutePrefix("api/cast")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CastController : BaseApiController
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
