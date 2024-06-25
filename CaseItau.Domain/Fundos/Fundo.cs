using CaseItau.Domain.Fundos.ValueObjects;

namespace CaseItau.Domain.Fundos;

public class Fundo
{
    public string Codigo { get; set; }
    public string Nome { get; set; }
    public string Cnpj { get; set; }
    public int TipoFundoId { get; set; }
    public TipoFundo TipoFundo { get; set; }
    public decimal? Patrimonio { get; set; }

    public Fundo(string codigo, string nome, string cnpj, int tipoFundoId, decimal? patrimonio)
    {
        Codigo = codigo;
        Nome = nome;
        Cnpj = cnpj;
        TipoFundoId = tipoFundoId;
        Patrimonio = patrimonio;
    }

    public void UpdateFundo(string nome, string cnpj, int tipoFundoId)
    {
        Nome = nome;
        Cnpj = cnpj;
        TipoFundoId = tipoFundoId;
    }
}
