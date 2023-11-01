using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payrolly.BLL.DTOs.Employee;
using Payrolly.BLL.Extensions;
using Payrolly.BLL.Filters;
using Payrolly.BLL.Helpers.Responses;
using Payrolly.BLL.IServices;
using Payrolly.BLL.Mapping;
using Payrolly.DAL.Constants;
using Payrolly.DAL.Entities;
using Payrolly.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        #region Private Properties
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IPayScheduleRepository _payScheduleRepository;
        private readonly IGrossPayRepository _grossPayRepository;
        private readonly ILogger<EmployeeService> _logger;
        #endregion

        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository,
            ILogger<EmployeeService> logger,
            ILocationRepository locationRepository,
            IGrossPayRepository grossPayRepository,
            IPayScheduleRepository payScheduleRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
            _locationRepository = locationRepository;
            _grossPayRepository = grossPayRepository;
            _payScheduleRepository = payScheduleRepository;
        }
        #endregion

        public async Task<GenericResponse<EmployeeDto>> CreateEmployeeAsync(CreateEmployeeDto dto, string companyId)
        {
            try
            {
                // search for default location of the company to assign the employee to this location
                var location = await _locationRepository.FindAsync(l => l.CompanyId == companyId, new string[] { "Company" });

                if (location == null) return new() { Message = "invalid company" };

                // check if email is exist or not inside the same company
                var employee = await _employeeRepository.FindAsync(e => location.CompanyId == companyId && e.Email == dto.Email); // TODO: search inside the company

                if (employee != null)
                    return new() { Message = "the employee is exist already" };

                // prepare employee entity to create 
                employee = dto.FromCreateEmployeeToEmployeeEntity();

                // assign employee for default location
                employee.Location = location;

                if (!await _employeeRepository.CreateAsync(employee))
                    return new() { Message = "cannot create" };

                // return response
                var employeeDto = employee.ToEmployeeDto();

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "created",
                    Data = employeeDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new() { Message = ex.Message };
            }
        }

        public async Task<PagedResponse<IEnumerable<PagedEmployeeItemDto>>> GetPaginatedEmployees(PaginationEmployeeFilter pagination, string companyId)
        {
            // get all employees
            var query = await _employeeRepository.FindAllAsync(e => e.Location.CompanyId == companyId, new string[] { "Location" });

            // if the user seareched for name
            if (!String.IsNullOrEmpty(pagination.Name))
            {
                var names = pagination.Name.Split(' ');
                if (names.Length > 1)
                {
                    // Filter by both first name and last name
                    string firstName = names[0];
                    string lastName = names[1];
                    query = query.Where(e => e.FirstName.Contains(firstName) && e.LastName.Contains(lastName));
                }
                else
                {
                    // Filter by either first name or last name
                    string name = names[0];
                    query = query.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name));
                }
            }

            // filter by status
            if (!string.IsNullOrEmpty(pagination.Status))
            {
                query = query.Where(e => e.Status == pagination.Status);
            }

            // calcuate all record inside this criteria of filters
            var allRecords = await query.CountAsync();

            // paged the data
            var paginatedQuery = query.Page(pagination.PageNumber, pagination.PageSize);

            // perpare and return response
            var employeesItemsDto = new List<PagedEmployeeItemDto>();

            // add all employees items that apear in the list
            foreach (var e in paginatedQuery)
            {
                // determine the format of pay rate for the employee
                string payRate = null;

                if (e.Salary == 0)
                {
                    payRate = "Missing";
                }
                else
                {
                    if (e.PayType == PayTypes.Hourly)
                    {
                        payRate += e.Salary.ToString() + "/Hour";
                    }
                    else if (e.PayType == PayTypes.Salary)
                    {
                        if (e.PayFrequency == PayFrequency.PerWeek)
                        {
                            payRate += (e.Salary / ((decimal)e.DaysPerWeek * (decimal)e.HoursPerDay)).ToString() + "/Hour";
                        }
                        else if (e.PayFrequency == PayFrequency.PerMonth)
                        {
                            payRate += (e.Salary / ((decimal)4*e.DaysPerWeek * (decimal)e.HoursPerDay)).ToString() + "/Hour";
                        }
                        else if (e.PayFrequency == PayFrequency.PerYear)
                        {
                            payRate += (e.Salary / ((decimal)48*e.DaysPerWeek * (decimal)e.HoursPerDay)).ToString() + "/Hour";
                        }
                    }
                }

                // add employee item
                employeesItemsDto.Add(
                    new() { Id = e.Id, Name = e.FirstName + " " + e.LastName, PayRate = payRate, PayMethod = e.PaymentMethod, Status = e.Status });
            }

            return new()
            {
                Success = true,
                StatusCode = 200,
                Message = "Success",
                TotalRecords = allRecords,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                Data = employeesItemsDto
            };
        }

        #region DeleteEmployeeAsync
        public async Task<GenericResponse<object>> DeleteEmployeeAsync(string id)
        {
            try
            {
                // check if employee is exist or not
                var employee = await _employeeRepository.FindAsync(e => e.Id == id);

                //can not Delete employee

                if (employee == null)
                    return new() { Message = "cannot Delete" };

                // Delete employee
                if (!await _employeeRepository.RemoveAsync(employee))
                    return new() { Message = "Delete" };

                // return response
                var employeeDto = employee.ToEmployeeDto();
                return new()
                {
                    Success = true,
                    StatusCode = 201,
                    Message = "Deleted",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                // return response
                return new()
                {
                    Message = "Data Not Exist"
                };
            }

        }
        #endregion

        #region GetEmployeeAsync
        public async Task<GenericResponse<EmployeeDto>> GetEmployeeAsync(string id)
        {
            try
            {
                // Get By Employee With id
                var employee = await _employeeRepository.GetByIDAsync(id);

                if (employee == null)
                {
                    return new() { Message = "the employee is not exist" };
                }

                // return response
                var employeeDto = employee.ToEmployeeDto();

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Data Found",
                    Data = employeeDto

                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                // return response
                return new()
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Not Found"

                };
            }
        }
        #endregion

        #region UpdatePaymentMethod
        public async Task<GenericResponse<EmployeeDto>> UpdatePaymentMethodAsync(UpdatePaymentMethodDto dto)
        {
            try
            {
                var employee = await _employeeRepository.GetByIDAsync(dto.Id);

                if (employee == null)
                {
                    // Handle case where employee is not found
                    return new() { Message = "employee not found" };
                }
                employee.PaymentMethod = dto.PaymentMethod;

                if (!await _employeeRepository.UpdateAsync(employee))
                {
                    return new() { Message = "error in updating employee" };
                }
                // return response
                var employeeDto = employee.ToEmployeeDto();

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "updated",
                    Data = employeeDto
                };
            }
            catch (Exception ex)
            {

                // Handle and log the exception
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new() { Message = ex.Message };
            }
            DateTime date;

            var v = DateTime.Now.Month;


        }

        #endregion

        #region UpdatePayTypeAsync
        public async Task<GenericResponse<EmployeeDto>> UpdatePayTypeAsync(UpdatePayTypeDto dto)
        {
            try
            {
                var employee = await _employeeRepository.FindAsync(e => e.Id == dto.Id, new string[] { "GrossPay" });
                if (employee == null)
                {
                    // Handle case where employee is not found
                    return new() { Message = "employee not found" };
                }
                employee.PayType = dto.PayType;
                employee.PayFrequency = dto.PayFrequency;
                employee.Salary = dto.Salary;
                employee.HoursPerDay = dto.HoursPerDay;
                employee.DaysPerWeek = dto.DaysPerWeek;
                //if (employee.GrossPay is null)
                //{
                    GrossPay grossPay = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        PayRate = dto.PayType == PayTypes.Hourly ? dto.Salary : dto.PayFrequency == PayFrequency.PerWeek
                                ? (dto.Salary / ((decimal)dto.DaysPerWeek * (decimal)dto.HoursPerDay)) : dto.PayFrequency == PayFrequency.PerMonth ?
                                (dto.Salary / ((decimal)4 * dto.DaysPerWeek * (decimal)dto.HoursPerDay)) :
                                (dto.Salary / ((decimal)48 * dto.DaysPerWeek * (decimal)dto.HoursPerDay)),
                        RegularPay = dto.HoursPerDay * dto.DaysPerWeek,
                        EmployeeId=employee.Id,

                        //Gross = (decimal)dto.HoursPerDay * dto.DaysPerWeek * dto.Salary,
                        //EmployeeId = employee.Id,
                        //Employee = employee
                    };
                    //employee.GrossPay = grossPay;
                //}
                //else
                //{
                //    employee.GrossPay.RegularPay = dto.HoursPerDay * dto.DaysPerWeek;
                //    employee.GrossPay.PayRate = dto.PayType == PayTypes.Hourly ? dto.Salary : dto.PayFrequency == PayFrequency.PerWeek
                //                ? (dto.Salary / ((decimal)dto.DaysPerWeek * (decimal)dto.HoursPerDay)) : dto.PayFrequency == PayFrequency.PerMonth ?
                //                (dto.Salary / ((decimal)4 * dto.DaysPerWeek * (decimal)dto.HoursPerDay)) :
                //                (dto.Salary / ((decimal)48 * dto.DaysPerWeek * (decimal)dto.HoursPerDay));
                //    //employee.GrossPay.Gross = (decimal)dto.HoursPerDay * dto.DaysPerWeek * dto.Salary;
                //    await _grossPayRepository.UpdateAsync(employee.GrossPay);
                //}

                await _employeeRepository.UpdateAsync(employee);
                // return response
                var employeeDto = employee.ToEmployeeDto();
                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "updated",
                    Data = employeeDto
                };
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new() { Message = ex.Message };
            }
            throw new NotImplementedException();
        }
        #endregion

        public async Task<GenericResponse<EmployeeDto>> UpdatePersonalInfoAsync(UpdatePersonalInfoDto dto)
        {
            try
            {
                // check if Employee is exist or not
                var employee = await _employeeRepository.FindAsync(e => e.Id == dto.Id);

                // employee not exist
                if (employee == null)
                {
                    // Handle case where employee is not found
                    return new() { Message = "employee not found" };
                }


                // update employee
                employee.FirstName = dto.FirstName;
                employee.LastName = dto.LastName;
                employee.MiddleInitial = dto.MiddleInitial;
                employee.Email = dto.Email;
                employee.BirthDay = dto.BirthDate;
                employee.Country = dto.Country;
                employee.State = dto.State;
                employee.City = dto.City;
                employee.StreetAddress = dto.StreetAddress;
                employee.Address = dto.Address;
                employee.ZIPCode = dto.ZIPCode;
                employee.Gender = dto.Gender;
                employee.Ssn = dto.Ssn;
                employee.PhoneNumber = dto.PhoneNumber;

                if (!await _employeeRepository.UpdateAsync(employee))
                {
                    return new() { Message = "cannot update employee" };
                }
                // return response

                var employeeDto = employee.ToEmployeeDto();
                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "updated",
                    Data = employeeDto
                };
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new() { Message = ex.Message };
            }

        }

        public async Task<GenericResponse<EmploymentDetailsDto>> GetEmploymentDetailsAsync(string id)
        {
            try
            {
                // check if employee is exist or not
                var employee = await _employeeRepository.FindAsync(e => e.Id == id, new string[] { "Location", "PaySchedule" });

                //can not Details employee
                if (employee == null)
                    return new() { Message = "not exist  EmploymentDetails" };

                // return response
                var employmentDetailsDto = employee.ToEmploymentDetailsDto();

                return new()
                {
                    Success = true,
                    StatusCode = 201,
                    Message = "EmploymentDetails",
                    Data = employmentDetailsDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                // return response
                return new()
                {
                    Message = "Data Not Exist"
                };
            }
        }

        public async Task<GenericResponse<EmploymentDetailsDto>> UpdateEmploymentDetailsAsync(EmploymentDetailsDto dto)
        {
            try
            {
                var employee = await _employeeRepository.FindAsync(e => e.Id == dto.Id, new string[] { "Location","PaySchedule" });

                if (employee == null)
                {
                    // Handle case where employee is not found
                    return new() { Message = "employee not found" };
                }

                var sepeartedLocation = dto.Location.Split('-'); // Location Format: State-City-ZIPCode

                var paySchedule = await _payScheduleRepository.FindAsync(p => p.Name == dto.PaySchedule);
                var grosspay = new GrossPay()
                {
                    Id = Guid.NewGuid().ToString(),
                    GrossPayDate =paySchedule.NextPayPeriod,
                    PayScheduleId=paySchedule.PayScheduleId,
                    PayRate = employee.PayType == PayTypes.Hourly ? employee.Salary : employee.PayFrequency == PayFrequency.PerWeek
                                ? (employee.Salary / ((decimal)employee.DaysPerWeek * (decimal)employee.HoursPerDay)) : employee.PayFrequency == PayFrequency.PerMonth ?
                                (employee.Salary / ((decimal)4 * employee.DaysPerWeek * (decimal)employee.HoursPerDay)) :
                                (employee.Salary / ((decimal)48 * employee.DaysPerWeek * (decimal)employee.HoursPerDay)),
                   EmployeeId= employee.Id,
                   RegularPay=employee.PayFrequency == PayFrequency.PerWeek ?employee.DaysPerWeek*employee.HoursPerDay:
                               employee.PayFrequency == PayFrequency.PerMonth ? employee.DaysPerWeek * employee.HoursPerDay*(double)4 :
                                employee.DaysPerWeek * employee.HoursPerDay * (double)4* (double)12,
                   CompanyId=employee.Location.CompanyId,
                   

                };
                if (!await _grossPayRepository.CreateAsync(grosspay))
                {
                    return new() { Message = "error in creating grosspay" };
                }
                var location = await _locationRepository.FindAsync(l => l.State == sepeartedLocation[0]
                                                                   && l.City == sepeartedLocation[1]
                                                                   && l.ZIPCode == sepeartedLocation[2]);

                if (paySchedule == null || location == null)
                    return new() { Message = "there is no exist pay shedule or location for employee" };

                employee.Status = dto.Status;
                employee.HireDate = dto.HireDate;
                employee.JobTitle = dto.JobTitle;
                employee.PaySchedule = paySchedule;
                employee.Location = location;

                if (!await _employeeRepository.UpdateAsync(employee))
                {
                    return new() { Message = "error in updating EmploymentDetails" };
                }
                // return response
                var employmentDetailsDto = employee.ToEmploymentDetailsDto();

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "updated",
                    Data = employmentDetailsDto
                };
            }
            catch (Exception ex)
            {

                // Handle and log the exception
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new() { Message = ex.Message };
            }
        }
    }
}
