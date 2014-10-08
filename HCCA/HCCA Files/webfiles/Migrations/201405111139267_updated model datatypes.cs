namespace HCCADBWebAppPrototype1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedmodeldatatypes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CommitteeAreaOfHealthModel", "AreaOfHealthName", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.ConsumerRepAreaOfInterestModel", "AreaOfInterestName", c => c.String(nullable: false, maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ConsumerRepAreaOfInterestModel", "AreaOfInterestName", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.CommitteeAreaOfHealthModel", "AreaOfHealthName", c => c.String(nullable: false, maxLength: 32));
        }
    }
}
