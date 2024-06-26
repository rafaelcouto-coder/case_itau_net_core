namespace CaseItau.Web.Extensions;

public static class StringExtensions
{
    public static string FormatCnpj(this string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj) || cnpj.Length != 14)
            return cnpj;

        return $"{cnpj.Substring(0, 2)}.{cnpj.Substring(2, 3)}.{cnpj.Substring(5, 3)}/{cnpj.Substring(8, 4)}-{cnpj.Substring(12, 2)}";
    }

    public static string FormatPatrimony(this decimal? patrimony)
    {
        if (patrimony == null)
        {
            return "Não informado";
        }
        else
        {
            return string.Format(System.Globalization.CultureInfo.GetCultureInfo("pt-BR"), "R$ {0:N2}", patrimony.Value);
        }
    }
}
