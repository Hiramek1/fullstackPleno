namespace Size.Receivables.Domain.Entities;

public class Invoice
{
    public Invoice(int companyId, string number, decimal amount, DateTime dueDate)
    {
        if (dueDate <= DateTime.UtcNow)
            throw new ArgumentException("Data de vencimento deve ser futura");

        CompanyId = companyId;
        Number = number;
        Amount = amount;
        DueDate = dueDate;
    }

    public int Id { get; private set; }
    public int CompanyId { get; private set; }
    public string Number { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime DueDate { get; private set; }
}
