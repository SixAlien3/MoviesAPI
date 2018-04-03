namespace Movies.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddingIndexes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Title", c => c.String(maxLength: 128));
            CreateIndex("dbo.Movies", "Title");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Movies", new[] { "Title" });
            AlterColumn("dbo.Movies", "Title", c => c.String());
        }
    }
}
