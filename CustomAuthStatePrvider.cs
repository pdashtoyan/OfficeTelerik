using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using OfficeTelerik.Data;
using System.Security.Claims;
using Telerik.SvgIcons;

namespace OfficeTelerik
{
    public class CustomAuthStatePrvider : AuthenticationStateProvider
    {
        public CustomAuthStatePrvider()
        {
            Claims = GetUser();
        }

        private ClaimsPrincipal Claims { get; set; }
        private ClaimsPrincipal GetUser(string userName = null, string position = null)
        {
            var identity = new ClaimsIdentity();
            if (userName != null)
            {
                 identity = new ClaimsIdentity(new[]
                 {
                     new Claim(ClaimTypes.Name,userName),
                     new Claim(ClaimTypes.Role,position ?? "admin"),

                 }, "CustomAuth");
            }
            return new ClaimsPrincipal(identity);
        }
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(Claims));
        }

        public async Task LogIn(string username, string position)
        {
            Claims = GetUser(username, position);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            Claims = GetUser();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}

