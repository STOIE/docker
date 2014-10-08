namespace HCCADBWebAppPrototype1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdasd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ConsumerRepCommitteeHistoryModel", "ReportedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ConsumerRepCommitteeHistoryModel", "EndorsementDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ConsumerRepCommitteeHistoryModel", "EndorsementDate", c => c.DateTime());
            AlterColumn("dbo.ConsumerRepCommitteeHistoryModel", "ReportedDate", c => c.DateTime());
        }
    }
}
