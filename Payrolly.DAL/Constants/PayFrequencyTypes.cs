using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.DAL.Constants
{
    public enum PayFrequencyTypes
    {
        [Display(Name = "Every Week")]
        EveryWeek,
        [Display(Name = "Every Other Week")]
        EveryOtherWeek,
        [Display(Name = "Twice Month")]
        TwiceAMonth,
        [Display(Name = "Every Month")]
        EveryMonth
    }
}
