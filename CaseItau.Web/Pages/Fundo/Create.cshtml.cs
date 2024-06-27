using CaseItau.API.Controllers.Fundos.Requests;
using CaseItau.Web.Services;
using CaseItau.Web.Services.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CaseItau.Web.Pages.Fundo
{
    public class CreateModel(IFundosClientService fundosClientService) : PageModel
    {
        private readonly IFundosClientService _fundosClientService = fundosClientService;

        [BindProperty]
        public CreateFundosRequest CreateFundosRequest { get; set; }

        public ApiError ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            var result = await _fundosClientService.CreateFundosAsync(CreateFundosRequest, cancellationToken);

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
