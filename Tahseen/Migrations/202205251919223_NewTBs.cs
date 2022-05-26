namespace Tahseen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTBs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Approvals",
                c => new
                    {
                        ApprovalId = c.Int(nullable: false, identity: true),
                        ChildNationalID = c.String(nullable: false, maxLength: 128),
                        VaccineId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApprovalId)
                .ForeignKey("dbo.Children", t => t.ChildNationalID)
                .ForeignKey("dbo.Vaccines", t => t.VaccineId)
                .Index(t => t.ChildNationalID)
                .Index(t => t.VaccineId);
            
            CreateTable(
                "dbo.ChildDevelopments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChildNationalID = c.String(nullable: false, maxLength: 128),
                        AdivceAge = c.Int(nullable: false),
                        AdviceGame = c.String(nullable: false),
                        AdviceTalk = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Children", t => t.ChildNationalID)
                .Index(t => t.ChildNationalID);
            
            CreateTable(
                "dbo.ChildHealths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChildNationalID = c.String(nullable: false, maxLength: 128),
                        Height = c.Single(nullable: false),
                        Weight = c.Single(nullable: false),
                        Temperature = c.Single(nullable: false),
                        HeadCirumference = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Children", t => t.ChildNationalID)
                .Index(t => t.ChildNationalID);
            
            DropColumn("dbo.Children", "Height");
            DropColumn("dbo.Children", "Weight");
            DropColumn("dbo.Children", "Temperature");
            DropColumn("dbo.Children", "HeadCirumference");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Children", "HeadCirumference", c => c.Single(nullable: false));
            AddColumn("dbo.Children", "Temperature", c => c.Single(nullable: false));
            AddColumn("dbo.Children", "Weight", c => c.Single(nullable: false));
            AddColumn("dbo.Children", "Height", c => c.Single(nullable: false));
            DropForeignKey("dbo.ChildHealths", "ChildNationalID", "dbo.Children");
            DropForeignKey("dbo.ChildDevelopments", "ChildNationalID", "dbo.Children");
            DropForeignKey("dbo.Approvals", "VaccineId", "dbo.Vaccines");
            DropForeignKey("dbo.Approvals", "ChildNationalID", "dbo.Children");
            DropIndex("dbo.ChildHealths", new[] { "ChildNationalID" });
            DropIndex("dbo.ChildDevelopments", new[] { "ChildNationalID" });
            DropIndex("dbo.Approvals", new[] { "VaccineId" });
            DropIndex("dbo.Approvals", new[] { "ChildNationalID" });
            DropTable("dbo.ChildHealths");
            DropTable("dbo.ChildDevelopments");
            DropTable("dbo.Approvals");
        }
    }
}
