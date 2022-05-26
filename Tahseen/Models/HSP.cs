using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tahseen.Models
{
    public class HSP
    {
        [Key]
        public int HSPId { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "اسم المركز الصحي")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "العنوان")]
        public string Address { get; set; }
        public virtual ICollection<ApplicationUser> HSPUsers { get; set; }
        public virtual ICollection<Clinic> Clinics { get; set; }
    }
}