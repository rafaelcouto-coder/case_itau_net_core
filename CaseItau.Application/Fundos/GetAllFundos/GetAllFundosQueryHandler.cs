using CaseItau.Application.Abstractions.Messaging;
using CaseItau.Application.Fundos.Shared;
using CaseItau.Domain.Abstractions;
using CaseItau.Domain.Repositories;

namespace CaseItau.Application.Fundos.GetAllFundos;

internal sealed class GetAllFundosQueryHandler(IFundosRepository fundosRepository) : IQueryHandler<GetAllFundosQuery, List<FundosResponse>>
{
    private readonly IFundosRepository _fundosRepository = fundosRepository;

    public async Task<Result<List<FundosResponse>>> Handle(GetAllFundosQuery request, CancellationToken ct)
    {
        var fundos = await _fundosRepository.GetAllAsync(ct);

        var response = fundos.Select(fundo => new FundosResponse
        {
            Codigo = fundo.Codigo,
            Nome = fundo.Nome,
            Cnpj = fundo.Cnpj,
            TipoFundo = fundo.TipoFundo,
            Patrimonio = fundo.Patrimonio
        }).ToList();

        return response;
    }
}
