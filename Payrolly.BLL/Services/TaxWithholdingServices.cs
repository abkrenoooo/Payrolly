using Microsoft.Extensions.Logging;
using Payrolly.BLL.DTOs.TaxWithholding;
using Payrolly.BLL.Helpers.Responses;
using Payrolly.BLL.IServices;
using Payrolly.BLL.Mapping;
using Payrolly.DAL.Entities;
using Payrolly.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.Services
{
    public class TaxWithholdingServices : ITaxWithholdingServices
    {
        #region Private Properties
        private readonly ITaxWithholdingRepository _taxWithholdingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<TaxWithholding> _logger;
        #endregion
        public TaxWithholdingServices(ITaxWithholdingRepository taxWithholdingRepository,
           ILogger<TaxWithholding> logger,
           ILocationRepository locationRepository,
           IEmployeeRepository employeeRepository)
        {
            _taxWithholdingRepository = taxWithholdingRepository;
            _logger = logger;
            _locationRepository = locationRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<GenericResponse<TaxWithholdingDto>> CreateTaxWithholdingAsync(TaxWithholdingDto dto, string companyId)
        {
            try
            {
                //var location = await _locationRepository.FindAsync(l => l.CompanyId == companyId, new string[] { "Company" });
               
                //if (location == null) return new() { Message = "invalid Tax" };
                var employee = await _employeeRepository.GetByIDAsync(dto.EmployeeId); 
                
                if (employee == null)
                {
                    // Handle case where employee is not found
                    return new() { Message = "employee not found" };
                }
                dto.FederalWithholdingId =employee.TaxWithholdingId;
                TaxWithholding tax = new TaxWithholding();
                if (employee.TaxWithholdingId == null)
                {
                    tax = dto.CreateNewTaxWithholding();
                    employee.TaxWithholdingId = tax.FederalWithholdingId;
                    if (!await _taxWithholdingRepository.CreateAsync(tax))
                        return new() { Message = "cannot create" };
                }
                else
                {
                     tax = dto.ToTaxWithholding();

                    //tax.FilingStatusFederal = dto.FilingStatusFederal;
                    //tax.FederalCheck = dto.FederalCheck;
                    //tax.ClaimedDependent = dto.ClaimedDependent;
                    //tax.OtherIncome = dto.OtherIncome;
                    //tax.Deducations = dto.Deducations;
                    //tax.ExtraWithholding = dto.ExtraWithholding;
                    //tax.FilingStatusState = dto.FilingStatusState;
                    //tax.WithholdingAllowance = dto.WithholdingAllowance;
                    //tax.AdditionAmount = dto.AdditionAmount;
                    //tax.FUTA = dto.FUTA;
                    //tax.SocialSecurity = dto.SocialSecurity;
                    //tax.CASUIAndETT = dto.CASUIAndETT;
                    //tax.CASDI = dto.CASDI;
                    //tax.EmployeeId = dto.EmployeeId;

                    if (!await _taxWithholdingRepository.UpdateAsync(tax))
                    {
                        return new() { Message = "error in updating Tax" };
                    }
                }
                // return response
                var taxWithholdingDto = tax.ToTaxWithholdingDto();
                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "created",
                    Data = taxWithholdingDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new() { Message = ex.Message };
            }
            // search for default location of the company to assign the employee to this location

        }

        public async Task<GenericResponse<TaxWithholdingDto>> GetTaxWithholdingAsync(string id)
        {
            try
            {
                // Get By Employee With id
                var tax = await _taxWithholdingRepository.GetByIDAsync(id);

                if (tax == null)
                {
                    return new() { Message = "the employee is not exist" };
                }
                // return response
                var taxWithholdingDto = tax.ToTaxWithholdingDto();
                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "created",
                    Data = taxWithholdingDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new()
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Not Found"

                };
            }
        }

        public async Task<GenericResponse<TaxWithholding>> UpdateTaxWithholdingAsync(TaxWithholdingDto dto)
        {
            try
            {
                var employee = await _employeeRepository.GetByIDAsync(dto.EmployeeId);

                if (employee == null)
                {
                    // Handle case where employee is not found
                    return new() { Message = "employee not found" };
                }
                var tax = employee.TaxWithholding;
                if (employee.TaxWithholding == null)
                {
                    // Handle case where employee is not found
                    return new() { Message = "employee not have Tax Withholding" };
                }
                tax.FilingStatusFederal = dto.FilingStatusFederal;
                tax.FederalCheck = dto.FederalCheck;
                tax.ClaimedDependent = dto.ClaimedDependent;
                tax.OtherIncome = dto.OtherIncome;
                tax.Deducations = dto.Deducations;
                tax.ExtraWithholding = dto.ExtraWithholding;
                tax.FilingStatusState = dto.FilingStatusState;
                tax.WithholdingAllowance = dto.WithholdingAllowance;
                tax.AdditionAmount = dto.AdditionAmount;
                tax.FUTA = dto.FUTA;
                tax.SocialSecurity = dto.SocialSecurity;
                tax.CASUIAndETT = dto.CASUIAndETT;
                tax.CASDI = dto.CASDI;
                tax.EmployeeId = dto.EmployeeId;

                if (!await _taxWithholdingRepository.UpdateAsync(tax))
                {
                    return new() { Message = "error in updating Tax" };
                }
                // return response
                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "created",
                    Data = tax
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new() { Message = ex.Message };
            }
        }
    }
}
