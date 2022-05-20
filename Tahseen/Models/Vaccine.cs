using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Tahseen.Models.Enums;

namespace Tahseen.Models
{
    public class Vaccine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "التطعيم")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "العمر")]
        public Age Age { get; set; }
        public virtual ICollection<Immunization> Immunizations { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}