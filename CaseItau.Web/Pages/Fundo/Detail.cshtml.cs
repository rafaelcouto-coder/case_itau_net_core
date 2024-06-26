using CaseItau.Application.Fundos.Shared;
using CaseItau.Web.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CaseItau.Web.Pages.Fundo
{
    public class DetailModel : PageModel
    {
        private readonly IFundosClientService _fundosClientService;

        public DetailModel(IFundosClientService fundosClientService)
        {
            _fundosClientService = fundosClientService;
        }

        [BindProperty]
        public FundosResponse FundosResponse { get; set; }

        public async Task<IActionResult> OnGetAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var fundosResponse = await _fundosClientService.SearchFundosByCodeAsync(id, cancellationToken);

            if (fundosResponse == null)
            {
                return NotFound();
            }

            FundosResponse = fundosResponse;

            return Page();
        }
    }
}
