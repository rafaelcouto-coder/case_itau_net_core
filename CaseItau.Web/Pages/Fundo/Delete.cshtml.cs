using CaseItau.Application.Fundos.Shared;
using CaseItau.Web.Services;
using CaseItau.Web.Services.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CaseItau.Web.Pages.Fundo
{
    public class DeleteModel(IFundosClientService fundosClientService) : PageModel
    {
        private readonly IFundosClientService _fundosClientService = fundosClientService;

        [BindProperty]
        public FundosResponse FundosResponse { get; set; }

        public ApiError ErrorMessage { get; set; }

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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var result = await _fundosClientService.DeleteFundosAsync(id, default);

            if (result.IsSuccess)
            {
                return RedirectToPage("../Index");
            }
            else
            {
                ErrorMessage = new ApiError();

                if (result.Error != null)
                {
                    ErrorMessage.Name = result.Error.Name;
                    ErrorMessage.Code = result.Error.Code;
                }

                return Page();
            }
        }
    }
}
