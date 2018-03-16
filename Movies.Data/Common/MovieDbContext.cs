using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Movies.Models;
using Movies.Models.Common;

namespace Movies.Data.Common
{
   public class MovieDbContext : IdentityDbContext<ApplicationUser>
    {
        public MovieDbContext(): base("Name=MovieDbContext", throwIfV1Schema: false)
        {
            Configuration.LazyLoadingEnabled = false;

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<Trailer> Trailers { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .ToTable("Users");

            modelBuilder.Entity<IdentityRole>()
                .ToTable("Roles");

            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("UserRoles");

            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("UserClaims");

            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable("UserLogins");
        }

        public static MovieDbContext Create()
        {
            return new MovieDbContext();
        }
        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity
                            &&
                            (x.State == System.Data.Entity.EntityState.Added ||
                             x.State == System.Data.Entity.EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    var currentClaims = (Thread.CurrentPrincipal as ClaimsPrincipal)?.Identities.FirstOrDefault()?.Claims;
                    if (currentClaims != null)
                    {
                        string identityName =
                            currentClaims.FirstOrDefault(c => c.Type == "userName")
                                ?.Value;

                        DateTime now = DateTime.Now;

                        if (entry.State == System.Data.Entity.EntityState.Added)
                        {
                            entity.CreatedBy = identityName;
                            entity.CreatedDate = now;
                        }
                        else
                        {
                            base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                            base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                        }

                        entity.UpdatedBy = identityName;
                        entity.UpdatedDate = now;
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}
