using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tahseen.Models.Enums
{
    public enum AppointmentPlace : int
    {
        [Display(Name = "في المنزل")]
        Home = 0,
        [Display(Name = "في العيادة")]
        Clinic = 1
    }
}