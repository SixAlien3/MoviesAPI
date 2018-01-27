using System;
using System.Collections.Generic;
using System.IO;
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
        [TestMethod]
        public void PopulateInitialMoviesTableData()
        {
            TextReader readFile = new StreamReader(@"C:\Users\Abhil\Desktop\movies_metadata.csv");
            var csv = new CsvReader(readFile);
            csv.Configuration.BadDataFound = null;
            csv.Configuration.ReadingExceptionOccurred = null;
            var moviesFromCsv = csv.GetRecords<dynamic>();


            using (var scope = new TransactionScope())
            {

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
    }
}