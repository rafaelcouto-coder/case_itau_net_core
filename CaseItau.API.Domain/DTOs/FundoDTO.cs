﻿namespace CaseItau.API.Domain.DTOs
{
    public abstract class FundoDTO
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public int CodigoTipo { get; set; }
        public decimal? Patrimonio { get; set; }
        public string NomeTipo { get; set; }
    }
}