namespace MercadoCinotam.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class ThemeInfoRemoved : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ThemePreviews", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.ThemeRequiredFields", "Theme_Id", "dbo.Themes");
            DropIndex("dbo.ThemePreviews", new[] { "Theme_Id" });
            DropIndex("dbo.ThemeRequiredFields", new[] { "Theme_Id" });
            DropTable("dbo.ThemePreviews");
            DropTable("dbo.Themes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Theme_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ThemeRequiredFields",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ThemeRequiredField_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ThemeRequiredFields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                        Type = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Theme_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ThemeRequiredField_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ThemeUniqueName = c.String(),
                        ThemeName = c.String(),
                        ThemeDescription = c.String(),
                        Released = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Theme_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ThemePreviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Theme_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ThemeRequiredFields", "Theme_Id");
            CreateIndex("dbo.ThemePreviews", "Theme_Id");
            AddForeignKey("dbo.ThemeRequiredFields", "Theme_Id", "dbo.Themes", "Id");
            AddForeignKey("dbo.ThemePreviews", "Theme_Id", "dbo.Themes", "Id");
        }
    }
}
