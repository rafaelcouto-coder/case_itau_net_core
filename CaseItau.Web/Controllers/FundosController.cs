using CaseItau.Web.Service;
using CaseItau.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CaseItau.Web.Controllers
{
    public class FundosController : Controller
    {
        private readonly IFundosClientService _fundosClientService;

        public FundosController(IFundosClientService fundosClientService)
        {
            _fundosClientService = fundosClientService;
        }

        public async Task<IActionResult> Index()
        {
            var fundos = await _fundosClientService.SearchAllFundosAsync(CancellationToken.None);
            var viewModel = new FundoViewModel { Fundos = fundos };
            return View(viewModel);
        }
    }
}
