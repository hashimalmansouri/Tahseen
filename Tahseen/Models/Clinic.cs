using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tahseen.Models
{
    public class Clinic
    {
        [Key]
        public int ClinicId { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "اسم العيادة")]
        public string Name { get; set; }
        public int HSPId { get; set; }
        [ForeignKey("HSPId")]
        public virtual HSP HSP { get; set; }
        public virtual ICollection<ApplicationUser> ClinicUsers { get; set; }
        public virtual ICollection<Child> Children { get; set; }
    }
}