using CaseItau.Application.Abstractions.Messaging;
using CaseItau.Application.Fundos.Shared;

namespace CaseItau.Application.Fundos.GetFundos;

public sealed record GetFundosByCodeQuery(string Code) : IQuery<FundosResponse>;
