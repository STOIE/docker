namespace HCCADBWebAppPrototype1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedAreaOfHealthmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommitteeAreaOfHealthModel", "CommitteeModel_CommitteeModelID", "dbo.CommitteeModel");
            DropIndex("dbo.CommitteeAreaOfHealthModel", new[] { "CommitteeModel_CommitteeModelID" });
            CreateTable(
                "dbo.CommitteeModel_CommitteeAreaOfHealthModel",
                c => new
                    {
                        CommitteeModel_CommitteeAreaOfHealthModelID = c.Int(nullable: false, identity: true),
                        CommitteeModelID = c.Int(nullable: false),
                        CommitteeAreaOfHealthModel_CommitteeAreaOfHealthModelID = c.Int(),
                    })
                .PrimaryKey(t => t.CommitteeModel_CommitteeAreaOfHealthModelID)
                .ForeignKey("dbo.CommitteeAreaOfHealthModel", t => t.CommitteeAreaOfHealthModel_CommitteeAreaOfHealthModelID)
                .ForeignKey("dbo.CommitteeModel", t => t.CommitteeModelID, cascadeDelete: true)
                .Index(t => t.CommitteeAreaOfHealthModel_CommitteeAreaOfHealthModelID)
                .Index(t => t.CommitteeModelID);
            
            DropColumn("dbo.CommitteeAreaOfHealthModel", "CommitteeModel_CommitteeModelID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CommitteeAreaOfHealthModel", "CommitteeModel_CommitteeModelID", c => c.Int());
            DropForeignKey("dbo.CommitteeModel_CommitteeAreaOfHealthModel", "CommitteeModelID", "dbo.CommitteeModel");
            DropForeignKey("dbo.CommitteeModel_CommitteeAreaOfHealthModel", "CommitteeAreaOfHealthModel_CommitteeAreaOfHealthModelID", "dbo.CommitteeAreaOfHealthModel");
            DropIndex("dbo.CommitteeModel_CommitteeAreaOfHealthModel", new[] { "CommitteeModelID" });
            DropIndex("dbo.CommitteeModel_CommitteeAreaOfHealthModel", new[] { "CommitteeAreaOfHealthModel_CommitteeAreaOfHealthModelID" });
            DropTable("dbo.CommitteeModel_CommitteeAreaOfHealthModel");
            CreateIndex("dbo.CommitteeAreaOfHealthModel", "CommitteeModel_CommitteeModelID");
            AddForeignKey("dbo.CommitteeAreaOfHealthModel", "CommitteeModel_CommitteeModelID", "dbo.CommitteeModel", "CommitteeModelID");
        }
    }
}
