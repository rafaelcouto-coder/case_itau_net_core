using CaseItau.Application.Abstractions.Messaging;
using CaseItau.Domain.Abstractions;
using CaseItau.Domain.Fundos;
using CaseItau.Domain.Repositories;

namespace CaseItau.Application.Fundos.CreateFundos;

internal sealed class CreateFundosCommandHandler(
    IFundosRepository fundosRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateFundosCommand, string>
{
    private readonly IFundosRepository _fundosRepository = fundosRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<string>> Handle(CreateFundosCommand request, CancellationToken ct)
    {
        var fundoWithRequestedCode = await _fundosRepository.GetByCodeAsync(request.Codigo, ct);
        if (FundoCodeExists(fundoWithRequestedCode))
            return Result.Failure<string>(FundoErrors.CodeAlreadyExists);

        var fundoWithRequestedCnpj = await _fundosRepository.GetByCnpjAsync(request.Cnpj, ct);
        if (CnpjAlreadyExists(fundoWithRequestedCnpj, request.Codigo))
            return Result.Failure<string>(FundoErrors.CnpjAlreadyExists);

        var fundo = new Fundo(
            request.Codigo,
            request.Nome,
            request.Cnpj,
            request.TipoFundo,
            request.Patrimonio);

        _fundosRepository.Add(fundo);

        await _unitOfWork.SaveChangesAsync(ct);

        return fundo.Codigo;
    }

    private static bool FundoCodeExists(Fundo? fundo)
    {
        return fundo is not null;
    }

    private static bool CnpjAlreadyExists(Fundo? fundo, string codigo)
    {
        return fundo is not null && fundo.Codigo != codigo;
    }
}
