namespace MercadoCinotam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemeContentType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ThemeRequiredFields", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ThemeRequiredFields", "Type");
        }
    }
}
