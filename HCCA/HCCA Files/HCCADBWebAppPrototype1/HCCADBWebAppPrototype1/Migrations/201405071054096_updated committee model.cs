namespace HCCADBWebAppPrototype1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedcommitteemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConsumerRepCommitteeHistoryModel", "FinishedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConsumerRepCommitteeHistoryModel", "FinishedDate");
        }
    }
}
