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
        [Display(Name = "الاسم الأوسط")]
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
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "رقم هوية / إقامة الأب")]
        public string ParentNationalId { get; set; }
        public int ClinicId { get; set; }
        [ForeignKey("ClinicId")]
        public Clinic Clinic { get; set; }
        public virtual ICollection<Immunization> Immunizations { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<ChildDevelopment> ChildDevelopments { get; set; }
        public virtual ICollection<ChildHealth> ChildHealths { get; set; }
        public virtual ICollection<Approval> Approvals { get; set; }

        [Display(Name = "اسم الطفل")]
        public string FullName
        {
            get
            {
                return $"{FName} {MName} {LName}";
            }
        }

        [NotMapped]
        public string Age
        {
            get
            {
                DateTime today = DateTime.Now;
                TimeSpan span = today - DOB;
                DateTime age = DateTime.MinValue + span;

                // Make adjustment due to MinValue equalling 1/1/1
                int years = age.Year - 1;
                int months = age.Month - 1;
                int days = age.Day - 1;

                string result = null;

                if (years == 1)
                {
                    result = "سنة";

                }
                else if (years == 2)
                {
                    result = "سنتان";

                }
                else if (years >= 3)
                {
                    result = years.ToString() + " سنوات";
                }
                else if (years < 1)
                {
                    if (months < 1)
                    {
                        if (days == 1)
                        {
                            result = "يوم";
                        }
                        else if (days == 2)
                        {
                            result = "يومان";
                        }
                        else if (3 <= days && days <= 10)
                        {
                            result = days.ToString() + " أيام";
                        }
                        else
                        {
                            result = days.ToString() + " يوم";
                        }
                    }
                    else if (months == 1)
                    {
                        result = "شهر";
                    }
                    else if (months == 2)
                    {
                        result = "شهرين";
                    }
                    else if (3 <= months && months <= 10)
                    {
                        result = months.ToString() + " أشهر";
                    }
                    else
                    {
                        result = months.ToString() + " شهر";
                    }
                }
                return result;
            }
        }
    }
}