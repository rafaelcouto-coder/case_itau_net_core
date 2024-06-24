using CaseItau.Domain.Fundos.Enums;

namespace CaseItau.API.Controllers.Fundos.Requests;

public sealed record EditFundosRequest(
    string Nome,
    string Cnpj,
    TipoFundoEnum TipoFundo);
