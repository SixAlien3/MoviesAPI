using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Transactions;
using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movies.Data.Common;
using Movies.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MoviesAPI.Tests
{
    [TestClass]
    public class MoviesInitialDataTest
    {
        //  [TestMethod]
        public void PopulateInitialMoviesTableData()
        {
            TextReader readFile = new StreamReader(@"C:\Users\Abhil\Desktop\movies_metadata.csv");
            var csv = new CsvReader(readFile);
            csv.Configuration.BadDataFound = null;
            csv.Configuration.ReadingExceptionOccurred = null;
            var moviesFromCsv = csv.GetRecords<dynamic>();


            using (var scope = new TransactionScope())
            {
                Database.SetInitializer<MovieDbContext>(null);

                using (var db = new MovieDbContext())
                {
                    int totalMovies = 0;
                    db.Configuration.AutoDetectChangesEnabled = false;
                    foreach (var mov in moviesFromCsv)
                    {
                        //  totalMovies++;
                        decimal budget;
                        decimal revenue;
                        int runtime;
                        decimal avgVote;
                        int id;
                        decimal popularity;
                        int voteCount;

                        var movie = new Movie();
                        movie.ExternalId = Int32.TryParse(mov.id, out id) ? id : 0;
                        movie.OriginalTitle = mov.original_title;
                        movie.Title = mov.title;
                        movie.Tagline = mov.tagline;
                        movie.AverageVote = Decimal.TryParse(mov.vote_average, out avgVote) ? avgVote : 0;
                        movie.Budget = Decimal.TryParse(mov.budget, out budget) ? budget : 0;
                        movie.WebsiteUrl = mov.homepage;
                        movie.Revenue = Decimal.TryParse(mov.revenue, out revenue) ? revenue : 0;
                        movie.RunTime = Int32.TryParse(mov.runtime, out runtime) ? runtime : 0;
                        movie.OriginalLanguage = mov.original_language;
                        movie.ReleaseDate = string.IsNullOrWhiteSpace(mov.release_date)
                            ? DateTime.Now
                            : DateTime.Parse(mov.release_date);
                        movie.Popularity = Decimal.TryParse(mov.popularity, out popularity) ? popularity : 0;
                        movie.Overview = mov.overview;
                        movie.VoteCount = Int32.TryParse(mov.vote_count, out voteCount) ? voteCount : 0;
                        movie.IsReleased = true;
                        movie.PosterUrl = "http://image.tmdb.org/t/p/w342/" + mov.poster_path;
                        movie.ImdbId = "http://www.imdb.com/title/" + mov.imdb_id;
                        db.Movies.Add(movie);
                        //Console.WriteLine(movie.Tagline);
                    }

                    db.SaveChanges();
                }

                scope.Complete();
            }
        }

        // [TestMethod]
        public void PopulateGeneresLookupTabeData()
        {
            TextReader readFile = new StreamReader(@"C:\Users\Abhil\Desktop\movies_metadata.csv");
            var csv = new CsvReader(readFile);
            csv.Configuration.BadDataFound = null;
            csv.Configuration.ReadingExceptionOccurred = null;
            var moviesFromCsv = csv.GetRecords<dynamic>();

            List<Genre> genresTotal = new List<Genre>();

            foreach (var mov in moviesFromCsv)
            {
                JArray genreArray = JArray.Parse(mov.genres);
                int id;
                var ImdbId = Int32.TryParse(mov.id, out id) ? id : 0;

                foreach (var g in genreArray)
                {
                    var genres = JsonConvert.DeserializeObject<Genre>(g.ToString());
                    genresTotal.Add(genres);
                }
            }

            List<Genre> genresDistinct = new List<Genre>();

            foreach (var genre in genresTotal.Select(g => new {id = g.Id, name = g.Name}).Distinct())
            {
                genresDistinct.Add(new Genre() {Id = genre.id, Name = genre.name});
            }


            using (var scope = new TransactionScope())
            {
                Database.SetInitializer<MovieDbContext>(null);
                using (var db = new MovieDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;

                    foreach (var g in genresDistinct)
                    {
                        var gg = new Genre
                        {
                            Id = g.Id,
                            Name = g.Name
                        };
                        db.Genres.Add(gg);
                    }

                    db.SaveChanges();
                }

                scope.Complete();
            }
        }

       // [TestMethod]
        public void PopulateMovieGenresTableData()
        {
            TextReader readFile = new StreamReader(@"C:\Users\Abhil\Desktop\movies_metadata.csv");
            var csv = new CsvReader(readFile);
            csv.Configuration.BadDataFound = null;
            csv.Configuration.ReadingExceptionOccurred = null;
            var moviesFromCsv = csv.GetRecords<dynamic>();
            List<Movie> moviesFromDb;
            List<Genre> genresFromDb;
            Dictionary<int, List<Genre>> moviesGenresDictionary = new Dictionary<int, List<Genre>>();

            foreach (var mov in moviesFromCsv)
            {
                List<Genre> genresTotal = new List<Genre>();
                JArray genreArray = JArray.Parse(mov.genres);
                int id;
                var externalId = Int32.TryParse(mov.id, out id) ? id : 0;
                foreach (var g in genreArray)
                {
                    var genres = JsonConvert.DeserializeObject<Genre>(g.ToString());
                    genresTotal.Add(genres);
                }

                moviesGenresDictionary.Add(externalId, genresTotal);
            }


            Database.SetInitializer<MovieDbContext>(null);
            using (var db = new MovieDbContext())
            {
                moviesFromDb = db.Movies.ToList();
                genresFromDb = db.Genres.ToList();
            }

            using (var db = new MovieDbContext())
            {
                // db.Configuration.AutoDetectChangesEnabled = false;

                foreach (var m in moviesFromDb)
                {
                    var generesToAdd = moviesGenresDictionary[m.ExternalId];
                    Movie movie = new Movie() {Id = m.Id};

                    foreach (var g in generesToAdd)
                    {
                        int genreId = genresFromDb.Where(gn => gn.Name == g.Name).Select(gn => gn.Id).FirstOrDefault();
                        Genre genre = new Genre() {Id = genreId};

                          db.Database.ExecuteSqlCommand("Insert into MovieGenres Values({0},{1})", genreId, movie.Id);
                    }
                }
            }
        }

        // [TestMethod]
        public void PopulateCastsLookupTableData()
        {
            TextReader readFile = new StreamReader(@"C:\Users\Abhil\Desktop\credits.csv");
            var csv = new CsvReader(readFile);
            csv.Configuration.BadDataFound = null;
            csv.Configuration.ReadingExceptionOccurred = null;
            var castsFromCsv = csv.GetRecords<dynamic>();
            Dictionary<string, List<Cast>> movieCastDictionary = new Dictionary<string, List<Cast>>();
            List<Cast> casts = new List<Cast>();
            List<Cast> eachMovieCasts = new List<Cast>();
            foreach (var c in castsFromCsv)
            {
                try
                {
                    JArray casteArray = JArray.Parse(c.casting);
                    string movieid = c.id;
                    foreach (var cast in casteArray)
                    {
                        var castings = JsonConvert.DeserializeObject<Cast>(cast.ToString());
                        eachMovieCasts.Add(castings);
                    }

                    // movieCastDictionary.Add(movieid, eachMovieCasts);
                }
                catch (Exception e)
                {
                    //Console.WriteLine(c.id + ":: " + e.InnerException);
                }
            }

            var distinctCasts = eachMovieCasts.GroupBy(x => x.ExternalId).Select(d => d.First()).ToList();


            using (var tr = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 15, 0)))
            {
                Database.SetInitializer<MovieDbContext>(null);

                using (var db = new MovieDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    foreach (var c in distinctCasts)
                    {
                        db.Casts.Add(c);
                    }

                    db.SaveChanges();
                }

                tr.Complete();
            }
        }


        public void PopulateMovieCastsTableData()
        {

        }
    }
}