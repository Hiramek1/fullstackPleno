using System.Text.Json.Serialization;

namespace Size.Receivables.Application.UseCases.Dtos;

public class InvoiceAdvanceDto
{
    [JsonPropertyName("numero")]
    public string Number { get; set; } = null!;
    
    [JsonPropertyName("valor_bruto")]
    public decimal GrossAmount { get; set; }
    
    [JsonPropertyName("valor_liquido")]
    public decimal NetAmount { get; set; }
}
