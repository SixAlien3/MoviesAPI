using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.AspNet.Identity;
using Movies.Data.Infrastructure;
using Movies.Data.Repositories;
using MoviesAPI.Infrastructure;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    //[RoutePrefix("api/token")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TokenController : ApiController
    {
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly IUserRepository _userRepository;

        public TokenController(ApplicationUserManager appUserManager, ApplicationRoleManager appRoleManager,
            IUserRepository userRepository)
        {
            _applicationUserManager = appUserManager;
            _userRepository = userRepository;
        }

        [Route("api/token")]
        [HttpPost]
        public async Task<IHttpActionResult> ValidateUserSendToken(LoginModel model)
        {
            var user = await _userRepository.FindByUserAsync(model.Username, model.Password);
            if (user == null)
            {
                return Content(HttpStatusCode.Unauthorized, "Invalid username or password");
            }
            var identity = _applicationUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            return Ok(JwtManager.GenerateToken(user, identity));
        }
    }
}
