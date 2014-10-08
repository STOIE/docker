namespace HCCADBWebAppPrototype1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedcommittteeandconsumerrepmodels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConsumerRepModel", "EndorsementStatus", c => c.Int());
            DropColumn("dbo.ConsumerRepCommitteeHistoryModel", "EndorsementStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConsumerRepCommitteeHistoryModel", "EndorsementStatus", c => c.Int());
            DropColumn("dbo.ConsumerRepModel", "EndorsementStatus");
        }
    }
}
