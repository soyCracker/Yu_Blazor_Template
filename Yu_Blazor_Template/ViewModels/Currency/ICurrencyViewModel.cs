using System.ComponentModel;
using Yu_Service.Models.Currency;

namespace Yu_Blazor_Template.ViewModels.Currency
{
    public interface ICurrencyViewModel
    {
        RTER_CurrencyModel RTER_CurrencyModel { get; set; }
        List<RTER_CurrencyModel> RTER_CurrencyList { get; }
        Task RefreshAsync();
        event PropertyChangedEventHandler PropertyChanged;
    }
}
