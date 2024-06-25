using CaseItau.Domain.Fundos.ValueObjects;

namespace CaseItau.Application.Fundos.Shared;

public sealed class FundosResponse
{
    public required string Codigo { get; set; }
    public required string Nome { get; set; }
    public required string Cnpj { get; set; }
    public required TipoFundo TipoFundo { get; set; }
    public decimal? Patrimonio { get; set; }
}
