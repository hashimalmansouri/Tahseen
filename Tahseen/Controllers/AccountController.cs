using System;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Tahseen.Helpers;
using Tahseen.Models;
using Tahseen.Models.Enums;
using Tahseen.Models.ViewModels;

namespace Tahseen.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private TahseenContext _db = new TahseenContext();


        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        
        [AllowAnonymous]
        public ActionResult SignIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    var user = await UserManager.FindAsync(model.Username, model.Password);
                    var roles = await UserManager.GetRolesAsync(user.Id);
                    if (roles.Contains(RolesConstant.Clinic))
                    {
                        return RedirectToAction("Index", "Clinics");
                    }
                    else if (roles.Contains(RolesConstant.Doctor))
                    {
                        return RedirectToAction("Index", "Doctors");
                    }
                    else if (roles.Contains(RolesConstant.HSP))
                    {
                        return RedirectToAction("Index", "HSPs");
                    }
                    else if (roles.Contains(RolesConstant.Parent))
                    {
                        return RedirectToAction("Index", "Parents");
                    }
                    else if (roles.Contains(RolesConstant.Vaccinator))
                    {
                        return RedirectToAction("Index", "Vaccinators");
                    }
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "محاولة تسجيل دخول خاطئة.");
                    return View(model);
            }
        }

        
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Role = RolesConstant.Parent,
                    EmailConfirmed = true,
                    PhoneNumber = model.PhoneNumber,
                    FName = model.FName,
                    LName = model.LName,
                    DOB = model.DOB,
                    NationalID = model.NationalID,
                    Gender = model.Gender,
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(user.Id, RolesConstant.Parent);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(model);
        }

        [Authorize(Roles = RolesConstant.Clinic)]
        public ActionResult RegisterPractitioner()
        {
            // تمرير دروب داون فيها اسم صلاحية الطبيب والممرضة
            ViewBag.Role = new SelectList(_db.Roles.Where(x => x.Name.Equals(RolesConstant.Doctor) ||
            x.Name.Equals(RolesConstant.Vaccinator)).ToList(), "Name", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterPractitioner(RegisterPractitionerViewModel model)
        {
            // تحقق اذا المدخلات صحيحة
            if (ModelState.IsValid)
            {
                // تعبئة كلاس جدول اليوزر من بيانات المودل
                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    FName = model.FName,
                    LName = model.LName,
                    NationalID = model.NationalID,
                    PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender,
                    DOB = model.DOB,
                    Role = model.Role,
                    Major = model.Role
                };
                // اضافة اليوزر
                var result = await UserManager.CreateAsync(user, model.Password);
                // اذا تمت الاضافة
                if (result.Succeeded)
                {
                    // تحقق من ان الصلاحية مش فارغة
                    if (model.Role != null)
                    {
                        // اضافة اليوزر للصلاحية حقه
                        result = await UserManager.AddToRoleAsync(user.Id, model.Role);
                        // اذا لم تتم الاضافة
                        if (!result.Succeeded)
                        {
                            // رسالة خطأ
                            ViewBag.Error = "خطأ، يرجى التأكد من البيانات المدخلة.";
                            return View();
                        }
                    }
                    // رسالة نجاح
                    ViewBag.Success =  "تم تسجيل الحساب بنجاح.";
                }
            }
            ViewBag.Role = new SelectList(await _db.Roles.Where(x => x.Name.Equals(RolesConstant.Doctor) ||
            x.Name.Equals(RolesConstant.Vaccinator)).ToListAsync(), "Name", "Name");
            return View(model);
        }

        
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ViewBag.Error = "البريد الإلكتروني غير موجود.";
                    return View(model);
                }
                // خدعة لتوليد كود مكون من 6 أرقام، وذلك من أجل التحقق منه لاحقا
                // لأنه لابد نجعل لكل كود مدة زمنية مالم يصبح منتهي
                string code = await UserManager.GenerateChangePhoneNumberTokenAsync(user.Id, user.PhoneNumber);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                string body;
                using (var sr = new StreamReader(Server.MapPath("~/App_Data/Templates/ForgotPassword.html")))
                {
                    body = sr.ReadToEnd();
                }
                var messageBody = string.Format(body, user.UserName, callbackUrl, code);
                try
                {
                    var message = new IdentityMessage
                    {
                        Subject = "استرجاع كلمة المرور",
                        Destination = model.Email,
                        Body = messageBody
                    };
                    MailSender.SendMail(message);
                    ViewBag.Success = "تم إرسال رمز استرجاع كلمة المرور بنجاح.";
                }
                catch
                {
                    ViewBag.Error = "حدث خطأ أثناء إلإرسال، الرجاء المحاولة مرة أخرى.";
                    return View(model);
                }
                ViewBag.Link = callbackUrl;
                return View("ForgotPasswordConfirmation", model);
            }
            return View(model);
        }

        
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                TempData["Error"] = "هذا المستخدم غير موجود.";
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            // خدعة من أجل التحقق أن الكود لم تنتهي مدته
            if (!await UserManager.VerifyChangePhoneNumberTokenAsync(user.Id, model.Code, user.PhoneNumber))
            {
                ViewBag.Error = "رمز غير صحيح، يرجى إعادة إرسال الرمز مرة أخرى.";
                return View(model);
            }
            var result = await UserManager.RemovePasswordAsync(user.Id);
            if (result.Succeeded)
            {
                result = await UserManager.AddPasswordAsync(user.Id, model.Password);
                if (result.Succeeded)
                {
                    ViewBag.Success = "تم تغيير كلمة المرور بنجاح.";
                    return View();
                }
            }
            ViewBag.Error = "حدث خطأ، يرجى المحاولة مرة أخرى.";
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<ActionResult> PersonalProfile()
        {
            var userId = User.Identity.GetUserId();
            var user = await _db.Users.Include(u => u.HSP).Include(u => u.Clinic).SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(new ProfileViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                NationalID = user.NationalID,
                DOB = user.DOB.Value.ToString("yyyy-MM-dd"),
                Gender = user.Gender.GetDisplayName(),
                Major = user.Major,
                HSPName = User.IsInRole(RolesConstant.HSP) ? user.HSP.Name : string.Empty,
                ClinicName = User.IsInRole(RolesConstant.Clinic) ? user.Clinic.Name : string.Empty
            });
        }

        [Authorize]
        public async Task<ActionResult> UpdateProfile()
        {
            var userId = User.Identity.GetUserId();
            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(new UpdateProfileViewModel { PhoneNumber = user.PhoneNumber, Email = user.Email });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateProfile(UpdateProfileViewModel model)
        {
            var userId = User.Identity.GetUserId();
            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            await UserManager.UpdateAsync(user);
            if (!string.IsNullOrEmpty(model.OldPassword) && !string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.ConfirmPassword))
            {
                var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
            }
            ViewBag.Success = "تم تحديث بيانات الملف الشخصي بنجاح.";
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}