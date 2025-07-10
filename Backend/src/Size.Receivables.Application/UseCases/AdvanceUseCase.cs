using Size.Receivables.Application.Interfaces;
using Size.Receivables.Application.UseCases.Dtos;
using Size.Receivables.Application.UseCases.Requests;
using Size.Receivables.Application.UseCases.Responses;
using Size.Receivables.CrossCutting.Helpers;
using Size.Receivables.CrossCutting.Services;
using Size.Receivables.Domain.Entities;
using Size.Receivables.Domain.Repositories;

namespace Size.Receivables.Application.UseCases;

public class AdvanceUseCase(IDateTimeHelper dateTimeHelper,
                            ICompanyRepository companyRepository,
                            ICreditLimitUseCase creditLimitUseCase) : IAdvanceUseCase
{
    public async Task<CustomResult<AdvanceResponse>> ExecuteAsync(AdvanceRequest request)
    {
        var company = await companyRepository.GetByCnpjAsync(request.CompanyCnpj);
        if (company is null)
            return CustomResult<AdvanceResponse>.Fail("Empresa não encontrada");

        var selectedInvoices = company.GetInvoicesByNumbers(request.InvoiceNumbers);
        if (!selectedInvoices.Any())
            return CustomResult<AdvanceResponse>.Fail("Nenhuma nota válida encontrada");

        var creditLimit = company.CreditLimit > 0
            ? company.CreditLimit
            : creditLimitUseCase.DetermineLimit(company.MonthlyRevenue, company.BusinessType);

        var grossTotal = selectedInvoices.Sum(i => i.Amount);
        if (company.WouldExceedCreditLimit(grossTotal))
            return CustomResult<AdvanceResponse>.Fail("Limite de crédito excedido");

        var advanceDetails = CalculateAdvanceDetails(selectedInvoices, dateTimeHelper.Now);

        return CustomResult<AdvanceResponse>.Ok(new AdvanceResponse
        {
            CompanyName = company.Name,
            Cnpj = company.Cnpj,
            AdvanceLimit = creditLimit,
            GrossTotal = grossTotal,
            NetTotal = advanceDetails.netTotal,
            Invoices = advanceDetails.invoicesAdvanceList
        });
    }

    private (decimal netTotal, List<InvoiceAdvanceDto> invoicesAdvanceList)
        CalculateAdvanceDetails(List<Invoice> invoices, DateTime currentDate)
    {
        const decimal monthlyRate = 0.0465m;
        var invoicesAdvanceList = new List<InvoiceAdvanceDto>();
        decimal netTotal = 0;

        foreach (var invoice in invoices)
        {
            var days = (invoice.DueDate - currentDate).Days;
            var dailyRate = Math.Pow(1 + (double)monthlyRate, 1 / 30.0) - 1;
            var discountFactor = Math.Pow(1 + dailyRate, days);
            var netAmount = (decimal)((double)invoice.Amount / discountFactor);

            invoicesAdvanceList.Add(new InvoiceAdvanceDto
            {
                Number = invoice.Number,
                GrossAmount = invoice.Amount,
                NetAmount = Math.Round(netAmount, 2)
            });

            netTotal += Math.Round(netAmount, 2);
        }

        return (netTotal, invoicesAdvanceList);
    }
}
