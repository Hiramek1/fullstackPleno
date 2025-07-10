using Size.Receivables.Domain.Enums;

namespace Size.Receivables.Domain.Entities;

public class Company
{
    public Company(string cnpj, string name, decimal monthlyRevenue, BusinessType businessType)
    {
        Cnpj = cnpj;
        Name = name;
        MonthlyRevenue = monthlyRevenue;
        BusinessType = businessType;
        CreditLimit = 0;
        Invoices = new List<Invoice>();
    }

    public int Id { get; private set; }
    public string Cnpj { get; private set; }
    public string Name { get; private set; }
    public decimal MonthlyRevenue { get; private set; }
    public BusinessType BusinessType { get; private set; }
    public decimal CreditLimit { get; private set; }
    public List<Invoice> Invoices { get; private set; }
    public decimal TotalOpenInvoices => Invoices.Sum(i => i.Amount);

    public void SetCreditLimit(decimal limit) => CreditLimit = limit;
    public void AddInvoice(Invoice invoice)
    {
        if (invoice.CompanyId != this.Id)
            throw new InvalidOperationException("Nota Fiscal não pertence a esta Empresa.");

        if (invoice.DueDate <= DateTime.UtcNow)
            throw new InvalidOperationException("Data da Nota Fiscal deve ser maior que hoje.");

        if (Invoices.Any(i => i.Number == invoice.Number))
            throw new InvalidOperationException("Nota Fiscal já cadastrada.");

        Invoices.Add(invoice);
    }

    public List<Invoice> GetInvoicesByNumbers(List<string> numbers)
    {
        return Invoices.Where(i => numbers.Contains(i.Number)).ToList();
    }

    public bool WouldExceedCreditLimit(decimal amount)
    {
        return amount > CreditLimit;
    }

    public bool WouldAdvanceExceedCreditLimit(decimal advanceAmount)
    {
        return advanceAmount > CreditLimit;
    }
}
