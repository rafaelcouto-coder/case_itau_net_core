using CaseItau.Application.Abstractions.Messaging;
using CaseItau.Domain.Fundos.Enums;

namespace CaseItau.Application.Fundos.CreateFundos;

public sealed record CreateFundosCommand(
    string Codigo,
    string Nome,
    string Cnpj,
    TipoFundoEnum TipoFundo,
    decimal? Patrimonio
 ) : ICommand<string>;
