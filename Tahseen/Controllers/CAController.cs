using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tahseen.Models;

namespace Tahseen.Controllers
{
    public class CAController : Controller
    {
        private TahseenContext db = new TahseenContext();

        // GET: CA
        public ActionResult Index()
        {
            var clinicAppointments = db.ClinicAppointments.Include(c => c.Clinic);
            return View(clinicAppointments.ToList());
        }

        // GET: CA/Details/5
        public ActionResult Details(int? id)
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

        // GET: CA/Create
        public ActionResult Create()
        {
            ViewBag.ClinicId = new SelectList(db.Clinics, "ClinicId", "Name");
            return View();
        }

        // POST: CA/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Time,ClinicId")] ClinicAppointment clinicAppointment)
        {
            if (ModelState.IsValid)
            {
                db.ClinicAppointments.Add(clinicAppointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClinicId = new SelectList(db.Clinics, "ClinicId", "Name", clinicAppointment.ClinicId);
            return View(clinicAppointment);
        }

        // GET: CA/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.ClinicId = new SelectList(db.Clinics, "ClinicId", "Name", clinicAppointment.ClinicId);
            return View(clinicAppointment);
        }

        // POST: CA/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Time,ClinicId")] ClinicAppointment clinicAppointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clinicAppointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClinicId = new SelectList(db.Clinics, "ClinicId", "Name", clinicAppointment.ClinicId);
            return View(clinicAppointment);
        }

        // GET: CA/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: CA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClinicAppointment clinicAppointment = db.ClinicAppointments.Find(id);
            db.ClinicAppointments.Remove(clinicAppointment);
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
