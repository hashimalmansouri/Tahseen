using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using Tahseen.Models;
using Tahseen.Models.Enums;
using Tahseen.Models.ViewModels;

namespace Tahseen.Controllers
{
    public class HomeController : Controller
    {
        private TahseenContext _db = new TahseenContext();
        public ActionResult Index() => View();

        public ActionResult About() => View();

        public ActionResult Instructions() => View();

        public ActionResult PractitionerDetails(string title)
        {
            return View(new PractitionerDetailsViewModel() { Title = title });
        }

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var names = (from doctor in _db.Users
                         where doctor.NationalID.ToString().StartsWith(prefix)
                         && doctor.Role.Equals(RolesConstant.Doctor)
                         || doctor.Role.Equals(RolesConstant.Vaccinator)
                         select new { label = doctor.NationalID.ToString(),
                                      val = doctor.NationalID.ToString() }).ToList();
            return Json(names);
        }

        public JsonResult GetPractitioner(string id)
        {
            var practitioner = _db.Users.FirstOrDefault(d => d.NationalID.Equals(id));
            return Json(new PractitionerDetailsViewModel 
            {
                NationalID = practitioner.NationalID.ToString(),
                DOB = practitioner.DOB.Value.ToString("yyyy-MM-yy"),
                FullName = practitioner.FullName,
                Email = practitioner.Email,
                Gender = practitioner.Gender.GetDisplayName(),
                Major = practitioner.Role,
                PhoneNumber = practitioner.PhoneNumber
            });
        }

        public ActionResult Contact() => View();

        public ActionResult Appointments() => View();

        [Authorize]
        public ActionResult PersonalProfile()
        {
            var userId = User.Identity.GetUserId();
            var user = _db.Users.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(new ProfileViewModel 
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                NationalID = user.NationalID,
                DOB = user.DOB.Value.ToString("yyyy-MM-dd")
            });
        }

    }
}