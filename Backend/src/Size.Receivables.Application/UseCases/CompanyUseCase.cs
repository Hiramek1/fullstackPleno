using Size.Receivables.Application.Interfaces;
using Size.Receivables.Application.UseCases.Requests;
using Size.Receivables.Application.UseCases.Responses;
using Size.Receivables.CrossCutting.Services;
using Size.Receivables.Domain.Entities;
using Size.Receivables.Domain.Repositories;

namespace Size.Receivables.Application.UseCases;

public class CompanyUseCase(ICompanyRepository repository,
                            ICreditLimitUseCase creditLimitUseCase) : ICompanyUseCase
{
    public async Task<CustomResult<RetrieveCompanyResponse>> GetAsync(string cnpj)
    {
        var company = await repository.GetByCnpjAsync(cnpj);
        if (company is null)
            return CustomResult<RetrieveCompanyResponse>.Fail("Empresa ainda não cadastrada");

        return CustomResult<RetrieveCompanyResponse>.Ok(new RetrieveCompanyResponse
        {
            Name = company.Name,
            MonthlyRevenue = company.MonthlyRevenue,
            BusinessCategory = company.BusinessType,
            CreditLimit = company.CreditLimit
        });
    }

    public async Task<CustomResult<bool>> RegisterAsync(RegisterCompanyRequest request)
    {
        var company = new Company(
            request.Cnpj,
            request.Name,
            request.MonthlyRevenue,
            request.BusinessType
        );

        var creditLimit = creditLimitUseCase.DetermineLimit(
            request.MonthlyRevenue,
            request.BusinessType
        );
        company.SetCreditLimit(creditLimit);

        await repository.AddAsync(company);
        return CustomResult<bool>.Ok(true);
    }
}
