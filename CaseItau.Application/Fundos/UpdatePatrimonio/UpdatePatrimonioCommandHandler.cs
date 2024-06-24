using CaseItau.Application.Abstractions.Messaging;
using CaseItau.Application.Fundos.UpdatePatrimonio.Factories;
using CaseItau.Domain.Abstractions;
using CaseItau.Domain.Fundos;
using CaseItau.Domain.Repositories;

namespace CaseItau.Application.Fundos.UpdatePatrimonio;

public sealed class UpdatePatrimonioCommandHandler : ICommandHandler<UpdatePatrimonioCommand, string>
{
    private readonly IFundosRepository _fundosRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePatrimonioCommandHandler(IFundosRepository fundosRepository, IUnitOfWork unitOfWork)
    {
        _fundosRepository = fundosRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdatePatrimonioCommand request, CancellationToken ct)
    {
        var fundo = await _fundosRepository.GetByCodeAsync(request.Codigo, ct);
        if (!FundoCodeExists(fundo))
            return Result.Failure<string>(FundoErrors.CodeDontExists);

        var action = PatrimonioActionFactory.GetAction(request.Operation);
        var actionResult = action.Execute(fundo!, request.Patrimonio);

        if (!actionResult.IsSuccess)
            return Result.Failure<string>(actionResult.Error);

        _fundosRepository.Update(fundo!);
        await _unitOfWork.SaveChangesAsync(ct);

        return fundo!.Codigo;
    }

    private static bool FundoCodeExists(Fundo? fundo)
    {
        return fundo is not null;
    }
}
