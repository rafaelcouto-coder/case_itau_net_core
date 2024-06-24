using CaseItau.Application.Abstractions.Messaging;
using CaseItau.Application.Fundos.Shared;

namespace CaseItau.Application.Fundos.GetAllFundos;

public sealed record GetAllFundosQuery() : IQuery<List<FundosResponse>>;
