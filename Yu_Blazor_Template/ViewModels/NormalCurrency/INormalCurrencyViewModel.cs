using Yu_Service.Models.Currency;

namespace Yu_Blazor_Template.ViewModels.NormalCurrency
{
    public interface INormalCurrencyViewModel
    {
        //RTER_CurrencyModel RTER_CurrencyModel { get; }
        List<RTER_CurrencyModel> RTER_CurrencyList { get; }
        Task RefreshAsync();
    }
}
