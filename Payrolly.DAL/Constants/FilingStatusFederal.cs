using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.DAL.Constants
{
    public enum FilingStatusFederal
    {
        [Display(Name = "Single Or Married Filing Separately")]
        SingleOrMarriedFilingSeparately,
        [Display(Name = "Married Filing Jointly (or Qualifying Widow(er))")]
        MarriedFilingJointly,
        [Display(Name = "Heed Of Household")]
        HeedOfHousehold,
        [Display(Name = "Exempt")]
        Exempt,
    }
}
