using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.Employee
{
    public class UpdatePaymentMethodDto
    {
        public string Id { get; set; } = null!;
        public string? PaymentMethod { get; set; }
    }
}
