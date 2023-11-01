using Payrolly.BLL.DTOs.Employee;
using Payrolly.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.Mapping
{
    public static class EmployeeMapping
    {
        #region FromCreateEmployeeToEmployeeEntity
        public static Employee FromCreateEmployeeToEmployeeEntity(this CreateEmployeeDto dto)
            => new()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleInitial = dto.MiddleInitial,
                Email = dto.Email
            };
        #endregion

        #region ToEmployeeDto
        public static EmployeeDto ToEmployeeDto(this Employee employee)
        {
            return new()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleInitial = employee.MiddleInitial,
                Email = employee.Email,
                Address = employee.Address,
                City = employee.City,
                Country = employee.Country,
                State = employee.State,
                StreetAddress = employee.StreetAddress,
                ZIPCode = employee.ZIPCode,
                Gender = employee.Gender,
                Ssn = employee.Ssn,
                Status = employee.Status,
                PhoneNumber = employee.PhoneNumber,
                DaysPerWeek = employee.DaysPerWeek,
                HireDate = employee.HireDate,
                HoursPerDay = employee.HoursPerDay,
                PaymentMethod = employee.PaymentMethod,
                JobTitle = employee.JobTitle,
                PayType = employee.PayType,
                PayFrequency = employee.PayFrequency,
                Salary = employee.Salary,
                ImageUrl = employee.ImageUrl
            };
        }
        #endregion

        #region ToEmployeeEntity
        public static Employee ToEmployeeEntity(this EmployeeDto employee)
        {
            return new()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleInitial = employee.MiddleInitial,
                Email = employee.Email,
                Address = employee.Address,
                City = employee.City,
                Country = employee.Country,
                State = employee.State,
                StreetAddress = employee.StreetAddress,
                ZIPCode = employee.ZIPCode,
                Gender = employee.Gender,
                Ssn = employee.Ssn,
                Status = employee.Status,
                PhoneNumber = employee.PhoneNumber,
                DaysPerWeek = employee.DaysPerWeek,
                HireDate = employee.HireDate,
                HoursPerDay = employee.HoursPerDay,
                PaymentMethod = employee.PaymentMethod,
                JobTitle = employee.JobTitle,
                PayType = employee.PayType,
                PayFrequency = employee.PayFrequency,
                Salary = employee.Salary,
                ImageUrl = employee.ImageUrl
            };
        }
        #endregion

        #region ToEmploymentDetailsDto
        public static EmploymentDetailsDto ToEmploymentDetailsDto(this Employee employee)
        {
            string? location = null;
            string? payShedule = null;

            if (employee.Location != null)
            {
                location = employee.Location.ToString();
            }

            if (employee.PaySchedule != null)
                payShedule = employee.PaySchedule.Name;


            return new()
            {
                Id = employee.Id, 
                Status = employee.Status,
                HireDate = employee.HireDate,   
                JobTitle = employee.JobTitle,
                Location = location,
                PaySchedule = payShedule
            };
        }
        #endregion
    }
}
