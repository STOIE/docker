namespace HCCADBWebAppPrototype1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedcontactinfp : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ConsumerRepModel", "Address");
            DropColumn("dbo.ConsumerRepModel", "PhoneNumber");
            DropColumn("dbo.ConsumerRepModel", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConsumerRepModel", "Email", c => c.String());
            AddColumn("dbo.ConsumerRepModel", "PhoneNumber", c => c.Int(nullable: false));
            AddColumn("dbo.ConsumerRepModel", "Address", c => c.String());
        }
    }
}
