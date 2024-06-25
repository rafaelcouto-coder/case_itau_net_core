using CaseItau.Application.Abstractions.Messaging;
using CaseItau.Domain.Abstractions;
using CaseItau.Domain.Fundos;
using CaseItau.Domain.Repositories;

namespace CaseItau.Application.Fundos.DeleteFundos;

public sealed class DeleteFundosCommandHandler(
    IFundosRepository fundosRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteFundosCommand, string>
{
    private readonly IFundosRepository _fundosRepository = fundosRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<string>> Handle(DeleteFundosCommand request, CancellationToken ct)
    {
        var fundo = await _fundosRepository.GetByCodeAsync(request.Codigo, ct);
        if (!FundoCodeExists(fundo))
            return Result.Failure<string>(FundoErrors.CodeDontExists);

        _fundosRepository.Delete(fundo!);
        await _unitOfWork.SaveChangesAsync(ct);

        return fundo!.Codigo;
    }

    private static bool FundoCodeExists(Fundo? fundo)
    {
        return fundo is not null;
    }
}
