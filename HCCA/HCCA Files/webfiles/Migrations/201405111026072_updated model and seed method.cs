namespace HCCADBWebAppPrototype1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedmodelandseedmethod : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CommitteeModel", "CommitteeName", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.CommitteeModel", "CurrentStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CommitteeModel", "CurrentStatus", c => c.Int());
            AlterColumn("dbo.CommitteeModel", "CommitteeName", c => c.String());
        }
    }
}
