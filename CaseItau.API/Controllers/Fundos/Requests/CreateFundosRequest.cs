namespace CaseItau.API.Controllers.Fundos.Requests;

public sealed record CreateFundosRequest(
    string Codigo,
    string Nome,
    string Cnpj,
    int TipoFundo,
    decimal? Patrimonio);

