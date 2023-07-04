using System.Globalization;
using System.Text.Json;
using Yu_Service.Interfaces;
using Yu_Service.Models;
using Yu_Service.Models.Currency;

namespace Yu_Service.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly string currencyUrl = "https://tw.rter.info/capi.php";

        public CurrencyService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<CommonAPIModel<List<RTER_CurrencyModel>>> RefreshAsync()
        {
            using var httpClient = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, currencyUrl);
            var res = await httpClient.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
                string originStr = await res.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(originStr))
                {
                    using JsonDocument doc = JsonDocument.Parse(originStr);
                    JsonElement root = doc.RootElement;
                    List<RTER_CurrencyModel> currencyList = new List<RTER_CurrencyModel>();
                    foreach (var currencyType in root.EnumerateObject())
                    {
                        decimal dExrate = 0;
                        if (currencyType.Value.TryGetProperty("Exrate", out JsonElement exrate))
                        {
                            dExrate = exrate.GetDecimal();
                        }
                        DateTime dateTime = DateTime.Now;
                        if (currencyType.Value.TryGetProperty("UTC", out JsonElement utc))
                        {
                            dateTime = DateTime.ParseExact(utc.GetString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentUICulture);
                        }
                        RTER_CurrencyModel currencyModel = new RTER_CurrencyModel
                        {
                            ExType = currencyType.Name,
                            Exrate = dExrate,
                            UTC = DateTime.UtcNow//dateTime
                        };
                        currencyList.Add(currencyModel);
                    }
                    return new CommonAPIModel<List<RTER_CurrencyModel>>()
                    {
                        Data = currencyList
                    };
                }
            }
            return new CommonAPIModel<List<RTER_CurrencyModel>>()
            {
                Success = false,
                Msg = "RefreshAsync fail",
                Data = new List<RTER_CurrencyModel>()
            };
        }
    }
}
