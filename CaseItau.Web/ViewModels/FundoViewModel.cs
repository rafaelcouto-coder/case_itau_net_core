using CaseItau.Application.Fundos.Shared;

namespace CaseItau.Web.ViewModels
{
    public sealed class FundoViewModel
    {
        public IEnumerable<FundosResponse> Fundos { get; set; }
    }
}