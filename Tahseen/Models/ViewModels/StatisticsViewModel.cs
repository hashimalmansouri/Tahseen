using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tahseen.Models.ViewModels
{
    public class StatisticsViewModel
    {
        public int AllChildren { get; set; }
        public int AtBirth { get; set; }
        public int TwoMonths { get; set; }
        public int FourMonths { get; set; }
        public int SixMonths { get; set; }
        public int NineMonths { get; set; }
        public int OneYear { get; set; }
        public int OneYearAndHalf { get; set; }
        public int TwoYears { get; set; }
        public int FourToSixYears { get; set; }
    }
}