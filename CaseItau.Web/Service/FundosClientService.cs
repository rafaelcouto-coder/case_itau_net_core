using CaseItau.API.Controllers.Fundos.Requests;
using CaseItau.Application.Fundos.Shared;
using CaseItau.Domain.Abstractions;
using CaseItau.Domain.Fundos;

namespace CaseItau.Web.Service;

public sealed class FundosClientService(HttpClient httpClient) : IFundosClientService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<FundosResponse> SearchFundosByCodeAsync(string id, CancellationToken ct)
    {
        return await _httpClient.GetFromJsonAsync<FundosResponse>($"fundo/{id}", ct);
    }

    public async Task<List<FundosResponse>> SearchAllFundosAsync(CancellationToken ct)
    {
        var result = await _httpClient.GetFromJsonAsync<List<FundosResponse>>("fundo", ct);

        return result ?? new List<FundosResponse>();
    }

    public async Task<Result> CreateFundosAsync(CreateFundosRequest request, CancellationToken ct)
    {
        var response = await _httpClient.PostAsJsonAsync("fundo", request, ct);

        if (response.IsSuccessStatusCode)
            return Result.Success(response.StatusCode);

        return Result.Failure<string>(FundoErrors.UnexpectedResponse);
    }

    public async Task<Result> EditFundosAsync(string codigo, EditFundosRequest request, CancellationToken ct)
    {
        var response = await _httpClient.PutAsJsonAsync($"fundo/{codigo}", request, ct);
        if (response.IsSuccessStatusCode)
            return Result.Success(response.StatusCode);

        return Result.Failure<string>(FundoErrors.UnexpectedResponse);
    }

    public async Task<Result> UpdatePatrimonioAsync(string codigo, UpdatePatrimonioRequest request, CancellationToken ct)
    {
        var response = await _httpClient.PutAsJsonAsync($"fundo/{codigo}/patrimonio", request, ct);
        if (response.IsSuccessStatusCode)
            return Result.Success(response.StatusCode);

        return Result.Failure<string>(FundoErrors.UnexpectedResponse);
    }

    public async Task<Result> DeleteFundosAsync(string codigo, CancellationToken ct)
    {
        var response = await _httpClient.DeleteAsync($"fundo/{codigo}", ct);
        if (response.IsSuccessStatusCode)
            return Result.Success(response.StatusCode);

        return Result.Failure<string>(FundoErrors.UnexpectedResponse);
    }
}