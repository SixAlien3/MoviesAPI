namespace Movies.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RatingsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        UserRating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieId, t.CustomerId })
                .ForeignKey("dbo.Users", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Ratings", "CustomerId", "dbo.Users");
            DropIndex("dbo.Ratings", new[] { "CustomerId" });
            DropIndex("dbo.Ratings", new[] { "MovieId" });
            DropTable("dbo.Ratings");
        }
    }
}
