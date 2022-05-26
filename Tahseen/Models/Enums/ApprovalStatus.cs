using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tahseen.Models.Enums
{
    public enum ApprovalStatus : int
    {
        [Display(Name = "قبول")]
        Accept = 1,
        [Display(Name = "رفض")]
        Deny = 2,
        [Display(Name = "تم تطعيمه")]
        Immunized = 3
    }
}