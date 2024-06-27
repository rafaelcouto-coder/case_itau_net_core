using CaseItau.API.Controllers.Fundos.Requests;
using CaseItau.Application.Fundos.Shared;
using CaseItau.Domain.Fundos.Enums;
using CaseItau.Web.Services;
using CaseItau.Web.Services.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CaseItau.Web.Pages.Fundo
{
    public class EditModel(IFundosClientService fundosClientService) : PageModel
    {
        private readonly IFundosClientService _fundosClientService = fundosClientService;

        [BindProperty]
        public EditFundosRequest EditFundosRequest { get; set; }

        public FundosResponse FundosResponse { get; set; }

        public ApiError ErrorMessage { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            FundosResponse = await _fundosClientService.SearchFundosByCodeAsync(id, default);

            if (FundosResponse == null)
            {
                return NotFound();
            }

            EditFundosRequest = new EditFundosRequest(
                FundosResponse.Nome,
                FundosResponse.Cnpj,
                ConvertTipoFundoToEnum(FundosResponse.TipoFundo.Nome) // Assuming this is compatible with TipoFundoEnum
            );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id, EditFundosRequest editFundosRequest)
        {
            var result = await _fundosClientService.EditFundosAsync(id, editFundosRequest, default);

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

        private TipoFundoEnum ConvertTipoFundoToEnum(string tipoFundo)
        {
            switch (tipoFundo)
            {
                case "RENDA FIXA":
                    return TipoFundoEnum.RendaFixa;
                case "ACOES":
                    return TipoFundoEnum.Acoes;
                case "MULTI MERCARDO":
                    return TipoFundoEnum.MultiMercado;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tipoFundo), $"Not expected TipoFundo: {tipoFundo}");
            }
        }
    }
}
