using CaseItau.Web.Service;
using CaseItau.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CaseItau.Web.Model;

public class FundosModel : PageModel
{
    private readonly IFundosClientService _fundosClientService;
    public FundoViewModel FundoViewModel { get; set; }

    public FundosModel(IFundosClientService fundosClientService)
    {
        _fundosClientService = fundosClientService;
    }

    public async Task OnGetAsync()
    {
        var fundos = await _fundosClientService.SearchAllFundosAsync(CancellationToken.None);
        FundoViewModel = new FundoViewModel { Fundos = fundos };
    }
}