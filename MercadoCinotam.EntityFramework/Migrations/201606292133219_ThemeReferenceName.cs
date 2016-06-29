namespace MercadoCinotam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemeReferenceName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MainPageContents", "ThemeReferenceId", c => c.Int(nullable: false));
            AddColumn("dbo.MainPageContents", "ThemeReferenceName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MainPageContents", "ThemeReferenceName");
            DropColumn("dbo.MainPageContents", "ThemeReferenceId");
        }
    }
}
