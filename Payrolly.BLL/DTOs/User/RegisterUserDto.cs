using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.User
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "Not matched password")]
        public string ConfirmedPassword { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        #region Company Location
        public string Country { get; set; } = null!;

        public string? Address { get; set; }

        public string? StreetAddress { get; set; }

        public string City { get; set; } = null!;

        public string State { get; set; } = null!;

        public string? ZIPCode { get; set; }
        #endregion

    }
}
