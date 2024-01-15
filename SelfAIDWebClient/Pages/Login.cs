using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SelfAID.CommonLib.Dtos.User;
using SelfAID.WebClient.Authorization;
using SelfAID.WebClient.Services;

namespace SelfAID.WebClient.Pages;

public partial class Login : ComponentBase
{
    [Inject]
    private IAuthService authService { get; set; }

    [Inject]
    private AuthenticationStateProvider authenticationStateProvider { get; set; }

    [Inject]
    private NavigationManager navigationManager { get; set; }

    [Inject]
    private TokenService tokenService { get; set; }

    [Inject]
    private HttpClient Http { get; set; }


    protected string Message = string.Empty;
    public UserDto user = new UserDto();

    protected async Task HandleLogin()
    {
        var response = await authService.LoginUser(user);
        if (response != null && response.Success)
        {
            tokenService.SetToken(response.Data);

            var customAuthProvider = authenticationStateProvider as CustomAuthStateProvider;
            customAuthProvider?.NotifyUserAuthentication(response.Data);
            navigationManager.NavigateTo("/");
        }
        else
        {
            Message = response?.Message ?? "Błąd logowania";
        
        }
    }
}