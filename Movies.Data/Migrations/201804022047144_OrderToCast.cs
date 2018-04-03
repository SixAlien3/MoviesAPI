namespace Movies.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class OrderToCast : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Casts", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Casts", "Order");
        }
    }
}
