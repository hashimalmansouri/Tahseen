using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tahseen.Models
{
    public class Immunization
    {
        [Key]
        public int ImmunizationId { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "رقم الهوية / إقامة الطفل")]
        public string NationalID { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "اسم اللقاح")]
        public int VaccineId { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "تاريخ التطعيم")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime VaccinationDate { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "رقم الجرعة")]
        public int DoseNo { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "موعد الجرعة القادمة")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfNextDose { get; set; }
        public string VaccinatorId { get; set; }
        [ForeignKey("VaccinatorId")]
        public virtual ApplicationUser Vaccinator { get; set; }
        [ForeignKey("NationalID")]
        public virtual Child Child { get; set; }
        [ForeignKey("VaccineId")]
        public virtual Vaccine Vaccine { get; set; }
    }
}