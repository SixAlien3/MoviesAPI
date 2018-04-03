namespace Movies.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MovieKeywords : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Keywords",
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
                "dbo.KeywordMovies",
                c => new
                    {
                        Keyword_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Keyword_Id, t.Movie_Id })
                .ForeignKey("dbo.Keywords", t => t.Keyword_Id, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.Keyword_Id)
                .Index(t => t.Movie_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KeywordMovies", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.KeywordMovies", "Keyword_Id", "dbo.Keywords");
            DropIndex("dbo.KeywordMovies", new[] { "Movie_Id" });
            DropIndex("dbo.KeywordMovies", new[] { "Keyword_Id" });
            DropTable("dbo.KeywordMovies");
            DropTable("dbo.Keywords");
        }
    }
}
