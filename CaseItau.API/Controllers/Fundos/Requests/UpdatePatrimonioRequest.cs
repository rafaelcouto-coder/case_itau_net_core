using CaseItau.Domain.Fundos.Enums;

namespace CaseItau.API.Controllers.Fundos.Requests;

public sealed record UpdatePatrimonioRequest(
    decimal Value,
    PatrimonyOperation Operation);
