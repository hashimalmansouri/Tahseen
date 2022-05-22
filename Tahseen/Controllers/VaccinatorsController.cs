using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tahseen.Helpers;
using Tahseen.Models;

namespace Tahseen.Controllers
{
    public class VaccinatorsController : Controller
    {
        private TahseenContext db = new TahseenContext();



        public ActionResult Create()
        {
            ViewBag.VaccineId = db.Vaccines.ToList();
            return View(new Immunization() 
            { 
                VaccinationDate = DateTime.Today,
                DateOfNextDose = DateTime.Today 
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,NationalID,VaccineId,VaccinationDate,DoseNo,DateOfNextDose,VaccinatorId")] Immunization immunization)
        {
            ViewBag.VaccineId = db.Vaccines.ToList();
            if (ModelState.IsValid)
            {
                if (!db.Children.Any(c => c.ChildID.Equals(immunization.NationalID)))
                {
                    ViewBag.Error = "خطأ، رقم الهوية أو الإقامة غير موجود.";
                    return View(immunization);
                }
                else if (db.Immunizations.Any(v => v.VaccineId.Equals(immunization.VaccineId) && v.NationalID.Equals(immunization.NationalID)))
                {
                    ViewBag.Error = $"هذا اللقاح ({db.Vaccines.First(v => v.Id == immunization.VaccineId).Name}) قد تم تطعيمه من قبل.";
                    return View(immunization);
                }
                var userId = User.Identity.GetUserId();
                immunization.VaccinatorId = userId;
                db.Immunizations.Add(immunization);
                db.SaveChanges();
                ViewBag.Success = "تم تسجيل التطعيم بنجاح.";
                var child = await db.Children.FindAsync(immunization.NationalID);
                if (child == null)
                {
                    ViewBag.Error = "هذا الطفل غير موجود.";
                    return View(immunization);
                }
                var parent = await db.Users.FirstOrDefaultAsync(u => u.NationalID.Equals(child.ParentNationalId));
                if (parent == null)
                {
                    ViewBag.Error = "أب / أم هذا الطفل غير موجود.";
                    return View(immunization);
                }
                string body;
                using (var sr = new StreamReader(Server.MapPath("~/App_Data/Templates/instructions.html")))
                {
                    body = sr.ReadToEnd();
                }
                try
                {
                    var message = new IdentityMessage
                    {
                        Subject = "ارشادات الرعاية بعد التطعيم",
                        //Destination = parent.Email,
                        Destination = "raghadlu999@gmail.com",
                        Body = body
                    };
                    MailSender.SendMail(message);
                    ViewBag.Success += " وتم إرسال ارشادات الرعاية بنجاح.";
                }
                catch
                {
                    ViewBag.Error = "حدث خطأ أثناء إرسال الارشادات، الرجاء المحاولة مرة أخرى.";
                }
            }
            return View(immunization);
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
