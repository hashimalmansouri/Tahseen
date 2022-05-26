using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tahseen.Models
{
    public class ChildHealth
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [RegularExpression(@"^[0-9]{10,10}$", ErrorMessage = "رقم الهوية/الإقامة غير صحيح.")]
        [Display(Name = "رقم الهوية / الإقامة")]
        public string ChildNationalID { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "طول الطفل")]
        [Range(49, 116, ErrorMessage = "49-116 فضلا ادخل طول بين ")]
        public float Height { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "وزن الطفل")]
        [Range(3.2, 20.7, ErrorMessage = "3-20  فضلا ادخل الوزن بين  ")]
        public float Weight { get; set; }

        [Required(ErrorMessage = "{0} حقل مطلوب.")]
        [Display(Name = "درجه الحرارة")]
        [Range(36, 38, ErrorMessage = "36-40  فضلا ادخل درجه حراره بين  ")]
        public float Temperature { get; set; }

        [Display(Name = "محيط الرأس")]
        [Range(31, 40, ErrorMessage = "32-39  فضلا ادخل محيط الراس بين  ")]
        public float HeadCirumference { get; set; }

        [ForeignKey("ChildNationalID")]
        public virtual Child Child { get; set; }
    }
}