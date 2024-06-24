using CaseItau.Application.Abstractions.Messaging;
using CaseItau.Application.Fundos.Shared;
using CaseItau.Domain.Abstractions;
using CaseItau.Domain.Repositories;

namespace CaseItau.Application.Fundos.GetFundos;

public sealed class GetFundosByCodeQueryHandler(IFundosRepository fundosRepository) : IQueryHandler<GetFundosByCodeQuery, FundosResponse>
{
    private readonly IFundosRepository _fundosRepository = fundosRepository;

    public async Task<Result<FundosResponse>> Handle(GetFundosByCodeQuery request, CancellationToken ct)
    {
        var fundos = await _fundosRepository.GetByCodeAsync(request.Code, ct);

        var response = fundos == null ? null : new FundosResponse
        {
            Codigo = fundos.Codigo,
            Nome = fundos.Nome,
            Cnpj = fundos.Cnpj,
            TipoFundo = fundos.TipoFundo!,
            Patrimonio = fundos.Patrimonio
        };

        return response;
    }
}
