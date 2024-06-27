using CaseItau.API.Controllers.Fundos.Requests;
using CaseItau.Application.Fundos.Shared;
using CaseItau.Web.Services;
using CaseItau.Web.Services.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CaseItau.Web.Pages.Fundo
{
    public class UpdatePatrimonyModel(IFundosClientService fundosClientService) : PageModel
    {
        private readonly IFundosClientService _fundosClientService = fundosClientService;

        [BindProperty]
        public UpdatePatrimonioRequest UpdatePatrimonioRequest { get; set; }
        public FundosResponse FundosResponse { get; set; }
        public ApiError ErrorMessage { get; set; }


        public async Task<IActionResult> OnGet(string id, CancellationToken cancellationToken)
        {
            var fundosResponse = await _fundosClientService.SearchFundosByCodeAsync(id, cancellationToken);

            if (fundosResponse == null)
            {
                return NotFound();
            }
            FundosResponse = fundosResponse;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id, UpdatePatrimonioRequest updatePatrimonioRequest)
        {
            var result = await _fundosClientService.UpdatePatrimonioAsync(id, updatePatrimonioRequest, default);

            if (result.IsSuccess)
            {
                FundosResponse = await _fundosClientService.SearchFundosByCodeAsync(id, default);
                return Page();
            }
            else
            {
                FundosResponse = await _fundosClientService.SearchFundosByCodeAsync(id, default);

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
