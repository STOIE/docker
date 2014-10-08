namespace HCCADBWebAppPrototype1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedconsumerrepmoel_consumerrepmodelareaofinterestjointable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ConsumerRepAreaOfInterestModel", "ConsumerRepModel_ConsumerRepModelID", "dbo.ConsumerRepModel");
            DropIndex("dbo.ConsumerRepAreaOfInterestModel", new[] { "ConsumerRepModel_ConsumerRepModelID" });
            CreateTable(
                "dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel",
                c => new
                    {
                        ConsumerRepModel_ConsumerRepAreaOfInterestModelID = c.Int(nullable: false, identity: true),
                        ConsumerRepModelID = c.Int(nullable: false),
                        ConsumerRepAreaOfInterestID = c.Int(nullable: false),
                        ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID = c.Int(),
                    })
                .PrimaryKey(t => t.ConsumerRepModel_ConsumerRepAreaOfInterestModelID)
                .ForeignKey("dbo.ConsumerRepAreaOfInterestModel", t => t.ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID)
                .ForeignKey("dbo.ConsumerRepModel", t => t.ConsumerRepModelID, cascadeDelete: true)
                .Index(t => t.ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID)
                .Index(t => t.ConsumerRepModelID);
            
            DropColumn("dbo.ConsumerRepAreaOfInterestModel", "ConsumerRepModel_ConsumerRepModelID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConsumerRepAreaOfInterestModel", "ConsumerRepModel_ConsumerRepModelID", c => c.Int());
            DropForeignKey("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepModelID", "dbo.ConsumerRepModel");
            DropForeignKey("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID", "dbo.ConsumerRepAreaOfInterestModel");
            DropIndex("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", new[] { "ConsumerRepModelID" });
            DropIndex("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", new[] { "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID" });
            DropTable("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel");
            CreateIndex("dbo.ConsumerRepAreaOfInterestModel", "ConsumerRepModel_ConsumerRepModelID");
            AddForeignKey("dbo.ConsumerRepAreaOfInterestModel", "ConsumerRepModel_ConsumerRepModelID", "dbo.ConsumerRepModel", "ConsumerRepModelID");
        }
    }
}
