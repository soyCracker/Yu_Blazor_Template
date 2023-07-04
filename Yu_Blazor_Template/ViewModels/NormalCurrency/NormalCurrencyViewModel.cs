using System.Net.Http.Json;
using Yu_Service.Models;
using Yu_Service.Models.Currency;

namespace Yu_Blazor_Template.ViewModels.NormalCurrency
{
    public class NormalCurrencyViewModel : INormalCurrencyViewModel
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly string currencyUrl = "http://localhost:5000/api/Currency/ExchangeRate";

        public NormalCurrencyViewModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        private List<RTER_CurrencyModel> rterCurrencyList = new List<RTER_CurrencyModel>();
        public List<RTER_CurrencyModel> RTER_CurrencyList
        {
            get => rterCurrencyList;
        }

        //public RTER_CurrencyModel RTER_CurrencyModel { get; }

        public async Task RefreshAsync()
        {
            using var httpClient = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, currencyUrl);
            var res = await httpClient.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
                var apiModel = await res.Content.ReadFromJsonAsync<CommonAPIModel<List<RTER_CurrencyModel>>>();
                rterCurrencyList = apiModel.Data;

            }
        }
    }
}
