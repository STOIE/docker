namespace HCCADBWebAppPrototype1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedmodelvaliation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CommitteeAreaOfHealthModel", "AreaOfHealthName", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.ConsumerRepModel", "FirstName", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.ConsumerRepModel", "LastName", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.ConsumerRepAreaOfInterestModel", "AreaOfInterestName", c => c.String(nullable: false, maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ConsumerRepAreaOfInterestModel", "AreaOfInterestName", c => c.String());
            AlterColumn("dbo.ConsumerRepModel", "LastName", c => c.String());
            AlterColumn("dbo.ConsumerRepModel", "FirstName", c => c.String());
            AlterColumn("dbo.CommitteeAreaOfHealthModel", "AreaOfHealthName", c => c.String());
        }
    }
}
