namespace Tahseen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VaccineType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Approvals", "VaccineId", "dbo.Vaccines");
            DropIndex("dbo.Approvals", new[] { "VaccineId" });
            AddColumn("dbo.Approvals", "VaccineType", c => c.Int(nullable: false));
            DropColumn("dbo.Approvals", "VaccineId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Approvals", "VaccineId", c => c.Int(nullable: false));
            DropColumn("dbo.Approvals", "VaccineType");
            CreateIndex("dbo.Approvals", "VaccineId");
            AddForeignKey("dbo.Approvals", "VaccineId", "dbo.Vaccines", "Id");
        }
    }
}
