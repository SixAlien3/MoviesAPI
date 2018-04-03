using System;
using System.Data.Entity;
using System.Linq;
using Movies.Data.Common;
using TMDbLib.Client;
using System.Timers;
using Timer = System.Timers.Timer;

namespace MovieConsole
{
    class Program
    {
        private static Timer aTimer;
        private static bool ExitProgram = false;

        static void Main(string[] args)
        {
            if (ExitProgram == false)
            {
                // Create a timer with a two second interval.
                aTimer = new Timer(60000);
                // Hook up the Elapsed event for the timer. 
                aTimer.Elapsed += TestMoviesGetMovieImages;
                aTimer.Enabled = true;
            }


            // Console.WriteLine("Press the Enter key to exit the program... ");

            Console.WriteLine("Terminating the application...");
            Console.ReadLine();
        }


        public static void TestMoviesGetMovieImages(Object source, ElapsedEventArgs e)
        {
            Database.SetInitializer<MovieDbContext>(null);

            using (var db = new MovieDbContext())
            {
                var moviesFromDb = db.Movies.Where(m => string.IsNullOrEmpty(m.BackdropUrl))
                    .OrderByDescending(m => m.Popularity).Take(40).ToList();
                if (!moviesFromDb.Any())
                {
                    ExitProgram = true;
                    return;
                }

                foreach (var movie in moviesFromDb)
                {
                    try
                    {
                        var client = new TMDbClient("f260170a65522e5006559539ef75a2c2");
                        var images = client.GetMovieImagesAsync(movie.ExternalId).Result;
                        if (images.Backdrops != null)
                        {
                            try
                            {
                                foreach (var img in images.Backdrops.OrderByDescending(b => b.VoteCount).Take(1))
                                {
                                    movie.BackdropUrl = "https://image.tmdb.org/t/p/original/" + img.FilePath;
                                }
                            }
                            catch (Exception exception)
                            {
                                continue;
                            }
                       
                        }
                    }
                    catch (Exception exception)
                    {
                        continue;
                    }

                    db.Entry(movie).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
    }
}