using CaseItau.Application.Abstractions.Messaging;
using CaseItau.Domain.Fundos.Enums;

namespace CaseItau.Application.Fundos.EditFundos;

public sealed record EditFundosCommand(
    string Codigo,
    string Nome,
    string Cnpj,
    TipoFundoEnum TipoFundo
 ) : ICommand<string>;
