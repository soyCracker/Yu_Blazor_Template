using KristofferStrube.Blazor.FileSystemAccess;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Yu_Blazor_Template;
using Yu_Blazor_Template.Settings;
using Yu_Blazor_Template.ViewModels.Currency;
using Yu_Blazor_Template.ViewModels.NormalCurrency;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddScoped<GraphAPIAuthorizationMessageHandler>();
builder.Services.AddHttpClient("GraphAPI",
        client => client.BaseAddress = new Uri("https://graph.microsoft.com"))
    .AddHttpMessageHandler<GraphAPIAuthorizationMessageHandler>();

builder.Services.AddMudServices();
builder.SetMsAuth();
builder.Services.AddFileSystemAccessService();
builder.Services.AddScoped<ICurrencyViewModel, CurrencyViewModel>();
builder.Services.AddScoped<INormalCurrencyViewModel, NormalCurrencyViewModel>();

//»y¨t
builder.Services.AddLocalization(option =>
{
    option.ResourcesPath = "Resources";
});

var host = builder.Build();

//»y¨t
await host.SetCulture();
await host.RunAsync();
