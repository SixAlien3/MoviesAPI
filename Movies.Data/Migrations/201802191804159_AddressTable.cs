namespace Movies.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Company = c.String(),
                        State = c.String(),
                        County = c.String(),
                        City = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        ZipPostalCode = c.String(),
                        PhoneNumber = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.Users", "BillingAddress_Id", c => c.Int());
            AddColumn("dbo.Users", "ShippingAddress_Id", c => c.Int());
            CreateIndex("dbo.Users", "BillingAddress_Id");
            CreateIndex("dbo.Users", "ShippingAddress_Id");
            AddForeignKey("dbo.Users", "BillingAddress_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.Users", "ShippingAddress_Id", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ShippingAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.Users", "BillingAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "ApplicationUser_Id", "dbo.Users");
            DropIndex("dbo.Addresses", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Users", new[] { "ShippingAddress_Id" });
            DropIndex("dbo.Users", new[] { "BillingAddress_Id" });
            DropColumn("dbo.Users", "ShippingAddress_Id");
            DropColumn("dbo.Users", "BillingAddress_Id");
            DropTable("dbo.Addresses");
        }
    }
}
