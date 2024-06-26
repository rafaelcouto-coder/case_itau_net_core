using CaseItau.API.Controllers.Fundos.Requests;
using CaseItau.Application.Fundos.Shared;
using CaseItau.Domain.Abstractions;

namespace CaseItau.Web.Service;

public interface IFundosClientService
{
    Task<FundosResponse> SearchFundosByCodeAsync(string code, CancellationToken ct);
    Task<List<FundosResponse>> SearchAllFundosAsync(CancellationToken ct);
    Task<Result> CreateFundosAsync(CreateFundosRequest request, CancellationToken ct);
    Task<Result> EditFundosAsync(string codigo, EditFundosRequest request, CancellationToken ct);
    Task<Result> UpdatePatrimonioAsync(string codigo, UpdatePatrimonioRequest request, CancellationToken ct);
    Task<Result> DeleteFundosAsync(string codigo, CancellationToken ct);
}