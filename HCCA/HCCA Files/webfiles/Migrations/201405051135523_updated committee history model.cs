namespace HCCADBWebAppPrototype1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedcommitteehistorymodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConsumerRepCommitteeHistoryModel", "TimePeriod", c => c.Int(nullable: false));
            AddColumn("dbo.ConsumerRepCommitteeHistoryModel", "Year", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConsumerRepCommitteeHistoryModel", "Year");
            DropColumn("dbo.ConsumerRepCommitteeHistoryModel", "TimePeriod");
        }
    }
}
