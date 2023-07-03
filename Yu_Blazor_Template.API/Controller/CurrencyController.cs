using Microsoft.AspNetCore.Mvc;
using Yu_Service.Interfaces;
using Yu_Service.Models;
using Yu_Service.Models.Currency;

namespace Yu_Blazor_Template.API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }

        [HttpGet]
        public async Task<CommonAPIModel<List<RTER_CurrencyModel>>> ExchangeRate()
        {
            return await currencyService.RefreshAsync();
        }
    }
}
