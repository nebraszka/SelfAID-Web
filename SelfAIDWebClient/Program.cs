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

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddSingleton<TokenService>();
builder.Services.AddScoped<JwtTokenHandler>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddScoped<IEmotionService, EmotionService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddHttpClient<IEmotionService, EmotionService>(ConfigureHttpClient)
                .AddHttpMessageHandler<JwtTokenHandler>();

builder.Services.AddHttpClient<IAuthService, AuthService>(ConfigureHttpClient)
                .AddHttpMessageHandler<JwtTokenHandler>();

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