using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tahseen.Models.Enums;

namespace Tahseen.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        //[Required(ErrorMessage = "{0} حقل مطلوب.")]
        //[EmailAddress]
        //[Display(Name = "البريد الإلكتروني")]
        //public string Email { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "اسم المستخدم")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [StringLength(100, ErrorMessage = "{0} يجب أن تكون على الأقل من {2} حرف أو رمز.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [Display(Name = "تذكرني ؟")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [EmailAddress]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [StringLength(100, ErrorMessage = "{0} يجب أن تكون على الأقل من {2} حرف أو رمز.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة المرور")]
        [Compare("Password", ErrorMessage = "كلمة المرور وتأكيد كلمة المرور غير متطابقتان")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "الصلاحية")]
        public string Role { get; set; }
    }

    public class RegisterPractitionerViewModel
    {
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "اسم المستخدم")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [EmailAddress]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [StringLength(100, ErrorMessage = "{0} يجب أن تكون على الأقل من {2} حرف أو رمز.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة المرور")]
        [Compare("Password", ErrorMessage = "كلمة المرور وتأكيد كلمة المرور غير متطابقتان")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "المهنة")]
        public string Role { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "رقم الجوال")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "الاسم الأول")]
        public string FName { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "الاسم الأخير")]
        public string LName { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "رقم الهوية / الإقامة")]
        public string NationalID { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "الجنس")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "تاريخ الميلاد")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
