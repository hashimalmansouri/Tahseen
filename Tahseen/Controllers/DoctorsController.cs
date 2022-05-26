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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var child = db.Children.Find(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChildName = child.FullName;
            return View(new ChildHealth { ChildNationalID = child.ChildID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateHealth(ChildHealth model)
        {
            var child = db.Children.Find(model.ChildNationalID);
            if (child == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChildName = child.FullName;
            try
            {
                db.ChildHealths.Add(model);
                db.SaveChanges();
                ViewBag.Success = "تم تحديث العلامات الحيوية للطفل بنجاح.";
            }
            catch
            {
                ViewBag.Error = "حدث خطأ، يرجى المحاولة مرة أخرى.";
            }
            return View(model);
        }
    }
}