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
            Claims = GetAnonymous();
        }

        private ClaimsPrincipal Claims { get; set; }
        private ClaimsPrincipal GetUser(string userName,string position)
        {
            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name,userName),
            new Claim(ClaimTypes.Role,position ?? "admin"),

            }, "CustomAuth");
            return new ClaimsPrincipal(identity);
        }

        private ClaimsPrincipal GetAnonymous()
        {
            var identity = new ClaimsIdentity();
            return new ClaimsPrincipal(identity);
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(Claims));
        }

        public async Task LogIn(string username,string position)
        {
            Claims = GetUser(username,position);
            var task = GetAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(task);
        }

        public async Task Logout()
        {
            Claims =GetAnonymous();
            var task =GetAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(task);
        }
    }
}

