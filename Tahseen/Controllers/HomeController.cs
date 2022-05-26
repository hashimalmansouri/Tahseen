using System.Linq;
using System.Net;
using System.Web.Mvc;
using Tahseen.Models;
using Tahseen.Models.Enums;
using Tahseen.Models.ViewModels;
using System.Data.Entity;

namespace Tahseen.Controllers
{
    public class HomeController : Controller
    {
        private TahseenContext _db = new TahseenContext();
        public ActionResult Index() => View();

        public ActionResult About() => View();

        public ActionResult BasicVaccinations() => View();

        public ActionResult Instructions() => View();

        [Authorize]
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

        [Authorize]
        public ActionResult Appointments() => View();

        public ActionResult TermsAndConditions() => View();

        [Authorize]
        public ActionResult Children()
        {
            return View(_db.Children.ToList());
        }

        [Authorize]
        public ActionResult ChildProfile(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var child = _db.Children.Include(c => c.Immunizations)
                .Include(c => c.Clinic).Include(c => c.ChildHealths)
                .SingleOrDefault(c => c.ChildID == id);
            if (child == null)
            {
                return HttpNotFound();
            }
            ViewBag.Vaccines = _db.Vaccines.ToList();
            return View(child);
        }

    }
}