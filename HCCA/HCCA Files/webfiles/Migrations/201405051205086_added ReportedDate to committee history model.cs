namespace HCCADBWebAppPrototype1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedReportedDatetocommitteehistorymodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConsumerRepCommitteeHistoryModel", "ReportedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ConsumerRepCommitteeHistoryModel", "TimePeriod");
            DropColumn("dbo.ConsumerRepCommitteeHistoryModel", "Year");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConsumerRepCommitteeHistoryModel", "Year", c => c.DateTime(nullable: false));
            AddColumn("dbo.ConsumerRepCommitteeHistoryModel", "TimePeriod", c => c.Int(nullable: false));
            DropColumn("dbo.ConsumerRepCommitteeHistoryModel", "ReportedDate");
        }
    }
}
