using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.Employee
{
    public class PagedEmployeeItemDto
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Status { get; set; }
        public string? PayRate { get; set; }
        public string? PayMethod { get; set; }

    }
}
