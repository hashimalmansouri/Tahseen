using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tahseen.Models.Enums
{
    public enum Gender : int
    {
        [Display(Name = "ذكر")]
        Male,
        [Display(Name = "أنثى")]
        Female
    }
}