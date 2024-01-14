using System.Net.Http.Headers;

namespace SelfAID.WebClient.Authorization
{
public class JwtTokenHandler : DelegatingHandler
{
    private readonly TokenService _tokenService;

    public JwtTokenHandler(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authToken = _tokenService.GetToken();
        if (!string.IsNullOrEmpty(authToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", authToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}

}
