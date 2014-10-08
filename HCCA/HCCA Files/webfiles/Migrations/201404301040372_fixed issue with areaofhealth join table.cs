namespace HCCADBWebAppPrototype1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedissuewithareaofhealthjointable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommitteeModel_CommitteeAreaOfHealthModel", "CommitteeAreaOfHealthModel_CommitteeAreaOfHealthModelID", "dbo.CommitteeAreaOfHealthModel");
            DropIndex("dbo.CommitteeModel_CommitteeAreaOfHealthModel", new[] { "CommitteeAreaOfHealthModel_CommitteeAreaOfHealthModelID" });
            RenameColumn(table: "dbo.CommitteeModel_CommitteeAreaOfHealthModel", name: "CommitteeAreaOfHealthModel_CommitteeAreaOfHealthModelID", newName: "CommitteeAreaOfHealthModelID");
            AlterColumn("dbo.CommitteeModel_CommitteeAreaOfHealthModel", "CommitteeAreaOfHealthModelID", c => c.Int(nullable: false));
            CreateIndex("dbo.CommitteeModel_CommitteeAreaOfHealthModel", "CommitteeAreaOfHealthModelID");
            AddForeignKey("dbo.CommitteeModel_CommitteeAreaOfHealthModel", "CommitteeAreaOfHealthModelID", "dbo.CommitteeAreaOfHealthModel", "CommitteeAreaOfHealthModelID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommitteeModel_CommitteeAreaOfHealthModel", "CommitteeAreaOfHealthModelID", "dbo.CommitteeAreaOfHealthModel");
            DropIndex("dbo.CommitteeModel_CommitteeAreaOfHealthModel", new[] { "CommitteeAreaOfHealthModelID" });
            AlterColumn("dbo.CommitteeModel_CommitteeAreaOfHealthModel", "CommitteeAreaOfHealthModelID", c => c.Int());
            RenameColumn(table: "dbo.CommitteeModel_CommitteeAreaOfHealthModel", name: "CommitteeAreaOfHealthModelID", newName: "CommitteeAreaOfHealthModel_CommitteeAreaOfHealthModelID");
            CreateIndex("dbo.CommitteeModel_CommitteeAreaOfHealthModel", "CommitteeAreaOfHealthModel_CommitteeAreaOfHealthModelID");
            AddForeignKey("dbo.CommitteeModel_CommitteeAreaOfHealthModel", "CommitteeAreaOfHealthModel_CommitteeAreaOfHealthModelID", "dbo.CommitteeAreaOfHealthModel", "CommitteeAreaOfHealthModelID");
        }
    }
}
