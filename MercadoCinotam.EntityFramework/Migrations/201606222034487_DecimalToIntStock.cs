namespace MercadoCinotam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DecimalToIntStock : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "AvailableStock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "AvailableStock", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
