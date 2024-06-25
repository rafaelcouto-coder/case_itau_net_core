using CaseItau.Domain.Abstractions;

namespace CaseItau.Domain.Fundos;

public static class FundoErrors
{
    public static Error CodeAlreadyExists = new(
        "Fundo.CodeAlreadyExists",
        "The code already exists for another Fundo");

    public static Error CodeDontExists = new(
        "Fundo.CodeDontExists",
        "The code don't exists for Fundo");

    public static Error CnpjAlreadyExists = new(
        "Fundo.CnpjAlreadyExists",
        "The cnpj already exists for another Fundo");

    public static Error NegativePatrimonyNotAllowed = new(
        "Fundo.NegativePatrimonyNotAllowed",
        "Negative patrimony value is not allowed");
}

