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
    [JwtAuthentication(Roles = "User,SuperAdmin,Admin")]
    [RoutePrefix("api/Customer")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class CustomerController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public CustomerController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

       
    }
}
