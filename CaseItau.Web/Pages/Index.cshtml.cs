using CaseItau.API.Controllers.Fundos.Requests;
using CaseItau.Application.Fundos.Shared;
using CaseItau.Web.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CaseItau.Web.Pages;

public class FundosModel(IFundosClientService fundosClientService) : PageModel
{
    private readonly IFundosClientService _fundosClientService = fundosClientService;

    [BindProperty]
    public CreateFundosRequest FundoViewModel { get; set; }

    public List<FundosResponse> Fundos { get; set; } = new List<FundosResponse>();

    public async Task<IActionResult> OnGetAsync()
    {
        Fundos = await _fundosClientService.SearchAllFundosAsync(CancellationToken.None);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        //if (!ModelState.IsValid)
        //{
        //    Fundos = await _fundosClientService.SearchAllFundosAsync(cancellationToken);
        //    return Page();
        //}

        var createResult = await _fundosClientService.CreateFundosAsync(FundoViewModel, cancellationToken);

        if (createResult.IsSuccess)
        {
            return RedirectToPage("./Index");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Houve um erro ao criar o fundo.");
            Fundos = await _fundosClientService.SearchAllFundosAsync(cancellationToken);
            return Page();
        }
    }
}
