namespace Movies.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FavoritesTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MovieGenres", newName: "GenreMovies");
            DropPrimaryKey("dbo.GenreMovies");
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.MovieId, t.CustomerId })
                .ForeignKey("dbo.Users", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.CustomerId);
            
            AddPrimaryKey("dbo.GenreMovies", new[] { "Genre_Id", "Movie_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favorites", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Favorites", "CustomerId", "dbo.Users");
            DropIndex("dbo.Favorites", new[] { "CustomerId" });
            DropIndex("dbo.Favorites", new[] { "MovieId" });
            DropPrimaryKey("dbo.GenreMovies");
            DropTable("dbo.Favorites");
            AddPrimaryKey("dbo.GenreMovies", new[] { "Movie_Id", "Genre_Id" });
            RenameTable(name: "dbo.GenreMovies", newName: "MovieGenres");
        }
    }
}
