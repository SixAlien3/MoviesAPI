namespace Movies.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MovieCastsCharacterLength : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.MovieCasts");
            AlterColumn("dbo.MovieCasts", "Character", c => c.String(nullable: false, maxLength: 2000));
            AddPrimaryKey("dbo.MovieCasts", new[] { "MovieId", "Character" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MovieCasts");
            AlterColumn("dbo.MovieCasts", "Character", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.MovieCasts", new[] { "MovieId", "CastId", "Character" });
        }
    }
}
