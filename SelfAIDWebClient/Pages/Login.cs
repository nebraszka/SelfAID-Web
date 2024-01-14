  using Microsoft.AspNetCore.Components;
using SelfAID.CommonLib.Dtos.User;
using SelfAID.WebClient.Services;

namespace SelfAID.WebClient.Pages;

public partial class Login : ComponentBase
{
    [Inject]
    private IAuthService authService { get; set; }

    [Inject]
    private AuthenticationStateProvider authenticationStateProvider { get; set; }

    [Inject]
    private ILocalStorageService localStorageService { get; set; }

    [Inject]
    private NavigationManager navigationManager { get; set; }

    protected string Message = string.Empty;
    public UserDto user = new UserDto();

    protected async Task HandleLogin()
    {
        var response = await authService.LoginUser(user);
        Console.WriteLine(response.Message);
        if (response != null)
        {
            if (response.Success)
            {
                await localStorageService.SetItemAsync("authToken", response.Data);
                await authenticationStateProvider.GetAuthenticationStateAsync();
                navigationManager.NavigateTo("/");
            }
            else
            {
                Message = response.Message;
            }
        }
        else
        {
            Message = "Błąd logowania";
        }
    }
}