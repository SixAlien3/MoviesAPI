namespace Movies.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TrailersToMovie : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trailers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Type = c.String(),
                        Site = c.String(),
                        Key = c.String(),
                        Name = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .Index(t => t.Movie_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trailers", "Movie_Id", "dbo.Movies");
            DropIndex("dbo.Trailers", new[] { "Movie_Id" });
            DropTable("dbo.Trailers");
        }
    }
}
