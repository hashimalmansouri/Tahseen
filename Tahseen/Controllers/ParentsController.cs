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
using Tahseen.Helpers;
using Tahseen.Models;
using Tahseen.Models.ViewModels;

namespace Tahseen.Controllers
{
    [Authorize(Roles = RolesConstant.Parent)]
    public class ParentsController : Controller
    {
        private TahseenContext db = new TahseenContext();
        // خدمات الوالدين
        public ActionResult Index()
        {
            return View();
        }
        // المواعيد
        public ActionResult Appointments()
        {
            return View();
        }
        // التابعين
        public ActionResult Dependents() 
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
            // جلب اطفال هذا الاب
            return View(db.Children.Where(c => c.ParentNationalId.Equals(parent.NationalID)).ToList());
        }

        public ActionResult ChildRecords(string id)
        {
            // تحقق اذا المفتاح ليس فارغ
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // جلب الطفل مع تطعيماته
            var child = db.Children.Include(c => c.Immunizations)
                .SingleOrDefault(c => c.ChildID == id);
            // تحقق ان الطقل ليسس فارغ
            if (child == null)
            {
                return HttpNotFound();
            }
            // ارسال جميع التطيعمات الى الفيو
            ViewBag.Vaccines = db.Vaccines.ToList();
            return View(child);
        }
        public ActionResult ContactDoctor() 
        {
            // جلب ايميلات الدكاترة وارسالها للفيو من اجل عرضها دروب  داون
            ViewBag.DoctorEmail = new SelectList(db.Users.Where(u => u.Role.Equals(RolesConstant.Doctor)), "Email", "FullName");
            return View(new ContactDoctorViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ContactDoctor(ContactDoctorViewModel model)
        {
            // تحقق من المدخلات
            if (ModelState.IsValid)
            {
                // جلب مفتاح الاب
                var userId = User.Identity.GetUserId();
                // جلب الاب
                var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);
                // تحقق ان الاب ليس فارغ
                if (user == null)
                {
                    return HttpNotFound();
                }
                string body;
                // جلب قالب ايميل التواصل مع الدكتور
                using (var sr = new StreamReader(Server.MapPath("~/App_Data/Templates/ContactDoctor.html")))
                {
                    body = sr.ReadToEnd();
                }
                // ارسال الاسم والبريد الالكتروني للاب والرسالة للقالب
                var messageBody = string.Format(body, user.FullName, user.Email, model.Message);
                try
                {
                    // ارسال عبر الجيميل
                    var message = new IdentityMessage
                    {
                        Subject = "رسالة تواصل من الوالدين",
                        Destination = user.Email,
                        Body = messageBody
                    };
                    MailSender.SendMail(message);
                    ViewBag.Success = "تم إرسال الرسالة بنجاح.";
                }
                catch
                {
                    ViewBag.Error = "حدث خطأ أثناء إرسال الرسالة، الرجاء المحاولة مرة أخرى.";
                }
            }
            ViewBag.DoctorEmail = new SelectList(db.Users.Where(u => u.Role.Equals(RolesConstant.Doctor)), "Email", "FullName", model.DoctorEmail);
            return View(model);
        }

        
    }
}