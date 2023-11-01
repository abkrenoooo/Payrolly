using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.DAL.Constants
{
    public enum FilingStatusState
    {
        [Display(Name = "Single Or Married(with two or more income)")]
        SingleOrMarriedMoreOne,
        [Display(Name = "Married(one income)")]
        MarriedOne,
        [Display(Name = "Heed Of Household")]
        HeedOfHousehold,
        [Display(Name = "Do Not Withhold")]
        DoNotWithhold,
    }
}
