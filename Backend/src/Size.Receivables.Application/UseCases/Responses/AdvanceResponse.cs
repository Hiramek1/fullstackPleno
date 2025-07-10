using System.Collections.Generic;
using System.Text.Json.Serialization;
using Size.Receivables.Application.UseCases.Dtos;

namespace Size.Receivables.Application.UseCases.Responses;

public class AdvanceResponse
{
    [JsonPropertyName("empresa")]
    public string CompanyName { get; set; } = null!;
    
    [JsonPropertyName("cnpj")]
    public string Cnpj { get; set; } = null!;
    
    [JsonPropertyName("limite")]
    public decimal AdvanceLimit { get; set; }
    
    [JsonPropertyName("total_bruto")]
    public decimal GrossTotal { get; set; }
    
    [JsonPropertyName("total_liquido")]
    public decimal NetTotal { get; set; }
    
    [JsonPropertyName("notas_fiscais")]
    public List<InvoiceAdvanceDto> Invoices { get; set; } = new();
}
