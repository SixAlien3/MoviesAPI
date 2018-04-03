namespace Movies.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MoviesBackdropUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "BackdropUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "BackdropUrl");
        }
    }
}
