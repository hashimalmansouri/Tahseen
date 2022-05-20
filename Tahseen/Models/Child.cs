using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Tahseen.Models.Enums;

namespace Tahseen.Models
{
    public class Child
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "رقم الهوية / إقامة الطفل")]
        public string ChildID { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "الاسم الأول")]
        public string FName { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "الاسم الاخير")]
        public string LName { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "اسم الأم")]
        public string MName { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "الجنس")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "تاريخ الميلاد")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "العنوان")]
        public string Address { get; set; }
        //[Required(ErrorMessage = "{0} حقل مطلوب.")]
        //[Display(Name = "رقم الجوال الأول")]
        //public string PhoneOne { get; set; }
        //[Display(Name = "رقم الجوال الثاني")]
        //public string PhoneTwo { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "رقم هوية / إقامة الأب")]
        public string ParentNationalId { get; set; }
        [Display(Name = "الطول")]
        public float Height { get; set; }
        [Display(Name = "الوزن")]
        public float Weight { get; set; }
        [Display(Name = "درجة الحرارة")]
        public float Temperature { get; set; }
        [Display(Name = "محيط الرأس")]
        public float HeadCirumference { get; set; }
        //[ForeignKey("ParentId")]
        //public virtual ApplicationUser Parent { get; set; }
        public int ClinicId { get; set; }
        [ForeignKey("ClinicId")]
        public Clinic Clinic { get; set; }
        public virtual ICollection<Immunization> Immunizations { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        [Display(Name = "اسم الطفل")]
        public string FullName
        {
            get
            {
                return $"{FName} {LName}";
            }
        }
    }
}