using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Payrolly.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        #region Personal Columns
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(50)]
        public string? Occupation { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;
        #endregion

        #region Address Columns
        public string? Country { get; set; }

        public string? Address { get; set; }

        public string? StreetAddress { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? ZIPCode { get; set; }
        #endregion

        #region Navigation Properties
        public string? CompanyID { get; set; }
        [ForeignKey("CompanyID")]
        public virtual Company? Company { get; set; }
        #endregion
    }
}