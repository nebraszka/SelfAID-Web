using System.Security.Claims;
using System.Text.Json;
using Microsoft.JSInterop;

namespace SelfAID.WebClient.Authorization
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly TokenService _tokenService;

        public CustomAuthStateProvider(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);

            var authToken = _tokenService.GetToken();
            if (!string.IsNullOrEmpty(authToken))
            {
                var claims = ParseClaimsFromJwt(authToken);
                identity = new ClaimsIdentity(claims, "jwt");
                user = new ClaimsPrincipal(identity);
            }

            return new AuthenticationState(user);
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

        public void NotifyUserAuthentication(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}