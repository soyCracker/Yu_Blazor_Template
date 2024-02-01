using Blazor8_OIDC_Template.Auth;
using Blazor8_OIDC_Template.Client.Pages;
using Blazor8_OIDC_Template.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

builder.Services.AddMudServices();

/*
 *  Blazor Web App(.Net 8)，整合OIDC，教學來自Auth0工程師 Andrea Chiarelli : 
 *  https://auth0.com/blog/auth0-authentication-blazor-web-apps/ 
 *
 */

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.Authority = config.GetValue<string>("MS_OIDC:Instance");
        options.ClientId = config.GetValue<string>("MS_OIDC:ClientID");
        options.ClientSecret = config.GetValue<string>("MS_OIDC:ClientSecret");
        options.ResponseType = "code";
        options.SaveTokens = true;
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidAudience = config.GetValue<string>("MS_OIDC:ClientID"),
        };
    });

//builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapGet("/Account/Login", async (HttpContext httpContext, string redirectUri = "/") =>
{
    var props = new AuthenticationProperties();
    props.RedirectUri = redirectUri;
    await httpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, props);
});

app.MapGet("/Account/Logout", async (HttpContext httpContext, string redirectUri = "/") =>
{
    var props = new AuthenticationProperties();
    props.RedirectUri = redirectUri;

    await httpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, props);
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
});
//app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

app.Run();
