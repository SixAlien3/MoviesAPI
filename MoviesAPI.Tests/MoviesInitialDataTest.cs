using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using System.Transactions;
using CsvHelper;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movies.Data.Common;
using Movies.Data.Infrastructure;
using Movies.Data.Repositories;
using Movies.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RandomNameGeneratorLibrary;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using Crew = Movies.Models.Crew;
using Genre = Movies.Models.Genre;
using Keyword = Movies.Models.Keyword;

namespace MoviesAPI.Tests
{
    [TestClass]
    public class MoviesInitialDataTest
    {
        private readonly Random gen = new Random();


        [TestInitialize]
        public void TestInitializeData()
        {
            //var startTimeSpan = TimeSpan.Zero;
            //var periodTimeSpan = TimeSpan.FromSeconds(60);
            //var timer = new System.Threading.Timer((e) => { TestMoviesGetMovieImages(); }, null, startTimeSpan,
            //    periodTimeSpan);
            Thread t = new Thread(ThreadFunc)
            {
                IsBackground = true,
                Name = "Worker"
            };
            t.Start();
        }
        private  void ThreadFunc()
        {
            while (true)
            {
                Thread.Sleep(60000);
                TestMoviesGetMovieImages();
            }
        }

       // [TestMethod]
        public void TestMethod33()
        {
            int x = 30;
        }

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
        public void PopulateGeneresLookupTableData()
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
        public void PopulateKeywordsLookupTableData()
        {
            TextReader readFile = new StreamReader(@"C:\Users\Abhil\Desktop\keywords.csv");
            var csv = new CsvReader(readFile);
            csv.Configuration.BadDataFound = null;
            csv.Configuration.ReadingExceptionOccurred = null;
            var keyWordsFromCsv = csv.GetRecords<dynamic>();

            List<Keyword> keywordsTotal = new List<Keyword>();

            foreach (var mov in keyWordsFromCsv)
            {
                JArray keywordArray = JArray.Parse(mov.keywords);
                foreach (var g in keywordArray)
                {
                    var keywords = JsonConvert.DeserializeObject<Keyword>(g.ToString());
                    keywordsTotal.Add(keywords);
                }
            }

            List<Keyword> keywordsDistinct = new List<Keyword>();

            foreach (var key in keywordsTotal.Select(g => new {id = g.Id, name = g.Name}).Distinct())
            {
                keywordsDistinct.Add(new Keyword() {Id = key.id, Name = key.name});
            }


            using (var scope = new TransactionScope())
            {
                Database.SetInitializer<MovieDbContext>(null);
                using (var db = new MovieDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;

                    foreach (var g in keywordsDistinct)
                    {
                        var gg = new Keyword
                        {
                            Id = g.Id,
                            Name = g.Name
                        };
                        db.Keywords.Add(gg);
                    }

                    // db.SaveChanges();
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
            //csv.Configuration.BadDataFound = null;
            //csv.Configuration.ReadingExceptionOccurred = null;
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

        // [TestMethod]
        public void PopulateMovieCastsTableData()
        {
            TextReader readFile = new StreamReader(@"C:\Users\Abhil\Desktop\credits.csv");
            var csv = new CsvReader(readFile);
            csv.Configuration.BadDataFound = null;
            csv.Configuration.ReadingExceptionOccurred = null;
            var castsFromCsv = csv.GetRecords<dynamic>();
            Dictionary<string, List<Cast>> movieCastDictionary = new Dictionary<string, List<Cast>>();
            Dictionary<string, List<MovieCasts>> movieCastDictionary2 = new Dictionary<string, List<MovieCasts>>();
            List<Cast> casts = new List<Cast>();
            List<Movie> moviesFromDb;
            int totalRecords = 0;
            int nonRecords = 0;
            foreach (var c in castsFromCsv)
            {
                try
                {
                    List<Cast> eachMovieCasts = new List<Cast>();
                    List<MovieCasts> eachMovieCasts2 = new List<MovieCasts>();

                    JArray casteArray = JArray.Parse(c.casting);
                    string movieid = c.id;
                    foreach (var cast in casteArray)
                    {
                        var castings = JsonConvert.DeserializeObject<MovieCasts>(cast.ToString());
                        eachMovieCasts2.Add(castings);
                    }

                    movieCastDictionary2.Add(movieid, eachMovieCasts2);
                }
                catch (Exception e)
                {
                    //Console.WriteLine(c.id + ":: " + e.InnerException);
                }
            }

            Database.SetInitializer<MovieDbContext>(null);
            using (var db = new MovieDbContext())
            {
                moviesFromDb = db.Movies.ToList();
                casts = db.Casts.ToList();
            }

            using (var db = new MovieDbContext())
            {
                List<MovieCasts> movieCastses = new List<MovieCasts>();

                foreach (var m in moviesFromDb)
                {
                    if (movieCastDictionary2.ContainsKey(m.ExternalId.ToString()))
                    {
                        var caststoAdd = movieCastDictionary2[m.ExternalId.ToString()];
                        foreach (var g in caststoAdd)
                        {
                            try
                            {
                                int castid = casts.Where(gn => gn.ExternalId == g.CastId).Select(gn => gn.Id)
                                    .FirstOrDefault();
                                string character;
                                if (string.IsNullOrEmpty(g.Character) && g.Character.Length > 127)
                                {
                                    character = g.Character.Substring(0, 125);
                                }
                                else
                                {
                                    character = g.Character;
                                }

                                db.Database.ExecuteSqlCommand("Insert into MovieCasts Values({0},{1},{2})", m.Id,
                                    castid, character);
                                if (!movieCastses.Any(mc =>
                                    mc.MovieId == m.Id && mc.Character == character && mc.CastId == castid))
                                {
                                    totalRecords++;
                                }

                                {
                                    nonRecords++;
                                }

                                movieCastses.Add(new MovieCasts()
                                {
                                    CastId = castid,
                                    MovieId = m.Id,
                                    Character = character
                                });
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine(e.Message);
                            }
                        }
                    }
                }
            }

            Assert.AreEqual(totalRecords, nonRecords);
        }

        //[TestMethod]
        public void PopulateCrewLookupTableData()
        {
            TextReader readFile = new StreamReader(@"C:\Users\Abhil\Desktop\crew.csv");
            var csv = new CsvReader(readFile);
            //csv.Configuration.BadDataFound = null;
            //csv.Configuration.ReadingExceptionOccurred = null;
            var crewsFromCsv = csv.GetRecords<dynamic>();
            Dictionary<string, List<Crew>> moviecrewDictionary = new Dictionary<string, List<Crew>>();
            List<Crew> crews = new List<Crew>();
            List<Crew> eachMoviecrews = new List<Crew>();
            foreach (var c in crewsFromCsv)
            {
                try
                {
                    JArray creweArray = JArray.Parse(c.crew);
                    string movieid = c.id;
                    foreach (var crew in creweArray)
                    {
                        // var crewings3 = JsonConvert.DeserializeObject<Cast>(crew.ToString());

                        var crewings = JsonConvert.DeserializeObject<Crew>(crew.ToString());
                        eachMoviecrews.Add(crewings);
                    }

                    // moviecrewDictionary.Add(movieid, eachMoviecrews);
                }
                catch (Exception e)
                {
                    //Console.WriteLine(c.id + ":: " + e.InnerException);
                }
            }

            var distinctcrews = eachMoviecrews.GroupBy(x => x.ExternalId).Select(d => d.First()).ToList();


            using (var tr = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 15, 0)))
            {
                Database.SetInitializer<MovieDbContext>(null);

                using (var db = new MovieDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    foreach (var c in distinctcrews)
                    {
                        db.Crews.Add(c);
                    }

                    db.SaveChanges();
                }

                tr.Complete();
            }
        }

        // [TestMethod]
        public void PopulateMovieCrewsTableData()
        {
            TextReader readFile = new StreamReader(@"C:\Users\Abhil\Desktop\crew.csv");
            var csv = new CsvReader(readFile);
            csv.Configuration.BadDataFound = null;
            csv.Configuration.ReadingExceptionOccurred = null;
            var CrewsFromCsv = csv.GetRecords<dynamic>();
            Dictionary<string, List<Crew>> movieCrewDictionary = new Dictionary<string, List<Crew>>();
            Dictionary<string, List<MovieCrew>> movieCrewDictionary2 = new Dictionary<string, List<MovieCrew>>();
            List<Crew> Crews = new List<Crew>();
            List<Movie> moviesFromDb;
            int totalRecords = 0;
            int nonRecords = 0;
            foreach (var c in CrewsFromCsv)
            {
                try
                {
                    List<Crew> eachMovieCrews = new List<Crew>();
                    List<MovieCrew> eachMovieCrews2 = new List<MovieCrew>();

                    JArray CreweArray = JArray.Parse(c.crew);
                    string movieid = c.id;
                    foreach (var Crew in CreweArray)
                    {
                        var Crewings = JsonConvert.DeserializeObject<MovieCrew>(Crew.ToString());
                        eachMovieCrews2.Add(Crewings);
                    }

                    movieCrewDictionary2.Add(movieid, eachMovieCrews2);
                }
                catch (Exception e)
                {
                    //Console.WriteLine(c.id + ":: " + e.InnerException);
                }
            }

            Database.SetInitializer<MovieDbContext>(null);
            using (var db = new MovieDbContext())
            {
                moviesFromDb = db.Movies.ToList();
                Crews = db.Crews.ToList();
            }

            using (var db = new MovieDbContext())
            {
                List<MovieCrew> movieCrewses = new List<MovieCrew>();

                foreach (var m in moviesFromDb)
                {
                    if (movieCrewDictionary2.ContainsKey(m.ExternalId.ToString()))
                    {
                        var CrewstoAdd = movieCrewDictionary2[m.ExternalId.ToString()];
                        foreach (var g in CrewstoAdd)
                        {
                            try
                            {
                                int Crewid = Crews.Where(gn => gn.ExternalId == g.CrewId).Select(gn => gn.Id)
                                    .FirstOrDefault();
                                db.Database.ExecuteSqlCommand("Insert into MovieCrews Values({0},{1})", m.Id, Crewid);
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine(e.Message);
                            }
                        }
                    }
                }
            }

            Assert.AreEqual(totalRecords, nonRecords);
        }

        // [TestMethod]
        public void PupulateRandomUsersTableData()
        {
            UserRepository repository = new UserRepository(MovieDbContext.Create(),
                new ApplicationUserManager(new UserStore<ApplicationUser>(new MovieDbContext())),
                new ApplicationRoleManager(new RoleStore<IdentityRole>(new MovieDbContext())));

            var personGenerator = new PersonNameGenerator();
            var randomNames = personGenerator.GenerateMultipleFirstAndLastNames(20);


            foreach (var n in randomNames)
            {
                var names = n.Split(' ');
                var user = new ApplicationUser()
                {
                    UserName = names[0] + names[1] + "@gmail.com",
                    Email = names[0] + names[1] + "@gmail.com",
                    FirstName = names[0],
                    LastName = names[1],
                    DateOfBirth = RandomDay()
                };
                var addUserResult = repository.CreatUserAsync(user, "Antra2018!").Result;
            }

            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        public void TestMoviesGetMovieImages()
        {
            Database.SetInitializer<MovieDbContext>(null);

            using (var db = new MovieDbContext())
            {
                var moviesFromDb = db.Movies.Where(m => string.IsNullOrEmpty(m.BackdropUrl))
                    .OrderByDescending(m => m.Popularity).Take(40).ToList();
                foreach (var movie in moviesFromDb)
                {
                    var client = new TMDbClient("f260170a65522e5006559539ef75a2c2");
                    var images = client.GetMovieImagesAsync(movie.ExternalId).Result;
                    foreach (var img in images.Backdrops.OrderByDescending(b => b.VoteCount).Take(1))
                    {
                        movie.BackdropUrl = "https://image.tmdb.org/t/p/original/" + img.FilePath;
                    }

                    db.Entry(movie).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }


        DateTime RandomDay()
        {
            DateTime start = new DateTime(1947, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}