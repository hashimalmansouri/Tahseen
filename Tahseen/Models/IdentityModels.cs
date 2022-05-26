using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Tahseen.Models.Enums;

namespace Tahseen.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string NationalID { get; set; }
        public Gender Gender { get; set; }
        public string Major { get; set; }
        public DateTime? DOB { get; set; }
        public string Role { get; set; }
        public int? ClinicId { get; set; }
        public int? HSPId { get; set; }
        public virtual ICollection<Immunization> Immunizations { get; set; }
        //public virtual ICollection<Child> ChildParents { get; set; }
        [ForeignKey("ClinicId")]
        public virtual Clinic Clinic { get; set; }
        [ForeignKey("HSPId")]
        public virtual HSP HSP { get; set; }
        public string FullName
        {
            get
            {
                return $"{FName} {LName}";
            }
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class TahseenContext : IdentityDbContext<ApplicationUser>
    {
        public TahseenContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static TahseenContext Create()
        {
            return new TahseenContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "dbo");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "dbo");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles", "dbo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims", "dbo");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins", "dbo");
        }

        public virtual DbSet<Immunization> Immunizations { get; set; }
        public virtual DbSet<Child> Children { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<ClinicAppointment> ClinicAppointments { get; set; }
        public DbSet<HSP> HSPs { get; set; }
        public DbSet<ChildDevelopment> ChildDevelopments { get; set; }
        public DbSet<ChildHealth> ChildHealths { get; set; }
        public DbSet<Approval> Approvals { get; set; }
    }
}