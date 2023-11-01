using Payrolly.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.DTOs.User
{
    public class UserDto
    {
        #region Security Properties
        public string Token { get; set; } = null!;
        public string Id { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
        #endregion

        #region Personal Properties
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public string? Occupation { get; set; }
        public virtual Company? Company { get; set; }

        public string? CompanyId { get; set; }
        #endregion

        #region Address
        public string? Country { get; set; }

        public string? Address { get; set; }

        public string? StreetAddress { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? ZIPCode { get; set; }

        public string? ImageUrl { get; set; }
        #endregion
    }
}
