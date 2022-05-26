using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tahseen.Models.Enums;

namespace Tahseen.Models
{
    public class ProfileViewModel
    {
        [Display(Name = "اسم المستخدم")]
        public string Username { get; set; }
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }
        [Display(Name = "رقم الجوال")]
        public string PhoneNumber { get; set; }
        [Display(Name = "الاسم")]
        public string FullName { get; set; }
        [Display(Name = "رقم الهوية / الإقامة")]
        public string NationalID { get; set; }
        [Display(Name = "تاريخ الميلاد")]
        public string DOB { get; set; }
        [Display(Name = "الجنس")]
        public string Gender { get; set; }
        [Display(Name = "المهنة")]
        public string Major { get; set; }
        [Display(Name = "اسم مزود الخدمة الصحية (المركز الصحي)")]
        public string HSPName { get; set; }
        [Display(Name = "اسم العيادة")]
        public string ClinicName { get; set; }
        [Display(Name = "العنوان")]
        public string Address { get; set; }
    }

    public class UpdateProfileViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور القديمة")]
        public string OldPassword { get; set; }

        [StringLength(100, ErrorMessage = "{0} يجب أن تكون على الأقل من {2} حرف أو رمز.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور الجديدة")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة المرور")]
        [Compare("NewPassword", ErrorMessage = "كلمة المرور وتأكيد كلمة المرور غير متطابقتان")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "رقم الجوال")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صالحة، يجب أن تكون مثل abc@xyz.com")]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }
    }
    public class LoginViewModel
    {
        //[Required(ErrorMessage = "{0} حقل مطلوب.")]
        //[EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صالحة، يجب أن تكون مثل abc@xyz.com")]
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
        [Display(Name = "اسم المستخدم")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صالحة، يجب أن تكون مثل abc@xyz.com")]
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

    public class RegisterPractitionerViewModel
    {
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "اسم المستخدم")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صالحة، يجب أن تكون مثل abc@xyz.com")]
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
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صالحة، يجب أن تكون مثل abc@xyz.com")]
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
        [Display(Name = "رمز التفعيل")]
        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صالحة، يجب أن تكون مثل abc@xyz.com")]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }
        [Display(Name = "رمز التفعيل")]
        public string Code { get; set; }
    }
}
