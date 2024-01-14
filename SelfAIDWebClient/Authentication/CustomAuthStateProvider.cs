using System.Security.Claims;
using System.Text.Json;
using Microsoft.JSInterop;

namespace SelfAID.WebClient.Authorization
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        // private readonly IJSRuntime _jsRuntime;

        // public CustomAuthStateProvider(IJSRuntime jsRuntime)
        // {
        //     _jsRuntime = jsRuntime;
        // }

        public async Task SetAuthenticationStateAsync(string jwtToken)
        {
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(jwtToken), "jwt");

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            // await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "jwtToken", jwtToken);

            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}