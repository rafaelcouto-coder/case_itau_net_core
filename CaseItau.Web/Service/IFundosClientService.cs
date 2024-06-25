using CaseItau.API.Controllers.Fundos.Requests;
using CaseItau.Application.Fundos.Shared;
using CaseItau.Domain.Abstractions;

namespace CaseItau.Web.Service;

public interface IFundosClientService
{
    Task<List<FundosResponse>> SearchAllFundosAsync(CancellationToken ct);
    Task<Result> CreateFundosAsync(CreateFundosRequest request, CancellationToken ct);
}