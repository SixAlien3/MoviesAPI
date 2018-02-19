using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesAPI.Infrastructure
{
    public class MovieApiConstants
    {
        public const string TmdbApiUri = "https://api.themoviedb.org/3/";
        public const string ApiVersion = "3";
        public const string ApiKey = "f260170a65522e5006559539ef75a2c2";

        public const string GetMovieDetails = "movie/{movieId}?";
        public const string GetNowPlayingMovies = "movie/now_playing?";
        public const string GetMovieVideos = "movie/{movieId}/videos?";
        public const string GetSimilarMovies = "movie/{movieId}/similar?";
        public const string GetUpComing = "movie/upcoming?";
        public const string GetRecommendations = "movie/{movieId}/recommendations?";
        public const string GetPopular = "movie/popular?";
        public const string GetImages = "movie/{movieId}/images?";
        public const string GetExternalIds = "movie/{movieId}/external_ids?";

        public const string GetPersonDetails = "person/{person_id}?";
        public const string GetPersonMovies = "person/{person_id}/movie_credits?";
        public const string GetPersonImages = "person/{person_id}/images?";
        public const string GetPopularPeople = "person/popular?";

        public const string GetEntityByImdbId = "find/{external_id}&external_source=imdb_id?";


        // movieis = 76338 personid = 74568
        // details https://api.themoviedb.org/3/movie/550?api_key=f260170a65522e5006559539ef75a2c2
        // now playing https://api.themoviedb.org/3/movie/now_playing?api_key=f260170a65522e5006559539ef75a2c2&language=en-US&page=1
        // popular https://api.themoviedb.org/3/movie/popular?api_key=f260170a65522e5006559539ef75a2c2&language=en-US&page=1
        // recomendations https://api.themoviedb.org/3/movie/76338/recommendations?api_key=f260170a65522e5006559539ef75a2c2&language=en-US&page=1
        // upcoming https://api.themoviedb.org/3/movie/upcoming?api_key=f260170a65522e5006559539ef75a2c2&language=en-US&page=1
        // similar https://api.themoviedb.org/3/movie/76338/similar?api_key=f260170a65522e5006559539ef75a2c2&language=en-US&page=1
        // videos https://api.themoviedb.org/3/movie/76338/videos?api_key=f260170a65522e5006559539ef75a2c2&language=en-US
        // images https://api.themoviedb.org/3/movie/284053/images?api_key=f260170a65522e5006559539ef75a2c2
        // externalIds https://api.themoviedb.org/3/movie/76338/external_ids?api_key=f260170a65522e5006559539ef75a2c2

        // personDetails https://api.themoviedb.org/3/person/74568?api_key=f260170a65522e5006559539ef75a2c2&language=en-US
        // person Movies https://api.themoviedb.org/3/person/74568/movie_credits?api_key=f260170a65522e5006559539ef75a2c2&language=en-US
        // person images https://api.themoviedb.org/3/person/74568/images?api_key=f260170a65522e5006559539ef75a2c2
        // popular people https://api.themoviedb.org/3/person/popular?api_key=f260170a65522e5006559539ef75a2c2&language=en-US&page=1

        // Get Entity by imdbId https://api.themoviedb.org/3/find/tt3501632?api_key=f260170a65522e5006559539ef75a2c2&language=en-US&external_source=imdb_id
    }
}