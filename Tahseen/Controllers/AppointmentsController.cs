using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tahseen.Models;
using Tahseen.Models.Enums;

namespace Tahseen.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private TahseenContext db = new TahseenContext();

        // جميع مواعيد الاب
        public ActionResult Index()
        {
            // id for parent
            var parentId = User.Identity.GetUserId();
            // جلب الاب
            var parent = db.Users.Find(parentId);
            // تحقق اذا الاب ليس فارغ
            if (parent == null)
            {
                return HttpNotFound();
            }
            var appointments = db.Appointments.Where(a => a.Child.ParentNationalId.Equals(parent.NationalID))
                .Include(a => a.ClinicAppointment)
                .Include(a => a.Child).Include(a => a.Vaccine);
            return View(appointments.ToList());
        }

        // تفاصيل الموعد
        public ActionResult Details(int? id)
        {
            // تحقق اذا المفتاح غير فارغ
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // جلب الموعد
            Appointment appointment = db.Appointments.Include(a => a.Vaccine)
                .Include(a => a.ClinicAppointment).Include(a => a.Child)
                .SingleOrDefault(a => a.AppointmentId == id);
            // تحقق اذا الموعد ليس فارغ
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        public ActionResult Create()
        {
            // جلب مفتاح الاب
            var parentId = User.Identity.GetUserId();
            // جلب بيانات الاب
            var parent = db.Users.Find(parentId);
            // جلب التطعيمات
            ViewBag.VaccineId = db.Vaccines.ToList();
            // جلب اطفال الاب
            ViewBag.ChildId = new SelectList(db.Children.Where(c => c.ParentNationalId.Equals(parent.NationalID)).Select(c => new { ChildID = c.ChildID,
                Name = c.ChildID + " - " + c.FName + " " + c.LName }), "ChildID", "Name");
            // جلب مواعيد العيادة
            ViewBag.ClinicAppointmentId = db.ClinicAppointments.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Appointment appointment)
        {
            // جلب مفتاح الاب
            var parentId = User.Identity.GetUserId();
            // جلب بيانات الاب
            var parent = db.Users.Find(parentId);
            // جلب التطعيمات
            ViewBag.VaccineId = db.Vaccines.ToList();
            // جلب اطفال الاب
            ViewBag.ChildId = new SelectList(db.Children.Where(c => c.ParentNationalId.Equals(parent.NationalID)).Select(c => new {
                ChildID = c.ChildID,
                Name = c.ChildID + " - " + c.FName + " " + c.LName
            }), "ChildID", "Name", appointment.ChildId);
            // جلب مواعيد العيادة
            ViewBag.ClinicAppointmentId = db.ClinicAppointments.ToList();
            // تحقق من المدخلات
            if (ModelState.IsValid)
            {
                // تحقق ما اذا كان الموعد تم حجزه من قبل
                if (db.Appointments.Any(c => c.ClinicAppointmentId == appointment.ClinicAppointmentId &&
                    c.ChildId == appointment.ChildId && c.VaccineId == appointment.VaccineId))
                {
                    ViewBag.Error = "لقد تم حجز هذا الموعد من قبل.";
                    return View(appointment);
                }
                // اضافة الموعد
                db.Appointments.Add(appointment);
                // حفظ التغييرات
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            // التحقق اذا المفتاح فارغ
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // استعلام عن موعد
            Appointment appointment = db.Appointments.Find(id);
            // التحقق اذا كان الاستعلام فارغ
            if (appointment == null)
            {
                return HttpNotFound();
            }
            // جلب مفتاح الاب
            var parentId = User.Identity.GetUserId();
            // جلب بيانات الاب
            var parent = db.Users.Find(parentId);
            // جلب التطعيمات
            ViewBag.VaccineId = db.Vaccines.ToList();
            // جلب اطفال الاب
            ViewBag.ChildId = new SelectList(db.Children.Where(c => c.ParentNationalId.Equals(parent.NationalID)).Select(c => new {
                ChildID = c.ChildID,
                Name = c.ChildID + " - " + c.FName + " " + c.LName
            }), "ChildID", "Name", appointment.ChildId);
            // جلب مواعيد العيادة
            ViewBag.ClinicAppointmentId = db.ClinicAppointments.ToList();
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Appointment appointment)
        {
            // تحقق من المدخلات
            if (ModelState.IsValid)
            {
                // تعديل الموعد
                db.Entry(appointment).State = EntityState.Modified;
                // حفظ التغييرات
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // جلب مفتاح الاب
            var parentId = User.Identity.GetUserId();
            // جلب بيانات الاب
            var parent = db.Users.Find(parentId);
            // جلب التطعيمات
            ViewBag.VaccineId = db.Vaccines.ToList();
            // جلب اطفال الاب
            ViewBag.ChildId = new SelectList(db.Children.Where(c => c.ParentNationalId.Equals(parent.NationalID)).Select(c => new {
                ChildID = c.ChildID,
                Name = c.ChildID + " - " + c.FName + " " + c.LName
            }), "ChildID", "Name", appointment.ChildId);
            // جلب مواعيد العيادة
            ViewBag.ClinicAppointmentId = db.ClinicAppointments.ToList();
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            // التحقق اذا المفتاح فارغ
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // استعلام عن موعد
            Appointment appointment = db.Appointments.Find(id);
            // التحقق اذا كان الاستعلام فارغ
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // جلب موعد
            Appointment appointment = db.Appointments.Find(id);
            // حذف موعد
            db.Appointments.Remove(appointment);
            // حفظ التغييرات 
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
