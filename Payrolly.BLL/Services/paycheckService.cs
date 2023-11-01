using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payrolly.BLL.DTOs.Deduction;
using Payrolly.BLL.DTOs.Paycheck;
using Payrolly.BLL.Extensions;
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
    public class PayCheckService : IPayCheckService
    {
        #region Private Properties
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IGrossPayRepository _grossPayRepository;
        private readonly ILogger<PayCheckService> _logger;
        #endregion

        #region Constructor
        public PayCheckService(
            IEmployeeRepository employeeRepository,
            IGrossPayRepository grossPayRepository,
            ILogger<PayCheckService> logger)
        {
            _employeeRepository = employeeRepository;
            _grossPayRepository = grossPayRepository;
            _logger = logger;
        }
        #endregion

        public async Task<GenericResponse<IEnumerable<GrossPayListItemDto>>> GetGrossPayItemsAsync(string companyId)
        {
            try
            {
                // get employees belongs to the company
                var grosspays = await _grossPayRepository.FindAllAsync(e => e.CompanyId == companyId, new string[] { "Employee" }).Result.DistinctBy(x => x.EmployeeId).ToListAsync();
                var grossPaysDto = new List<GrossPayListItemDto>();

                if (grosspays != null)
                {
                    if (grosspays.Count() != 0)
                    {
                        // calculate gross pay for each employee
                        foreach (var e in grosspays)
                        {
                            if (e == null || e.Employee == null)
                            {
                                continue;
                            }
                            // get all gross pay items for each employee
                            grossPaysDto.Add(new()
                            {
                                EmployeeId = e.Id,
                                GrossPayId = e.Id,
                                PayScheduleId = e.PayScheduleId,
                                EmployeeName = string.Join(" ", e.Employee.FirstName, e.Employee.LastName),
                                OverTime = e.OverTime,
                                Bonus = e.Bonus,
                                HolidayPay = e.HolidayPay,
                                Commission = e.Commision,
                                RegularPay = e.RegularPay,
                                TotalHours = e.RegularPay + e.OverTime + e.HolidayPay,
                                PayRate = e.Employee.PayType == PayTypes.Hourly ? e.Employee.Salary : e.Employee.PayFrequency == PayFrequency.PerWeek
                                ? (e.Employee.Salary / ((decimal)e.Employee.DaysPerWeek * (decimal)e.Employee.HoursPerDay)) : e.Employee.PayFrequency == PayFrequency.PerMonth ?
                                (e.Employee.Salary / ((decimal)4 * e.Employee.DaysPerWeek * (decimal)e.Employee.HoursPerDay)) :
                                (e.Employee.Salary / ((decimal)48 * e.Employee.DaysPerWeek * (decimal)e.Employee.HoursPerDay)),
                                EmpTax = e.EmpTax,
                                NetPay = e.PayRate - e.EmpTax,
                                Payment = e.Payment,

                            }); ;
                        }
                    }
                }


                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Data Found",
                    Data = grossPaysDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new() { Message = ex.Message };
            }
        }

        public async Task<GenericResponse<List<CalculateGrossPayDto>>> CalculateGrossPayForEmployeeAsync(string companyId, List<CalculateGrossPayDto> dto)
        {
            try
            {
                #region Old Code
                // get the employee
                //var employee = await _employeeRepository
                //    .FindAsync(e => e.Location.CompanyId == companyId && e.Id == employeeId, new string[] { "Location", "GrossPay" });

                //// check if that employee is in the company
                //if (employee is null)
                //    return new() { Message = "Invalid employee id" };

                //// prepare variables to calculate gross pay
                //decimal regularSalary = (decimal)dto.RegularPay * employee.Salary;
                //decimal overTimeSalary = employee.Salary * (decimal)dto.OverTime * 1.5m;
                //decimal holidaySalary = (decimal)dto.HolidayPay * employee.Salary;

                //// calcuate gross pay for the employee
                //employee.GrossPay.RegularPay = dto.RegularPay;
                //employee.GrossPay.OverTime = dto.OverTime;
                //employee.GrossPay.HolidayPay = dto.HolidayPay;
                //employee.GrossPay.Bonus = dto.Bonus;
                //employee.GrossPay.Commision = dto.Commission;
                //employee.GrossPay.EmpTax = dto.EmpTax;
                ////employee.GrossPay.TotalHours = dto.RegularPay + dto.OverTime + dto.HolidayPay;
                ////employee.GrossPay.Gross = regularSalary + overTimeSalary + holidaySalary + dto.Commission + dto.Bonus;

                //// update the gross pay
                //foreach (var item in dto)
                //{
                //    await _grossPayRepository.UpdateAsync(item);
                //}
                //// prepare gross pay items to return
                //var grossPayDto = new GrossPayListItemDto
                //{
                //    RegularPay = employee.GrossPay.RegularPay,
                //    OverTime = employee.GrossPay.OverTime,
                //    HolidayPay = employee.GrossPay.HolidayPay,
                //    Bonus = employee.GrossPay.Bonus,
                //    Commission = employee.GrossPay.Commision,
                //    TotalHours = dto.RegularPay + dto.OverTime + dto.HolidayPay,
                //    EmpTax = employee.GrossPay.EmpTax,


                //};
                //if (employee.PayType == PayType.Hourly)
                //{
                //    grossPayDto.PayRate = employee.Salary;

                //}
                //else if (employee.PayType == PayType.Salary)
                //{
                //    if (employee.PayFrequency == PayFrequency.PerWeek)
                //    {
                //        grossPayDto.PayRate = (employee.Salary / ((decimal)employee.DaysPerWeek * (decimal)employee.HoursPerDay));
                //    }
                //    else if (employee.PayFrequency == PayFrequency.PerMonth)
                //    {
                //        grossPayDto.PayRate = (employee.Salary / ((decimal)4 * employee.DaysPerWeek * (decimal)employee.HoursPerDay));
                //    }
                //    else if (employee.PayFrequency == PayFrequency.PerYear)
                //    {
                //        grossPayDto.PayRate = (employee.Salary / ((decimal)48 * employee.DaysPerWeek * (decimal)employee.HoursPerDay));
                //    }
                //}
                //grossPayDto.NetPay = grossPayDto.PayRate - grossPayDto.EmpTax;

                // return gross pay items
                #endregion

                var groospay = new List<GrossPay>();
                foreach (var item in dto)
                {
                    item.Payment = true;
                    //groospay.Add(item.ToGrossPay(companyId));
                    var grosspay = await _grossPayRepository.FindAsync(x => x.EmployeeId == item.EmployeeId && x.PayScheduleId == item.PayScheduleId && x.CompanyId == companyId && x.GrossPayDate.Date == item.GrossPayDate.Date);
                    var count = groospay.Count();
                    if (grosspay == null)
                    {
                        var newItem = item.ToGrossPay(companyId);
                        if (!await _grossPayRepository.CreateAsync(newItem))
                        {
                            return new()
                            {
                                Success = true,
                                StatusCode = 200,
                                Message = "Data Not Found",
                                Data = null
                            };
                        }
                    }
                    else
                    {
                        var newItem = item.ToGrossPayUpdate(companyId);
                        grosspay.Bonus = item.Bonus;
                        grosspay.RegularPay = item.RegularPay;
                        grosspay.HolidayPay = item.HolidayPay;
                        grosspay.CheckNum = item.CheckNum;
                        grosspay.Commision = item.Commission;
                        grosspay.EmpTax = item.EmpTax;
                        grosspay.OverTime = item.OverTime;
                        grosspay.PayRate = item.PayRate;
                        grosspay.Payment = true;
                        if (!await _grossPayRepository.UpdateAsync(grosspay))
                        {
                            return new()
                            {
                                Success = true,
                                StatusCode = 200,
                                Message = "Data Not Found",
                                Data = null
                            };
                        }
                    }
                }

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Data Found",
                    Data = dto
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new()
                {
                    Message = ex.Message
                };
            }
        }

        public async Task<GenericResponse<List<GrossPayListItemDto>>> GetTotalRecordForAllPayChecks(string companyId, string PayScheduleId, DateTime NextPayDay)
        {
            try
            {
                // get all gross pays inside the company
                var grossPays = await _grossPayRepository.GetAllGrossPaysInsideCompany(companyId, PayScheduleId, NextPayDay);
                //var sss = grossPays.ToList();
                // check if the company not already have gross pays (not end the employees info)
                if (grossPays is null)
                    return new() { Message = "there is no gross pays inside the company: review the employees settings and finsish it" };
                var count = grossPays == null ? 0 : grossPays.Count();
                // initialize the total record to return
                var totalRecordDto = new List<GrossPayListItemDto>();
                var totalRecordDtofack = new GrossPayListItemDto();
                // calculate items
                for (int i = 0; i < count; i++)
                {

                    if (grossPays[i].GrossPay == null || grossPays[i].GrossPay.Count() == 0)
                    {
                        continue;
                    }
                    else
                    {
                        totalRecordDto.Add(new()
                        {
                            PayScheduleId = grossPays[i].PayScheduleId,
                            EmployeeId = grossPays[i].Id,
                            EmployeeName = string.Join(" ", grossPays[i].FirstName, grossPays[i].LastName),
                            RegularPay = grossPays[i].GrossPay.LastOrDefault().RegularPay,
                            GrossPayId = grossPays[i].GrossPay.LastOrDefault().Id,
                            OverTime = grossPays[i].GrossPay.LastOrDefault().OverTime,
                            TotalHours = grossPays[i].GrossPay.LastOrDefault().OverTime + grossPays[i].GrossPay.LastOrDefault().RegularPay + grossPays[i].GrossPay.LastOrDefault().HolidayPay,
                            EmpTax = grossPays[i].GrossPay.LastOrDefault().EmpTax,
                            HolidayPay = grossPays[i].GrossPay.LastOrDefault().HolidayPay,
                            Bonus = grossPays[i].GrossPay.LastOrDefault().Bonus,
                            Commission = grossPays[i].GrossPay.LastOrDefault().Commision,
                            PayRate = grossPays[i].GrossPay.LastOrDefault().PayRate,
                            NetPay = grossPays[i].GrossPay.LastOrDefault().PayRate - grossPays[i].GrossPay.LastOrDefault().EmpTax,
                            Payment = grossPays[i].GrossPay.LastOrDefault().Payment,

                        });
                    }
                    //totalRecordDto.TotalHours +=  grossPays[i].TotalHours;
                    //totalRecordDto.GrossPay += g.Gross;
                }

                // return the items
                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Data Found",
                    Data = totalRecordDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new()
                {
                    Message = ex.Message
                };
            }
        }

        public async Task<PagedResponse<IEnumerable<CalculateGrossPayDto>>> GetPaginatedGrossPayAsync(PayCheckFilter pagination, string CompanyId)
        {
            try
            {
                var gros = await _grossPayRepository.FindAllAsync(e => e.CompanyId == CompanyId && e.PayScheduleId == pagination.PayScheduleId && e.GrossPayDate.Date == pagination.GrossPayDate.Date, new string[] { "Employee", "PaySchedule" });
                var countGros = await gros.CountAsync();
                if (gros == null || countGros == 0)
                {
                    var query = await _employeeRepository.FindAllAsync(e => e.Location.CompanyId == CompanyId && e.PayScheduleId == pagination.PayScheduleId, new string[] { "Location", "PaySchedule" });
                    var grosspays = new List<GrossPay>();
                    // calcuate all record inside this criteria of filters
                    var allRecords = await query.CountAsync();
                    foreach (var item in query.ToList())
                    {
                        var grosspay = new GrossPay()
                        {
                            Id = Guid.NewGuid().ToString(),
                            CompanyId = CompanyId,
                            EmployeeId = item.Id,
                            PayScheduleId = pagination.PayScheduleId,
                            GrossPayDate = pagination.GrossPayDate.Date,
                            RegularPay = item.PayFrequency == PayFrequency.PerWeek ? item.DaysPerWeek * item.HoursPerDay :
                                   item.PayFrequency == PayFrequency.PerMonth ? item.DaysPerWeek * item.HoursPerDay * (double)4 :
                                    item.DaysPerWeek * item.HoursPerDay * (double)4 * (double)12,
                            PayRate = item.PayType == PayTypes.Hourly ? item.Salary : item.PayFrequency == PayFrequency.PerWeek
                                    ? (item.Salary / ((decimal)item.DaysPerWeek * (decimal)item.HoursPerDay)) : item.PayFrequency == PayFrequency.PerMonth ?
                                    (item.Salary / ((decimal)4 * item.DaysPerWeek * (decimal)item.HoursPerDay)) :
                                    (item.Salary / ((decimal)48 * item.DaysPerWeek * (decimal)item.HoursPerDay)),
                            Payment = false,
                        };
                        if (!await _grossPayRepository.CreateAsync(grosspay))
                        {
                            return new()
                            {
                                Success = true,
                                StatusCode = 200,
                                Message = "Data Not Found",
                                Data = null
                            };
                        }
                        grosspays.Add(grosspay);
                    }
                    // paged the data
                    var paginatedQuery = grosspays.AsQueryable().Page(pagination.PageNumber, pagination.PageSize);

                    // perpare and return response
                    var GrosspayItemsDto = new List<CalculateGrossPayDto>();

                    // add all employees items that apear in the list
                    foreach (var e in paginatedQuery)
                    {
                        GrosspayItemsDto.Add(e.CalculateGrossPay());
                    }

                    return new()
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "Success",
                        TotalRecords = allRecords,
                        PageNumber = pagination.PageNumber,
                        PageSize = pagination.PageSize,
                        Data = GrosspayItemsDto
                    };
                }
                else
                {
                    
                    // paged the data
                    var paginatedQuery = gros.Page(pagination.PageNumber, pagination.PageSize);

                    // perpare and return response
                    var GrosspayItemsDto = new List<CalculateGrossPayDto>();

                    // add all employees items that apear in the list
                    foreach (var e in paginatedQuery)
                    {
                        GrosspayItemsDto.Add(e.CalculateGrossPay());
                    }

                    return new()
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "Success",
                        TotalRecords = countGros,
                        PageNumber = pagination.PageNumber,
                        PageSize = pagination.PageSize,
                        Data = GrosspayItemsDto
                    };
                }

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new() { Message = ex.Message };
            }
        }


        //public async Task<GenericResponse<GetGrossPayDto>> CalculateGrossPay(string companyId, string employeeId, CalculateGrossPayDto dto)
        //{
        //    try
        //    {
        //        // check if employee exist for the company or not
        //        var employee = _employeeRepository.
        //            FindAsync(e => e.Location.CompanyId == companyId && e.Id == employeeId, new string[] {"Location"});

        //        if (employee is null)
        //            return new() { Message = "Invalid id" };



        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        _logger.LogError(ex.StackTrace);
        //        return new() { Message = ex.Message };
        //    }
        //}

        //private async Task<decimal?> GetNetEmployeeDeductionsAsync(string employeeId)
        //{
        //    try
        //    {
        //        // search for employee
        //        var employee = await _employeeRepository.FindAsync(e => e.Id == employeeId);

        //        // check if employee exist or not
        //        if (employee == null)
        //        {
        //            _logger.LogError("employee not exist to calculate its deductions!");
        //            return null; // flag for failed function
        //        }

        //        // get deductions for employee
        //        var employeeDeductions = employee.Deductions.ToList();

        //        if (employeeDeductions.Count == 0)
        //        {
        //            _logger.LogError("the employee not has deductions");
        //            return null;
        //        }

        //        decimal? total = 0m;

        //        foreach (var d in employeeDeductions)
        //            total += d.Amount;

        //        return total;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        _logger.LogError(ex.StackTrace);
        //        return null;
        //    }
        //}
    }
}
