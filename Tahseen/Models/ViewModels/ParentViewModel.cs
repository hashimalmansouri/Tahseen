using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tahseen.Models.ViewModels
{
    public class ContactDoctorViewModel
    {
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "الدكتور")]
        public string DoctorEmail { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "الرسالة")]
        public string Message { get; set; }
    }
}