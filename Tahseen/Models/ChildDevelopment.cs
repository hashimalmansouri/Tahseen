using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Tahseen.Models.Enums;

namespace Tahseen.Models
{
    public class ChildDevelopment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [RegularExpression(@"^[0-9]{10,10}$", ErrorMessage = "رقم الهوية/الإقامة غير صحيح.")]
        [Display(Name = "رقم الهوية / الإقامة")]
        public string ChildNationalID { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "عمر الطفل")]
        public AdivceAge AdivceAge { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "توصيه اللعب من أجل تطوّر الطفل ")]
        public string AdviceGame { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "توصيه التواصل من أجل تطوّر الطفل")]
        public string AdviceTalk { get; set; }

        [ForeignKey("ChildNationalID")]
        public virtual Child Child { get; set; }
    }
}