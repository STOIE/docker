namespace HCCADBWebAppPrototype1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incorrectIDnameinconsumerrepmodel_consumerepareaofinterestmodelidjointable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID", "dbo.ConsumerRepAreaOfInterestModel");
            DropIndex("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", new[] { "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID" });
            RenameColumn(table: "dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", name: "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID", newName: "ConsumerRepAreaOfInterestModelID");
            AlterColumn("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModelID", c => c.Int(nullable: false));
            CreateIndex("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModelID");
            AddForeignKey("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModelID", "dbo.ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModelID", cascadeDelete: true);
            DropColumn("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModelID", "dbo.ConsumerRepAreaOfInterestModel");
            DropIndex("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", new[] { "ConsumerRepAreaOfInterestModelID" });
            AlterColumn("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModelID", c => c.Int());
            RenameColumn(table: "dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", name: "ConsumerRepAreaOfInterestModelID", newName: "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID");
            CreateIndex("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID");
            AddForeignKey("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID", "dbo.ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModelID");
        }
    }
}
