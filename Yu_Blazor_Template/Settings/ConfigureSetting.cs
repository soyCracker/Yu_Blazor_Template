using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;

namespace Yu_Blazor_Template.Settings
{
    public static class ConfigureSetting
    {
        public static async Task LoadSettingJson(this WebAssemblyHostConfiguration configuration, HttpClient httpClient)
        {
            using var response = await httpClient.GetAsync("yusettings.json");
            using var stream = await response.Content.ReadAsStreamAsync();

            configuration.AddJsonStream(stream);
        }

        public static void SetMsAuth(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add("User.Read");
                options.ProviderOptions.DefaultAccessTokenScopes.Add("Mail.Read");
            });
        }

        public static async Task SetCulture(this WebAssemblyHost host)
        {
            CultureInfo culture;
            var js = host.Services.GetRequiredService<IJSRuntime>();
            var result = await js.InvokeAsync<string>("blazorCulture.get");

            culture = new CultureInfo("zh-Hant");
            await js.InvokeVoidAsync("blazorCulture.set", "zh-Hant");
            /*if (result != null)
            {
                culture = new CultureInfo(result);
            }
            else
            {
                culture = new CultureInfo("zh-Hant");
                await js.InvokeVoidAsync("blazorCulture.set", "zh-Hant");
            }*/

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }
}
