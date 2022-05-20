using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tahseen.Models.Enums
{
    public enum Age : int
    {
        [Display(Name = "عند الولادة")]
        AtBirth = 1,
        [Display(Name = "شهرين")]
        TwoMonths = 2,
        [Display(Name = "4 شهور")]
        FourMonths = 3,
        [Display(Name = "6 شهور")]
        SixMonths = 4,
        [Display(Name = "9 شهور")]
        NineMonths = 5,
        [Display(Name = "12 شهر")]
        OneYear = 6,
        [Display(Name = "18 شهر")]
        OneYearAndHalf = 7,
        [Display(Name = "24 شهر")]
        TwoYears = 8,
        [Display(Name = "4 - 6 سنوات")]
        FourToSixYears = 9,
    }
}