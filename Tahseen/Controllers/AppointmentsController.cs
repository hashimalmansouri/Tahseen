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
    public class AppointmentsController : Controller
    {
        private TahseenContext db = new TahseenContext();

        // GET: Appointments
        public ActionResult Index()
        {
            var appointments = db.Appointments.Include(a => a.ClinicAppointment)
                .Include(a => a.Child).Include(a => a.Vaccine);
            return View(appointments.ToList());
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Include(a => a.Vaccine)
                .Include(a => a.ClinicAppointment).Include(a => a.Child)
                .SingleOrDefault(a => a.AppointmentId == id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            ViewBag.VaccineId = db.Vaccines.ToList();
            ViewBag.ChildId = new SelectList(db.Children.Select(c => new { ChildID = c.ChildID,
                Name = c.ChildID + " - " + c.FName + " " + c.LName }), "ChildID", "Name");
            ViewBag.ClinicAppointmentId = db.ClinicAppointments.ToList();
            return View();
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentId,ChildId,ClinicAppointmentId,VaccineId,Place")] Appointment appointment)
        {
            ViewBag.VaccineId = db.Vaccines.ToList();
            ViewBag.ChildId = new SelectList(db.Children.Select(c => new {
                ChildID = c.ChildID,
                Name = c.ChildID + " - " + c.FName + " " + c.LName
            }), "ChildID", "Name", appointment.ChildId);
            ViewBag.ClinicAppointmentId = db.ClinicAppointments.ToList();

            if (ModelState.IsValid)
            {
                if (db.Appointments.Any(c => c.ClinicAppointmentId == appointment.ClinicAppointmentId &&
                    c.ChildId == appointment.ChildId && c.VaccineId == appointment.VaccineId))
                {
                    ViewBag.Error = "لقد تم حجز هذا الموعد من قبل.";
                    return View(appointment);
                }
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.VaccineId = db.Vaccines.ToList();
            ViewBag.ChildId = new SelectList(db.Children.Select(c => new {
                ChildID = c.ChildID,
                Name = c.ChildID + " - " + c.FName + " " + c.LName
            }), "ChildID", "Name", appointment.ChildId);
            ViewBag.ClinicAppointmentId = db.ClinicAppointments.ToList();
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentId,ChildId,ClinicAppointmentId,VaccineId,Place")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VaccineId = db.Vaccines.ToList();
            ViewBag.ChildId = new SelectList(db.Children.Select(c => new {
                ChildID = c.ChildID,
                Name = c.ChildID + " - " + c.FName + " " + c.LName
            }), "ChildID", "Name", appointment.ChildId);
            ViewBag.ClinicAppointmentId = db.ClinicAppointments.ToList();
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
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
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
