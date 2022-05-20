using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tahseen.Models.ViewModels
{
    public class PractitionerDetailsViewModel
    {
        public string Title { get; set; }
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }

        [Display(Name = "رقم الجوال")]
        public string PhoneNumber { get; set; }

        [Display(Name = "الاسم كاملاً")]
        public string FullName { get; set; }

        [Display(Name = "رقم الهوية / الإقامة")]
        public string NationalID { get; set; }

        [Display(Name = "الجنس")]
        public string Gender { get; set; }

        [Display(Name = "التخصص")]
        public string Major { get; set; }

        [Display(Name = "تاريخ الميلاد")]
        public string DOB { get; set; }
    }

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
    }
}