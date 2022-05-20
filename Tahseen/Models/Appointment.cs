using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Tahseen.Models.Enums;

namespace Tahseen.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "رقم الهوية / إقامة الطفل")]
        public string ChildId { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "نوع التطعيم")]
        public int VaccineId { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "مكان التطعيم")]
        public AppointmentPlace Place { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "موعد التطعيم")]
        public int ClinicAppointmentId { get; set; }
        [ForeignKey("ClinicAppointmentId")]
        public virtual ClinicAppointment ClinicAppointment { get; set; }
        [ForeignKey("ChildId")]
        public virtual Child Child { get; set; }
        [ForeignKey("VaccineId")]
        public virtual Vaccine Vaccine { get; set; }
    }
}