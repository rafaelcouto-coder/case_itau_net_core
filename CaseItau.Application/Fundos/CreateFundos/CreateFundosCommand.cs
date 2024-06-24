using CaseItau.Application.Abstractions.Messaging;

namespace CaseItau.Application.Fundos.CreateFundos;

public sealed record CreateFundosCommand(
    string Codigo,
    string Nome,
    string Cnpj,
    int TipoFundo,
    decimal? Patrimonio
 ) : ICommand<string>;
