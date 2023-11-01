using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.DAL.Constants
{
    public enum StateTaxType
    {
        [Display(Name = "Static State Tax")]
        StaticStateTax,
        [Display(Name = "Static Local State Tax")]
        StaticLocalStateTax
    }
}
