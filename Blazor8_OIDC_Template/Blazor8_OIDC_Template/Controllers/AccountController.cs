using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Blazor8_OIDC_Template.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //[HttpGet]
        //public async Task Login(string redirectUri)
        //{
        //    var props = new AuthenticationProperties();
        //    props.RedirectUri = redirectUri;
        //    await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, props);
        //}

        //[HttpGet]
        //public async Task Logout(string redirectUri)
        //{
        //    var props = new AuthenticationProperties();
        //    props.RedirectUri = redirectUri;

        //    await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, props);
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //}
    }
}
