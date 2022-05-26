using System.Linq;
using System.Web.Mvc;
using Tahseen.Models;
using Tahseen.Models.Enums;
using System.Data.Entity;
using Tahseen.Models.ViewModels;

namespace Tahseen.Controllers
{
    [Authorize(Roles = RolesConstant.HSP)]
    public class HSPsController : Controller
    {
        private TahseenContext db = new TahseenContext();
        // GET: HSPs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Statistics()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetStatistics()
        {
            var allChildren = db.Children.Count();
            int atBirth = db.Immunizations.Include(c => c.Vaccine).Where(c => c.Vaccine.Age == Age.AtBirth).Distinct().Count();
            int twoMonths = db.Immunizations.Include(c => c.Vaccine).Where(c => c.Vaccine.Age == Age.TwoMonths).Distinct().Count();
            int fourMonths = db.Immunizations.Include(c => c.Vaccine).Where(c => c.Vaccine.Age == Age.FourMonths).Distinct().Count();
            int sixMonths = db.Immunizations.Include(c => c.Vaccine).Where(c => c.Vaccine.Age == Age.SixMonths).Distinct().Count();
            int nineMonths = db.Immunizations.Include(c => c.Vaccine).Where(c => c.Vaccine.Age == Age.NineMonths).Distinct().Count();
            int oneYear = db.Immunizations.Include(c => c.Vaccine).Where(c => c.Vaccine.Age == Age.OneYear).Distinct().Count();
            int oneYearAndHalf = db.Immunizations.Include(c => c.Vaccine).Where(c => c.Vaccine.Age == Age.OneYearAndHalf).Distinct().Count();
            int twoYears = db.Immunizations.Include(c => c.Vaccine).Where(c => c.Vaccine.Age == Age.TwoYears).Distinct().Count();
            int fourToSixYears = db.Immunizations.Include(c => c.Vaccine).Where(c => c.Vaccine.Age == Age.FourToSixYears).Distinct().Count();
            var model = new StatisticsViewModel
            {
                AllChildren = allChildren,
                AtBirth = atBirth,
                TwoMonths = twoMonths,
                FourMonths = fourMonths,
                SixMonths = sixMonths,
                NineMonths = nineMonths,
                OneYear = oneYear,
                OneYearAndHalf = oneYearAndHalf,
                TwoYears = twoMonths,
                FourToSixYears = fourToSixYears
            };
            return Json(model);
        }
    }
}