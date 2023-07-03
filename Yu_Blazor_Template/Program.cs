using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Yu_Blazor_Template;
using Yu_Blazor_Template.Settings;
using Yu_Blazor_Template.ViewModels.Currency;
using Yu_Service.Interfaces;
using Yu_Service.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

HttpClient httpClient = new HttpClient()
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
};
builder.Services.AddScoped(sp => httpClient);
await builder.Configuration.LoadSettingJson(httpClient);

builder.Services.AddMudServices();
builder.Services.AddGraphClient();
builder.SetMsAuth();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ICurrencyViewModel, CurrencyViewModel>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();

//»y¨t
builder.Services.AddLocalization(option =>
{
    option.ResourcesPath = "Resources";
});

var host = builder.Build();

//»y¨t
await host.SetCulture();
await host.RunAsync();
