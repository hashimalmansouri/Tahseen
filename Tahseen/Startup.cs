using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Linq;
using Tahseen.Models;
using Tahseen.Models.Enums;

[assembly: OwinStartupAttribute(typeof(Tahseen.Startup))]
namespace Tahseen
{
    public partial class Startup
    {
        private TahseenContext db = new TahseenContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }

        public void CreateDefaultRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new
                RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new
                UserStore<ApplicationUser>(db));

            IdentityRole role1 = new IdentityRole();
            IdentityRole role2 = new IdentityRole();
            IdentityRole role3 = new IdentityRole();
            IdentityRole role4 = new IdentityRole();
            IdentityRole role5 = new IdentityRole();

            if (!roleManager.RoleExists(RolesConstant.HSP))
            {
                role1.Name = RolesConstant.HSP;
                roleManager.Create(role1);
                ApplicationUser user = new ApplicationUser
                {
                    Email = "Admin@example.com",
                    UserName = "HSP",
                    Role = RolesConstant.HSP,
                    EmailConfirmed = true,
                    FName = "رغد",
                    LName = "العمري",
                    PhoneNumber = "0556114239",
                    DOB = DateTime.Today.AddYears(-30),
                    Gender = Gender.Female,
                    NationalID = "884410",
                    Major = "HSP"
                };
                var check = userManager.Create(user, "Tahseen@123");
                if (check.Succeeded)
                {
                    userManager.AddToRole(user.Id, role1.Name);
                }
            }

            if (!roleManager.RoleExists(RolesConstant.Parent))
            {
                role2.Name = RolesConstant.Parent;
                roleManager.Create(role2);
                ApplicationUser user = new ApplicationUser
                {
                    Email = "raghadlu999@gmail.com",
                    UserName = "Raghad9",
                    Role = RolesConstant.Parent,
                    EmailConfirmed = true,
                    FName = "والد",
                    LName = "العمري",
                    PhoneNumber = "0556114239",
                    DOB = DateTime.Today.AddYears(-30),
                    Gender = Gender.Female,
                    NationalID = "884411",
                    Major = "الوالدين"
                };
                var check = userManager.Create(user, "Tahseen@123");
                if (check.Succeeded)
                {
                    userManager.AddToRole(user.Id, RolesConstant.Parent);
                }
            }

            if (!roleManager.RoleExists(RolesConstant.Doctor))
            {
                role3.Name = RolesConstant.Doctor;
                roleManager.Create(role3);
                ApplicationUser user = new ApplicationUser
                {
                    Email = "raghadlu555@gmail.com",
                    UserName = "Doaa9",
                    Role = RolesConstant.Doctor,
                    EmailConfirmed = true,
                    FName = "دعاء",
                    LName = "الـــ",
                    PhoneNumber = "0547809100",
                    DOB = DateTime.Today.AddYears(-30),
                    Gender = Gender.Female,
                    NationalID = "884411",
                    Major = "دكتور"
                };
                var check = userManager.Create(user, "Tahseen@123");
                if (check.Succeeded)
                {
                    userManager.AddToRole(user.Id, RolesConstant.Doctor);
                }
            }

            if (!roleManager.RoleExists(RolesConstant.Vaccinator))
            {
                role4.Name = RolesConstant.Vaccinator;
                roleManager.Create(role4);
                ApplicationUser user = new ApplicationUser
                {
                    Email = "raghadlu333@gmail.com",
                    UserName = "Maryam9",
                    Role = RolesConstant.Vaccinator,
                    EmailConfirmed = true,
                    FName = "مريم",
                    LName = "الـــ",
                    PhoneNumber = "0569743550",
                    DOB = DateTime.Today.AddYears(-30),
                    Gender = Gender.Female,
                    NationalID = "884412",
                    Major = "ممرض"
                };
                var check = userManager.Create(user, "Tahseen@123");
                if (check.Succeeded)
                {
                    userManager.AddToRole(user.Id, RolesConstant.Vaccinator);
                }
            }

            if (!roleManager.RoleExists(RolesConstant.Clinic))
            {
                role5.Name = RolesConstant.Clinic;
                roleManager.Create(role5);
                ApplicationUser user = new ApplicationUser
                {
                    Email = "TahseenFYP@gmail.com",
                    UserName = "Raghad9997",
                    Role = RolesConstant.Vaccinator,
                    EmailConfirmed = true,
                    FName = "رغد",
                    LName = "العمري",
                    PhoneNumber = "0556114239",
                    DOB = DateTime.Today.AddYears(-30),
                    Gender = Gender.Female,
                    NationalID = "884413",
                    Major = "العيادة",
                    ClinicId = db.Clinics.First().ClinicId
                };
                var check = userManager.Create(user, "Tahseen@123");
                if (check.Succeeded)
                {
                    userManager.AddToRole(user.Id, RolesConstant.Vaccinator);
                }
            }
        }
    }
}
