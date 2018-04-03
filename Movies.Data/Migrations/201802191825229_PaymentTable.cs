namespace Movies.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PaymentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(maxLength: 128),
                        CardType = c.String(),
                        CardName = c.String(),
                        CardNumber = c.String(),
                        MaskedCreditCardNumber = c.String(),
                        CardCvv2 = c.String(),
                        CardExpirationMonth = c.String(),
                        CardExpirationYear = c.String(),
                        AddressId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CustomerId)
                .Index(t => t.CustomerId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PurchaseNumber = c.Guid(nullable: false, identity: true),
                        CustomerId = c.String(maxLength: 128),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchaseDateTime = c.DateTime(nullable: false),
                        MovieId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CustomerId)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.MovieId);
            
            AddColumn("dbo.Rentals", "RentalNumber", c => c.Guid(nullable: false, identity: true));
            AddColumn("dbo.Rentals", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Rentals", "OrderGuid");
            DropColumn("dbo.Rentals", "OrderTotal");
            DropColumn("dbo.Rentals", "RentalExpireDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rentals", "RentalExpireDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Rentals", "OrderTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Rentals", "OrderGuid", c => c.Guid(nullable: false, identity: true));
            DropForeignKey("dbo.Purchases", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Purchases", "CustomerId", "dbo.Users");
            DropForeignKey("dbo.Payments", "CustomerId", "dbo.Users");
            DropForeignKey("dbo.Payments", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Purchases", new[] { "MovieId" });
            DropIndex("dbo.Purchases", new[] { "CustomerId" });
            DropIndex("dbo.Payments", new[] { "AddressId" });
            DropIndex("dbo.Payments", new[] { "CustomerId" });
            DropColumn("dbo.Rentals", "TotalPrice");
            DropColumn("dbo.Rentals", "RentalNumber");
            DropTable("dbo.Purchases");
            DropTable("dbo.Payments");
        }
    }
}
