using Microsoft.JSInterop;

namespace SelfAID.WebClient.Authorization
{
    public class CustomAuthStateProviderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CustomAuthStateProviderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public CustomAuthStateProvider Create()
    {
        return new CustomAuthStateProvider(_serviceProvider.GetRequiredService<IJSRuntime>());
    }
}

}