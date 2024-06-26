using CaseItau.API.Controllers.Fundos.Requests;
using CaseItau.Application.Fundos.Shared;
using CaseItau.Domain.Fundos.Enums;
using CaseItau.Web.Service;
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
            var updateResult = await _fundosClientService.EditFundosAsync(id, editFundosRequest, default);

            if (updateResult.IsSuccess)
            {
                return RedirectToPage("../Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Houve um erro ao atualizar o fundo.");
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
