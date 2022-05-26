using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tahseen.Models.Enums
{
    public enum AdivceAge : int
    {
        [Display(Name = "حديث الولادة")]
        AtBirth = 1,
        [Display(Name = "عمر أسبوع واحد حتى 6 أشهر")]
        WeekToSixMonths = 2,
        [Display(Name = "عمر 6 أشهر حتى 9 أشهر")]
        SixToNineMonths = 3,
        [Display(Name = "عمر 9 أشهر حتى 12 أشهر")]
        NineMonthsToYear = 4,
        [Display(Name = "عمر 12 شهر حتى سنتين")]
        OneYearToTwoYears = 5,
        [Display(Name = "عمر سنتنين وما فوق")]
        TwoYearsAndMore = 6
    }
}