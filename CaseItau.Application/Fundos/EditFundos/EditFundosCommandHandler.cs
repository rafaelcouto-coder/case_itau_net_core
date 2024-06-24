using CaseItau.Application.Abstractions.Messaging;
using CaseItau.Domain.Abstractions;
using CaseItau.Domain.Fundos;
using CaseItau.Domain.Repositories;

namespace CaseItau.Application.Fundos.EditFundos;

internal sealed class EditFundosCommandHandler(
    IFundosRepository fundosRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<EditFundosCommand, string>
{
    private readonly IFundosRepository _fundosRepository = fundosRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<string>> Handle(EditFundosCommand request, CancellationToken ct)
    {
        var fundoWithRequestedCode = await _fundosRepository.GetByCodeAsync(request.Codigo, ct);
        if (!FundoCodeExists(fundoWithRequestedCode))
            return Result.Failure<string>(FundoErrors.CodeDontExists);

        var fundoWithRequestedCnpj = await _fundosRepository.GetByCnpjAsync(request.Cnpj, ct);
        if (CnpjAlreadyExists(fundoWithRequestedCnpj, request.Codigo))
            return Result.Failure<string>(FundoErrors.CnpjAlreadyExists);

        fundoWithRequestedCode!.UpdateFundo(request.Nome, request.Cnpj, request.TipoFundo);

        _fundosRepository.Update(fundoWithRequestedCode);

        await _unitOfWork.SaveChangesAsync(ct);

        return fundoWithRequestedCode.Codigo;
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
