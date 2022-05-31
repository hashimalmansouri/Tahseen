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
        // الخدمات
        public ActionResult Index()
        {
            return View();
        }              
        // اضافة طفل
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
                // اسناد العيادة للطفل
                child.ClinicId = db.Clinics.First().ClinicId;
                // اضافة الطفل
                db.Children.Add(child);
                // تطبيق التغييرات في قاعدة البيانات
                await db.SaveChangesAsync();
                // رسالة
                ViewBag.Success = "تم تسجيل الطفل بنجاح";
                return View(child);
            }
            // خطا
            ViewBag.Error = "يوجد خطأ في البيانات المدخلة، تحقق وأعد التسجيل مرة أخرى";
            return View(child);
        }

        // الكادر الصحي
        public ActionResult Medical()
        {
            return View();
        }

        public ActionResult Appointments()
        {
            // مواعيد العيادة
            return View(db.ClinicAppointments.ToList());
        }

        public ActionResult NewAppointment()
        {
            // موعد جديد
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewAppointment(ClinicAppointment clinicAppointment)
        {
            // التحقق من صحة البيانات
            if (ModelState.IsValid)
            {
                // اسناد العيادة للموعد
                clinicAppointment.ClinicId = db.Clinics.First().ClinicId;
                // اضافة الموعد
                db.ClinicAppointments.Add(clinicAppointment);
                // تطبيق التغييرات في قاعدة البيانات
                db.SaveChanges();
                // التوجيه لصفحة المواعيد
                return RedirectToAction(nameof(Appointments));
            }
            return View(clinicAppointment);
        }

        public ActionResult EditAppointment(int? id)
        {
            // التحقق اذا المفتاح فارغ
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // استعلام عن موعد
            ClinicAppointment clinicAppointment = db.ClinicAppointments.Find(id);
            // التحقق اذا كان الاستعلام فارغ
            if (clinicAppointment == null)
            {
                return HttpNotFound();
            }
            // ارسال للفيو
            return View(clinicAppointment);
        }

        [HttpPost]
        // حماية من الهجمات
        [ValidateAntiForgeryToken]
        public ActionResult EditAppointment(ClinicAppointment clinicAppointment)
        {
            // التحقق من صحة المدخلات
            if (ModelState.IsValid)
            {
                // اسناد العيادة للموعد
                clinicAppointment.ClinicId = db.Clinics.First().ClinicId;
                // تعديل الموعد
                db.Entry(clinicAppointment).State = EntityState.Modified;
                // تطبيق التغييرات في قاعدة البيانات
                db.SaveChanges();
                // التوجيه لصفحة المواعيد
                return RedirectToAction(nameof(Appointments));
            }
            return View(clinicAppointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAppointment(int id)
        {
            // جلب الموعد
            ClinicAppointment clinicAppointment = db.ClinicAppointments.Find(id);
            // حذف الموعد
            db.ClinicAppointments.Remove(clinicAppointment);
            // اذا تنفذ الكود كامل لي بداخل التراي لايدخل الى الكاتش
            try
            {
                // يحفظ التغييرات في داتابيس
                db.SaveChanges();
                ViewBag.Success = "تم حذف الموعد بنجاح.";
            }
            // اذا لم يتنفذ احد اكواد التراي يدخل الى الكاتش
            catch
            {
                ViewBag.Error = "حدث خطأ أثناء حذف الموعد";
            }
            return RedirectToAction(nameof(Appointments));
        }

        public ActionResult PractitionerDetails(string title)
        {
            // صفحة بيانات الاطباء او الممرضين بحسب العنوان المرر
            return View(new PractitionerDetailsViewModel() { Title = title });
        }

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            // كود وظيفته اظهار الاقتراحات التي تظهر اثناء كتابة اول الارقام من رقم الهوية للدكتور او الممرض
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
            // جلب بيانات الدكتور او الممرض وارسالها ك json
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