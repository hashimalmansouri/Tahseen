using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Tahseen.Models.Enums;

namespace Tahseen.Models
{
    public class Approval
    {
        [Key]
        public int ApprovalId { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [RegularExpression(@"^[0-9]{10,10}$", ErrorMessage = "رقم الهوية / الإقامة غير صحيح.")]
        [Display(Name = "رقم الهوية / الإقامة")]
        public string ChildNationalID { get; set; }

        [Required(ErrorMessage = "*هذا الحقل مطلوب")]
        [Display(Name = "نوع التطعيم")]
        public Age VaccineType { get; set; }

        public ApprovalStatus Status { get; set; }

        [ForeignKey("ChildNationalID")]
        public virtual Child Child { get; set; }
    }
}