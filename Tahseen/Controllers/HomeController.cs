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

        public ActionResult TermsAndConditions() => View();

        [Authorize]
        public ActionResult Children()
        {
            return View(_db.Children.ToList());
        }

        [Authorize]
        public ActionResult ChildProfile(string id)
        {
            // التحقق ما اذا كان المفتاح لايحتوي على قيمة
            if (id == null)
            {
                // ارجاع خطأ أن هذا الطلب غير صالح
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // جلب بيانات الطفل مع جميع تطعيماته مع العيادة مع علاماته الحيوية
            var child = _db.Children.Include(c => c.Immunizations)
                .Include(c => c.Clinic).Include(c => c.ChildHealths)
                .SingleOrDefault(c => c.ChildID == id);
            if (child == null)
            {
                // ارجاع خطأ اذا الطفل غير موجود
                return HttpNotFound();
            }
            // تمرير جميع التطعيمات الى الفيو
            ViewBag.Vaccines = _db.Vaccines.ToList();
            return View(child);
        }

    }
}