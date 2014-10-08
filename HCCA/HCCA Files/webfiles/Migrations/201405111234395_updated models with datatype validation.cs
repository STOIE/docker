namespace HCCADBWebAppPrototype1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedmodelswithdatatypevalidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ConsumerRepModel", "MemberStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ConsumerRepModel", "MemberStatus", c => c.Int());
        }
    }
}
