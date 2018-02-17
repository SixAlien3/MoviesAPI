namespace Movies.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuditColumsChange : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.MovieCasts");
            AlterColumn("dbo.Casts", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Casts", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Genres", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Genres", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Movies", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Movies", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Keywords", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Keywords", "UpdatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Rentals", "CreatedBy", c => c.String(maxLength: 256));
            AlterColumn("dbo.Rentals", "UpdatedBy", c => c.String(maxLength: 256));
            AddPrimaryKey("dbo.MovieCasts", new[] { "MovieId", "CastId", "Character" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MovieCasts");
            AlterColumn("dbo.Rentals", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Rentals", "CreatedBy", c => c.String());
            AlterColumn("dbo.Keywords", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Keywords", "CreatedBy", c => c.String());
            AlterColumn("dbo.Movies", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Movies", "CreatedBy", c => c.String());
            AlterColumn("dbo.Genres", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Genres", "CreatedBy", c => c.String());
            AlterColumn("dbo.Casts", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Casts", "CreatedBy", c => c.String());
            AddPrimaryKey("dbo.MovieCasts", new[] { "MovieId", "Character" });
        }
    }
}
