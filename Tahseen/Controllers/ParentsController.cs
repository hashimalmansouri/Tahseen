﻿using Microsoft.AspNet.Identity;
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
    public class ParentsController : Controller
    {
        private TahseenContext db = new TahseenContext();

        public ActionResult Index() => View();

        public ActionResult Dependents() 
        {
            //var parentId = User.Identity.GetUserId();
            //return View(db.Children.Where(p => p.ParentId == parentId).ToList());
            return View(db.Children.ToList());
        }

        public ActionResult ChildRecords(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var parentId = User.Identity.GetUserId();
            //var children = db.Children.Include(c => c.Immunizations).SingleOrDefault(p => p.ChildId == id && p.ParentId == parentId);
            var child = db.Children.Include(c => c.Immunizations)
                .SingleOrDefault(c => c.ChildID == id);
            if (child == null)
            {
                return HttpNotFound();
            }
            ViewBag.Vaccines = db.Vaccines.ToList();
            return View(child);
        }
        public ActionResult ContactDoctor() 
        {
            ViewBag.DoctorEmail = new SelectList(db.Users.Where(u => u.Role.Equals(RolesConstant.Doctor)), "Email", "FullName");
            return View(new ContactDoctorViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ContactDoctor(ContactDoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    return HttpNotFound();
                }
                string body;
                using (var sr = new StreamReader(Server.MapPath("~/App_Data/Templates/contact.html")))
                {
                    body = sr.ReadToEnd();
                }
                var messageBody = string.Format(body, user.FullName, user.Email, model.Message);
                try
                {
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