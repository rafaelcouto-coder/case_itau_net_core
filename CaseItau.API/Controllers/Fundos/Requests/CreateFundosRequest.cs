using CaseItau.Domain.Fundos.Enums;

namespace CaseItau.API.Controllers.Fundos.Requests;

public sealed record CreateFundosRequest(
    string Codigo,
    string Nome,
    string Cnpj,
    TipoFundoEnum TipoFundo,
    decimal? Patrimonio);

