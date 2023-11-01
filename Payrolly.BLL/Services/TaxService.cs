using Microsoft.Extensions.Logging;
using Payrolly.BLL.DTOs.Tax;
using Payrolly.BLL.Helpers.Responses;
using Payrolly.BLL.IServices;
using Payrolly.DAL.Constants;
using Payrolly.DAL.Entities;
using Payrolly.DAL.Interface;
using Payrolly.DAL.Repository;

namespace Payrolly.BLL.Services
{
    public class TaxService : ITaxService
    {
        #region Private Fields
        private readonly IFederalTaxRepository _federalTaxRepository;
        private readonly IStateTaxRepository _stateTaxRepository;
        private readonly ILogger<TaxService> _logger;
        #endregion

        #region Constructor
        public TaxService(
            IFederalTaxRepository federalTaxRepository,
            IStateTaxRepository stateTaxRepository,
            ILogger<TaxService> logger)
        {
            _federalTaxRepository = federalTaxRepository;
            _stateTaxRepository = stateTaxRepository;
            _logger = logger;
        }
        #endregion

        #region Get Services
        public async Task<GenericResponse<FederalTaxDto>> GetFederalTaxDto(string companyId)
        {
            try
            {
                var federalTax = await _federalTaxRepository.FindAsync(f => f.CompanyId == companyId);
                
                // if the employer not fill the federal tax info
                if (federalTax == null)
                {
                    return new()
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "empty data",
                        Data = new()
                    };
                }

                var federalTaxDto = new FederalTaxDto
                {
                    EIN = federalTax.EIN,
                    TaxForm = federalTax!.TaxForm,
                    TaxRate = federalTax!.TaxRate,
                };

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "data found",
                    Data = federalTaxDto
                };

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);

                return new() { Message = ex.Message };
            }
        }

        public async Task<GenericResponse<StateTaxDto>> GetStateTaxDto(string stateName, string companyId, StateTaxType stateTaxType)
        {
            try
            {
                var stateTax = await _stateTaxRepository.FindAsync(s => s.CompanyId == companyId 
                && s.StateName == stateName
                && s.StateTaxType == stateTaxType);

                if (stateTax == null)
                {
                    return new()
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "empty data",
                        Data = new()
                    };
                }

                var stateTaxDto = new StateTaxDto
                {
                    EmployerAccountNumber = stateTax.EmployerAccountNumber,
                    StateName = stateName,
                    TrainingTaxRate = stateTax.TrainingTaxRate,
                    UnEmploymentInsuranceRate = stateTax.UnEmploymentInsuranceRate,
                };

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "data found",
                    Data = stateTaxDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);

                return new() { Message = ex.Message };
            }
        }
        #endregion

        #region Update Services
        public async Task<GenericResponse<FederalTaxDto>> UpdateFederalTaxAsync(FederalTaxDto dto, string companyId)
        {
            try
            {
                var federalTax = await _federalTaxRepository.FindAsync(f => f.CompanyId == companyId);

                // if the federal tax info not filled yet so create new federal tax
                if (federalTax == null)
                {
                    federalTax = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CompanyId = companyId,
                        EIN = dto.EIN!,
                        TaxForm = dto.TaxForm,
                        TaxRate = dto.TaxRate
                    };

                    if (!await _federalTaxRepository.CreateAsync(federalTax))
                        return new() { Message = "error in assign federal tax for company" };

                    return new()
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "created",
                        Data = dto
                    };
                }

                // if it exist so update with the new data from the dto
                federalTax.EIN = dto.EIN!;
                federalTax.TaxForm = dto.TaxForm;
                federalTax.TaxRate = dto.TaxRate;

                if (!await _federalTaxRepository.UpdateAsync(federalTax))
                    return new() { Message = "update failed!" };

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "updated",
                    Data = dto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);

                return new() { Message = ex.Message };
            }
        }

        public async Task<GenericResponse<StateTaxDto>> UpdateStateTaxAsync(StateTaxDto dto, string companyId, StateTaxType stateTaxType)
        {
            try
            {
                var stateTax = await _stateTaxRepository.FindAsync(s => s.CompanyId == companyId
                && s.StateName == dto.StateName
                && s.StateTaxType == stateTaxType);

                if (stateTax == null)
                {
                    stateTax = new StateTax
                    {
                        Id = Guid.NewGuid().ToString(),
                        CompanyId = companyId,
                        StateName = dto.StateName,
                        EmployerAccountNumber = dto.EmployerAccountNumber,
                        TrainingTaxRate = dto.TrainingTaxRate,
                        UnEmploymentInsuranceRate = dto.UnEmploymentInsuranceRate,
                        StateTaxType = stateTaxType
                    };

                    if (!await _stateTaxRepository.CreateAsync(stateTax))
                        return new() { Message = $"error in assigning {dto.StateName} tax info" };

                    return new()
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "created",
                        Data = dto
                    };
                }

                stateTax.EmployerAccountNumber = dto.EmployerAccountNumber!;
                stateTax.UnEmploymentInsuranceRate = dto.UnEmploymentInsuranceRate;
                stateTax.TrainingTaxRate = dto.TrainingTaxRate;

                if (!await _stateTaxRepository.UpdateAsync(stateTax))
                    return new() { Message = "error in updating state tax info" };

                return new()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "created",
                    Data = dto
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return new() { Message = ex.Message };
            }
        }
        #endregion
    }
}
