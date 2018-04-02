using Movies.Data.Common;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;

namespace Movies.Data.Repositories
{
    public class CastRepository : Repository<Models.Cast>, ICastRepository
    {
        private readonly TMDbClient _client;
        private const string TmdbApiKey = "f260170a65522e5006559539ef75a2c2";

        protected CastRepository(MovieDbContext context) : base(context)
        {
            _client = new TMDbClient(TmdbApiKey) { DefaultCountry = "us" };
        }

        public Task<TMDbLib.Objects.Movies.Movie> GetCastForMovie(int tmdbId)
        {
            throw new NotImplementedException();
        }
    }

    public interface ICastRepository : IRepository<Models.Cast>
    {
        Task<TMDbLib.Objects.Movies.Movie> GetCastForMovie(int tmdbId);

    }
}
