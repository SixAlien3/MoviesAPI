namespace Movies.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieCrewTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieCrews",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        CrewId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieId, t.CrewId })
                .ForeignKey("dbo.Crews", t => t.CrewId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.CrewId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieCrews", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MovieCrews", "CrewId", "dbo.Crews");
            DropIndex("dbo.MovieCrews", new[] { "CrewId" });
            DropIndex("dbo.MovieCrews", new[] { "MovieId" });
            DropTable("dbo.MovieCrews");
        }
    }
}
