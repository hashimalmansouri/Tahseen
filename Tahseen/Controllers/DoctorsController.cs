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
        // GET: خدمات الدكتور
        public ActionResult Index()
        {
            return View();
        }

        // دليل تطور صحية الطفل
        public ActionResult ChildDevelopment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChildDevelopment(ChildDevelopment model)
        {
            // تحقق من المدخلات
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "خطأ، تأكد من البيانات المدخلة.";
                return View(model);
            }
            // التحقق من رقم الهوية
            if (!db.Children.Any(c => c.ChildID.Equals(model.ChildNationalID)))
            {
                ViewBag.Error = "رقم الهوية المدخل غير معرّف مسبقًا، تحقق وأعد الإرسال مرة أخرى";
                return View(model);
            }
            // اضافة التوصيات
            db.ChildDevelopments.Add(model);
            // تطبيق التغييرات في قاعدة البيانات
            db.SaveChanges();
            ViewBag.Success = "تم إرسال الموافقة بنجاح.";
            return View();
        }
        // الموافقات
        public ActionResult Approval()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approval(Approval model)
        {
            // تحقق من المدخلات
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "خطأ، تأكد من البيانات المدخلة.";
                return View(model);
            }
            // التحقق من رقم الهوية
            if (!db.Children.Any(c => c.ChildID.Equals(model.ChildNationalID)))
            {
                ViewBag.Error = "رقم الهوية المدخل غير معرّف مسبقًا، تحقق وأعد الإرسال مرة أخرى";
                return View(model);
            }
            // تحقق اذا التطعيم موجود من قبل
            if (db.Approvals.Any(c => c.ChildNationalID.Equals(model.ChildNationalID) && c.VaccineType == model.VaccineType))
            {
                ViewBag.Error = "تمت الموافقة على هذا التطعيم من قبل.";
                return View(model);
            }
            // قبول التطعيم
            model.Status = ApprovalStatus.Accept;
            // اضافة الموافقة
            db.Approvals.Add(model);
            // حفظ التغييرات
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