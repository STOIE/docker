namespace HCCADBWebAppPrototype1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommitteeAreaOfHealthModel",
                c => new
                    {
                        CommitteeAreaOfHealthModelID = c.Int(nullable: false, identity: true),
                        AreaOfHealthName = c.String(),
                        CommitteeModel_CommitteeModelID = c.Int(),
                    })
                .PrimaryKey(t => t.CommitteeAreaOfHealthModelID)
                .ForeignKey("dbo.CommitteeModel", t => t.CommitteeModel_CommitteeModelID)
                .Index(t => t.CommitteeModel_CommitteeModelID);
            
            CreateTable(
                "dbo.CommitteeModel",
                c => new
                    {
                        CommitteeModelID = c.Int(nullable: false, identity: true),
                        CommitteeName = c.String(),
                        CurrentStatus = c.Int(),
                    })
                .PrimaryKey(t => t.CommitteeModelID);
            
            CreateTable(
                "dbo.ConsumerRepCommitteeHistoryModel",
                c => new
                    {
                        ConsumerRepCommitteeHistoryModelID = c.Int(nullable: false, identity: true),
                        CommitteeModelID = c.Int(nullable: false),
                        ConsumerRepModelID = c.Int(nullable: false),
                        PrepTime = c.Int(nullable: false),
                        Meetingtime = c.Int(nullable: false),
                        EndorsementStatus = c.Int(),
                        EndorsementDate = c.DateTime(nullable: false),
                        EndorsementType = c.Int(),
                    })
                .PrimaryKey(t => t.ConsumerRepCommitteeHistoryModelID)
                .ForeignKey("dbo.CommitteeModel", t => t.CommitteeModelID, cascadeDelete: true)
                .ForeignKey("dbo.ConsumerRepModel", t => t.ConsumerRepModelID, cascadeDelete: true)
                .Index(t => t.CommitteeModelID)
                .Index(t => t.ConsumerRepModelID);
            
            CreateTable(
                "dbo.ConsumerRepModel",
                c => new
                    {
                        ConsumerRepModelID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        Email = c.String(),
                        MemberStatus = c.Int(),
                        DateTrained = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ConsumerRepModelID);
            
            CreateTable(
                "dbo.ConsumerRepAreaOfInterestModel",
                c => new
                    {
                        ConsumerRepAreaOfInterestModelID = c.Int(nullable: false, identity: true),
                        AreaOfInterestName = c.String(),
                        ConsumerRepModel_ConsumerRepModelID = c.Int(),
                    })
                .PrimaryKey(t => t.ConsumerRepAreaOfInterestModelID)
                .ForeignKey("dbo.ConsumerRepModel", t => t.ConsumerRepModel_ConsumerRepModelID)
                .Index(t => t.ConsumerRepModel_ConsumerRepModelID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConsumerRepCommitteeHistoryModel", "ConsumerRepModelID", "dbo.ConsumerRepModel");
            DropForeignKey("dbo.ConsumerRepAreaOfInterestModel", "ConsumerRepModel_ConsumerRepModelID", "dbo.ConsumerRepModel");
            DropForeignKey("dbo.ConsumerRepCommitteeHistoryModel", "CommitteeModelID", "dbo.CommitteeModel");
            DropForeignKey("dbo.CommitteeAreaOfHealthModel", "CommitteeModel_CommitteeModelID", "dbo.CommitteeModel");
            DropIndex("dbo.ConsumerRepCommitteeHistoryModel", new[] { "ConsumerRepModelID" });
            DropIndex("dbo.ConsumerRepAreaOfInterestModel", new[] { "ConsumerRepModel_ConsumerRepModelID" });
            DropIndex("dbo.ConsumerRepCommitteeHistoryModel", new[] { "CommitteeModelID" });
            DropIndex("dbo.CommitteeAreaOfHealthModel", new[] { "CommitteeModel_CommitteeModelID" });
            DropTable("dbo.ConsumerRepAreaOfInterestModel");
            DropTable("dbo.ConsumerRepModel");
            DropTable("dbo.ConsumerRepCommitteeHistoryModel");
            DropTable("dbo.CommitteeModel");
            DropTable("dbo.CommitteeAreaOfHealthModel");
        }
    }
}
