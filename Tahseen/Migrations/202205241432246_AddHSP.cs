namespace Tahseen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHSP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HSPs",
                c => new
                    {
                        HSPId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.HSPId);
            
            AddColumn("dbo.Clinics", "HSPId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "HSPId", c => c.Int());
            CreateIndex("dbo.Clinics", "HSPId");
            CreateIndex("dbo.Users", "HSPId");
            AddForeignKey("dbo.Clinics", "HSPId", "dbo.HSPs", "HSPId");
            AddForeignKey("dbo.Users", "HSPId", "dbo.HSPs", "HSPId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "HSPId", "dbo.HSPs");
            DropForeignKey("dbo.Clinics", "HSPId", "dbo.HSPs");
            DropIndex("dbo.Users", new[] { "HSPId" });
            DropIndex("dbo.Clinics", new[] { "HSPId" });
            DropColumn("dbo.Users", "HSPId");
            DropColumn("dbo.Clinics", "HSPId");
            DropTable("dbo.HSPs");
        }
    }
}
