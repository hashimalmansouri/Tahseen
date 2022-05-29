using System.Linq;
using System.Web.Mvc;
using Tahseen.Models;
using Tahseen.Models.Enums;
using System.Data.Entity;
using Tahseen.Models.ViewModels;
using System.Net;

namespace Tahseen.Controllers
{
    [Authorize(Roles = RolesConstant.Doctor)]
    public class DoctorsController : Controller
    {
        private TahseenContext db = new TahseenContext();
        // GET: Doctors
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChildDevelopment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChildDevelopment(ChildDevelopment model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "خطأ، تأكد من البيانات المدخلة.";
                return View(model);
            }

            if (!db.Children.Any(c => c.ChildID.Equals(model.ChildNationalID)))
            {
                ViewBag.Error = "رقم الهوية المدخل غير معرّف مسبقًا، تحقق وأعد الإرسال مرة أخرى";
                return View(model);
            }
            db.ChildDevelopments.Add(model);
            db.SaveChanges();
            ViewBag.Success = "تم إرسال الموافقة بنجاح.";
            return View();
        }

        public ActionResult Approval()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approval(Approval model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "خطأ، تأكد من البيانات المدخلة.";
                return View(model);
            }

            if (!db.Children.Any(c => c.ChildID.Equals(model.ChildNationalID)))
            {
                ViewBag.Error = "رقم الهوية المدخل غير معرّف مسبقًا، تحقق وأعد الإرسال مرة أخرى";
                return View(model);
            }
            if (db.Approvals.Any(c => c.ChildNationalID.Equals(model.ChildNationalID) && c.VaccineType == model.VaccineType))
            {
                ViewBag.Error = "تمت الموافقة على هذا التطعيم من قبل.";
                return View(model);
            }
            model.Status = ApprovalStatus.Accept;
            db.Approvals.Add(model);
            db.SaveChanges();
            ViewBag.Success = "تم إرسال الموافقة بنجاح.";
            return View();
        }

        public ActionResult UpdateHealth(string id)
        {
            // التحقق ما اذا كان المفتاح لايحتوي على قيمة
            if (id == null)
            {
                // ارجاع خطأ أن هذا الطلب غير صالح
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // استعلام عن الطفل
            var child = db.Children.Find(id);
            if (child == null)
            {
                // ارجاع خطأ اذا الطفل غير موجود
                return HttpNotFound();
            }
            // ارسال اسم الطفل الى الفيو
            ViewBag.ChildName = child.FullName;
            // تمرير رقم هوية الطفل إلى المودل
            return View(new ChildHealth { ChildNationalID = child.ChildID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateHealth(ChildHealth model)
        {
            // مشابه للشرح السابق
            var child = db.Children.Find(model.ChildNationalID);
            if (child == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChildName = child.FullName;
            // اذا تنفذ الكود كامل لي بداخل التراي لايدخل الى الكاتش
            try
            {
                // يضيف المودل
                db.ChildHealths.Add(model);
                // يحفظ التغييرات في داتابيس
                db.SaveChanges();
                // يمرر رسالة نجاح
                ViewBag.Success = "تم تحديث العلامات الحيوية للطفل بنجاح.";
            }
            // اذا لم يتنفذ احد اكواد التراي يدخل الى الكاتش
            catch
            {
                // رسالة خطأ
                ViewBag.Error = "حدث خطأ، يرجى المحاولة مرة أخرى.";
            }
            return View(model);
        }
    }
}