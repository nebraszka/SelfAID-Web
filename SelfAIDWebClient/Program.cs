global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.LocalStorage;

using SelfAID.CommonLib.Services;
using SelfAID.WebClient.Authorization;
using SelfAID.WebClient.Services;

var builder = WebApplication.CreateBuilder(args);

static void ConfigureHttpClient(HttpClient client)
{
    client.BaseAddress = new Uri("https://selfaid.azurewebsites.net/api/");
}


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<IEmotionService, EmotionService>();
builder.Services.AddHttpClient<IEmotionService, EmotionService>(ConfigureHttpClient);
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddHttpClient<IAuthService, AuthService>(ConfigureHttpClient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();


//Czasem przy zapisz musze czekac az wyskoczy message i dopiero przy drugim kliknieciu
// Bardziej szcvzegolowy komunikat