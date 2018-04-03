namespace Movies.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CrewTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Crews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Gender = c.String(),
                        Department = c.String(),
                        Job = c.String(),
                        ExternalId = c.Int(nullable: false),
                        ProfilePath = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Crews");
        }
    }
}
