﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Yu_Blazor_Template.Settings
{
    public class GraphAPIAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public GraphAPIAuthorizationMessageHandler(IAccessTokenProvider provider,
            NavigationManager navigationManager) : base(provider, navigationManager)
        {
            ConfigureHandler(
                authorizedUrls: new[] { "https://graph.microsoft.com" },
                scopes: new[] { "https://graph.microsoft.com/User.Read", "https://graph.microsoft.com/Mail.Read" });
        }
    }
}
