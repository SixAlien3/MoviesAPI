namespace Movies.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenreMovies : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GenreMovies",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Movie_Id })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Movie_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GenreMovies", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.GenreMovies", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.GenreMovies", new[] { "Movie_Id" });
            DropIndex("dbo.GenreMovies", new[] { "Genre_Id" });
            DropTable("dbo.GenreMovies");
            DropTable("dbo.Genres");
        }
    }
}
