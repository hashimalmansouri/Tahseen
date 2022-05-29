using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tahseen.Models;
using Tahseen.Models.Enums;
using Tahseen.Models.ViewModels;

namespace Tahseen.Controllers
{
    [Authorize(Roles = RolesConstant.Clinic)]
    public class ClinicsController : Controller
    {
        private TahseenContext db = new TahseenContext();
        // GET: Clinics
        public ActionResult Index()
        {
            return View();
        }              

        public ActionResult NewChild()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewChild(Child child)
        {
            if (ModelState.IsValid)
            {
                child.ClinicId = db.Clinics.First().ClinicId;
                db.Children.Add(child);
                await db.SaveChangesAsync();
                ViewBag.Success = "تم تسجيل الطفل بنجاح";
                return View(child);
            }
            ViewBag.Error = "يوجد خطأ في البيانات المدخلة، تحقق وأعد التسجيل مرة أخرى";
            return View(child);
        }

        public ActionResult Medical() => View();

        public ActionResult Appointments()
        {
            return View(db.ClinicAppointments.ToList());
        }

        public ActionResult NewAppointment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewAppointment([Bind(Include = "Id,Date,Time,ChildId")] ClinicAppointment clinicAppointment)
        {
            if (ModelState.IsValid)
            {
                clinicAppointment.ClinicId = db.Clinics.First().ClinicId;
                db.ClinicAppointments.Add(clinicAppointment);
                db.SaveChanges();
                return RedirectToAction(nameof(Appointments));
            }
            return View(clinicAppointment);
        }

        public ActionResult EditAppointment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClinicAppointment clinicAppointment = db.ClinicAppointments.Find(id);
            if (clinicAppointment == null)
            {
                return HttpNotFound();
            }
            return View(clinicAppointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAppointment([Bind(Include = "Id,Date,Time,ChildId")] ClinicAppointment clinicAppointment)
        {
            if (ModelState.IsValid)
            {
                clinicAppointment.ClinicId = db.Clinics.First().ClinicId;
                db.Entry(clinicAppointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(nameof(Appointments));
            }
            return View(clinicAppointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAppointment(int id)
        {
            ClinicAppointment clinicAppointment = db.ClinicAppointments.Find(id);
            db.ClinicAppointments.Remove(clinicAppointment);
            try
            {
                db.SaveChanges();
                ViewBag.Success = "تم حذف الموعد بنجاح.";
            }
            catch 
            {
                ViewBag.Error = "حدث خطأ أثناء حذف الموعد";
            }
            return RedirectToAction(nameof(Appointments));
        }

        public ActionResult PractitionerDetails(string title)
        {
            return View(new PractitionerDetailsViewModel() { Title = title });
        }

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var names = (from doctor in db.Users
                         where doctor.NationalID.ToString().StartsWith(prefix)
                         && doctor.Role.Equals(RolesConstant.Doctor)
                         || doctor.Role.Equals(RolesConstant.Vaccinator)
                         select new
                         {
                             label = doctor.NationalID.ToString(),
                             val = doctor.NationalID.ToString()
                         }).ToList();
            return Json(names);
        }

        public JsonResult GetPractitioner(string id)
        {
            var practitioner = db.Users.FirstOrDefault(d => d.NationalID.Equals(id));
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
    }
}