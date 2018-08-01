using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesAPI.Models
{
    public class FavoriteModel
    {
        public string UserName { get; set; }
        public int MovieId { get; set; }
    }
}