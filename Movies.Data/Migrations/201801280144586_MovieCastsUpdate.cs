namespace Movies.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieCastsUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MovieCasts", new[] { "MovieCasts_MovieId", "MovieCasts_CastId", "MovieCasts_Character" }, "dbo.MovieCasts");
            DropIndex("dbo.MovieCasts", new[] { "MovieCasts_MovieId", "MovieCasts_CastId", "MovieCasts_Character" });
            DropColumn("dbo.MovieCasts", "MovieCasts_MovieId");
            DropColumn("dbo.MovieCasts", "MovieCasts_CastId");
            DropColumn("dbo.MovieCasts", "MovieCasts_Character");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MovieCasts", "MovieCasts_Character", c => c.String(maxLength: 128));
            AddColumn("dbo.MovieCasts", "MovieCasts_CastId", c => c.Int());
            AddColumn("dbo.MovieCasts", "MovieCasts_MovieId", c => c.Int());
            CreateIndex("dbo.MovieCasts", new[] { "MovieCasts_MovieId", "MovieCasts_CastId", "MovieCasts_Character" });
            AddForeignKey("dbo.MovieCasts", new[] { "MovieCasts_MovieId", "MovieCasts_CastId", "MovieCasts_Character" }, "dbo.MovieCasts", new[] { "MovieId", "CastId", "Character" });
        }
    }
}
