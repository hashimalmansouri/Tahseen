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