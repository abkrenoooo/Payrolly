using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.User
{
    public class UpdateNameDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
