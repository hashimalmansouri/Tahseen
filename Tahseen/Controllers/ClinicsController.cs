using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tahseen.Models;
using Tahseen.Models.ViewModels;

namespace Tahseen.Controllers
{
    public class ClinicsController : Controller
    {
        private TahseenContext db = new TahseenContext();
        // GET: Clinics
        public ActionResult Index()
        {
            return View();
        }       

        public ActionResult Children()
        {
            return View(db.Children.ToList());
        }

        public ActionResult ChildProfile(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var child = db.Children.Include(c => c.Immunizations).Include(c => c.Clinic)
                .SingleOrDefault(c => c.ChildID == id);
            if (child == null)
            {
                return HttpNotFound();
            }
            ViewBag.Vaccines = db.Vaccines.ToList();
            return View(child);
            //return View(db.Children.Include(c => c.Clinic).First());
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


    }
}