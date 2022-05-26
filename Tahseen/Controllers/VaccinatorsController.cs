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
using Tahseen.Models.Enums;

namespace Tahseen.Controllers
{
    [Authorize(Roles = RolesConstant.Vaccinator)]
    public class VaccinatorsController : Controller
    {
        private TahseenContext db = new TahseenContext();

        public ActionResult Index() => View();

        public ActionResult Approval()
        {
            return View(db.Approvals.Include(a => a.Child)
                .Where(a => a.Status == ApprovalStatus.Accept).ToList());
        }

        public ActionResult ApprovalDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var approvedChild = db.Approvals.Include(a => a.Child)
                .Where(a => a.ApprovalId == id).SingleOrDefault();
            if (approvedChild == null)
            {
                return HttpNotFound();
            }
            return View(approvedChild);
        }

        public ActionResult RegisterVaccination(string childId, int? approvalId)
        {
            if (childId == null || approvalId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var child = db.Children.Find(childId);
            if (child == null)
            {
                return HttpNotFound();
            }
            var approval = db.Approvals.Find(approvalId);
            if (approval == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChildName = child.FullName;
            ViewBag.ApprovalId = approvalId;
            ViewBag.VaccineId = db.Vaccines.Where(v => v.Age == approval.VaccineType).ToList();
            return View(new Immunization() 
            { 
                NationalID = child.ChildID,
                VaccinationDate = DateTime.Today,
                DateOfNextDose = DateTime.Today 
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterVaccination(Immunization immunization, int? approvalId)
        {
            var child = db.Children.Find(immunization.NationalID);
            if (child == null)
            {
                ViewBag.Error = "هذا الطفل غير موجود.";
                return View(immunization);
            }
            var approval = db.Approvals.Find(approvalId);
            if (approval == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChildName = child.FullName;
            ViewBag.ApprovalId = approvalId;
            ViewBag.VaccineId = db.Vaccines.Where(v => v.Age == approval.VaccineType).ToList();
            //if (ModelState.IsValid)
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

                approval.Status = ApprovalStatus.Immunized;
                db.Entry(approval).State = EntityState.Modified;

                db.SaveChanges();

                ViewBag.Success = "تم تسجيل التطعيم بنجاح.";
                
                var parent = await db.Users.FirstOrDefaultAsync(u => u.NationalID.Equals(child.ParentNationalId));
                if (parent == null)
                {
                    ViewBag.Error = "أب / أم هذا الطفل غير موجود.";
                    return View(immunization);
                }
                string body;
                using (var sr = new StreamReader(Server.MapPath("~/App_Data/Templates/Instructions.html")))
                {
                    body = sr.ReadToEnd();
                }
                try
                {
                    var message = new IdentityMessage
                    {
                        Subject = "ارشادات الرعاية بعد التطعيم",
                        Destination = parent.Email,
                        //Destination = "raghadlu999@gmail.com",
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
    }
}
