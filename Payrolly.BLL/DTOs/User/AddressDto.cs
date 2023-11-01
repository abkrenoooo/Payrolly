using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.User
{
    public class AddressDto
    {
        public string? Country { get; set; }

        public string? Address { get; set; }

        public string? StreetAddress { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? ZIPCode { get; set; }

    }
}
