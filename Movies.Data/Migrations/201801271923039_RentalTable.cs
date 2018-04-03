namespace Movies.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RentalTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GenreMovies", newName: "MovieGenres");
            DropPrimaryKey("dbo.MovieGenres");
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        OrderGuid = c.Guid(nullable: false, identity: true),
                        CustomerId = c.String(maxLength: 128),
                        OrderTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RentalDateTime = c.DateTime(nullable: false),
                        RentalDurationHours = c.Int(nullable: false),
                        RentalExpireDate = c.DateTime(nullable: false),
                        MovieId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CustomerId)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.MovieId);
            
            AddPrimaryKey("dbo.MovieGenres", new[] { "Movie_Id", "Genre_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Rentals", "CustomerId", "dbo.Users");
            DropIndex("dbo.Rentals", new[] { "MovieId" });
            DropIndex("dbo.Rentals", new[] { "CustomerId" });
            DropPrimaryKey("dbo.MovieGenres");
            DropTable("dbo.Rentals");
            AddPrimaryKey("dbo.MovieGenres", new[] { "Genre_Id", "Movie_Id" });
            RenameTable(name: "dbo.MovieGenres", newName: "GenreMovies");
        }
    }
}
