namespace Tahseen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        ChildId = c.String(nullable: false, maxLength: 128),
                        VaccineId = c.Int(nullable: false),
                        Place = c.Int(nullable: false),
                        ClinicAppointmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentId)
                .ForeignKey("dbo.Children", t => t.ChildId)
                .ForeignKey("dbo.Vaccines", t => t.VaccineId)
                .ForeignKey("dbo.ClinicAppointments", t => t.ClinicAppointmentId)
                .Index(t => t.ChildId)
                .Index(t => t.VaccineId)
                .Index(t => t.ClinicAppointmentId);
            
            CreateTable(
                "dbo.Children",
                c => new
                    {
                        ChildID = c.String(nullable: false, maxLength: 128),
                        FName = c.String(nullable: false),
                        LName = c.String(nullable: false),
                        MName = c.String(nullable: false),
                        Gender = c.Int(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        Address = c.String(nullable: false),
                        ParentNationalId = c.String(nullable: false),
                        Height = c.Single(nullable: false),
                        Weight = c.Single(nullable: false),
                        Temperature = c.Single(nullable: false),
                        HeadCirumference = c.Single(nullable: false),
                        ClinicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChildID)
                .ForeignKey("dbo.Clinics", t => t.ClinicId)
                .Index(t => t.ClinicId);
            
            CreateTable(
                "dbo.Clinics",
                c => new
                    {
                        ClinicId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ClinicId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FName = c.String(),
                        LName = c.String(),
                        NationalID = c.String(),
                        Gender = c.Int(nullable: false),
                        Major = c.String(),
                        DOB = c.DateTime(),
                        Role = c.String(),
                        ClinicId = c.Int(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clinics", t => t.ClinicId)
                .Index(t => t.ClinicId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Immunizations",
                c => new
                    {
                        ImmunizationId = c.Int(nullable: false, identity: true),
                        NationalID = c.String(nullable: false, maxLength: 128),
                        VaccineId = c.Int(nullable: false),
                        VaccinationDate = c.DateTime(nullable: false),
                        DoseNo = c.Int(nullable: false),
                        DateOfNextDose = c.DateTime(nullable: false),
                        VaccinatorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ImmunizationId)
                .ForeignKey("dbo.Children", t => t.NationalID)
                .ForeignKey("dbo.Users", t => t.VaccinatorId)
                .ForeignKey("dbo.Vaccines", t => t.VaccineId)
                .Index(t => t.NationalID)
                .Index(t => t.VaccineId)
                .Index(t => t.VaccinatorId);
            
            CreateTable(
                "dbo.Vaccines",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ClinicAppointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                        ClinicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clinics", t => t.ClinicId)
                .Index(t => t.ClinicId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Appointments", "ClinicAppointmentId", "dbo.ClinicAppointments");
            DropForeignKey("dbo.ClinicAppointments", "ClinicId", "dbo.Clinics");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Immunizations", "VaccineId", "dbo.Vaccines");
            DropForeignKey("dbo.Appointments", "VaccineId", "dbo.Vaccines");
            DropForeignKey("dbo.Immunizations", "VaccinatorId", "dbo.Users");
            DropForeignKey("dbo.Immunizations", "NationalID", "dbo.Children");
            DropForeignKey("dbo.Users", "ClinicId", "dbo.Clinics");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Children", "ClinicId", "dbo.Clinics");
            DropForeignKey("dbo.Appointments", "ChildId", "dbo.Children");
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.ClinicAppointments", new[] { "ClinicId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.Immunizations", new[] { "VaccinatorId" });
            DropIndex("dbo.Immunizations", new[] { "VaccineId" });
            DropIndex("dbo.Immunizations", new[] { "NationalID" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Users", new[] { "ClinicId" });
            DropIndex("dbo.Children", new[] { "ClinicId" });
            DropIndex("dbo.Appointments", new[] { "ClinicAppointmentId" });
            DropIndex("dbo.Appointments", new[] { "VaccineId" });
            DropIndex("dbo.Appointments", new[] { "ChildId" });
            DropTable("dbo.Roles");
            DropTable("dbo.ClinicAppointments");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.Vaccines");
            DropTable("dbo.Immunizations");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Clinics");
            DropTable("dbo.Children");
            DropTable("dbo.Appointments");
        }
    }
}
