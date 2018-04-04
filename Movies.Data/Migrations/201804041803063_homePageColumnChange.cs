namespace Movies.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class homePageColumnChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "HomePage", c => c.String());
            DropColumn("dbo.Movies", "WebsiteUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "WebsiteUrl", c => c.String());
            DropColumn("dbo.Movies", "HomePage");
        }
    }
}
