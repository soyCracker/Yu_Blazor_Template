using Yu_Service.Models;
using Yu_Service.Models.Currency;

namespace Yu_Service.Interfaces
{
    public interface ICurrencyService
    {
        Task<CommonAPIModel<List<RTER_CurrencyModel>>> RefreshAsync();
    }
}
