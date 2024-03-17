using Auth0.AspNetCore.Authentication;
using Lib.Core.Rebus;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PDH.MessagingDemo.Shared;
using PDH.MessagingDemo.Ui;
using PDH.MessagingDemo.Ui.Auth;
using PDH.MessagingDemo.Ui.Components;
using Rebus.Routing.TypeBased;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = "pdh-services.eu.auth0.com";
    options.ClientId = "52pJJaBQpUbGycF8rS7UYnOjqQZaQUQn";
});

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddMessaging(options =>
{
    options.Provider = MessagingProvider.RabbitMq;
    options.QueueName = "message-queue";
    options.ConnectionString = builder.Configuration["RabbitMq:ConnectionString"]!;

    options.Routing = r => r.TypeBased()
        .Map<ChatMessage>("message-queue");

    options.Subscriptions = new List<Type> { typeof(ChatMessage) };

    options.Options = o => o.SetNumberOfWorkers(3);
    options.Handlers = new List<Type> { typeof(MessageHandler) };
});

builder.Services.AddSingleton(typeof(EventDispatcher<>));

builder.Services.AddHttpClient<ChatService>(options =>
{
    options.BaseAddress = new Uri(builder.Configuration["ApiBase"]!);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapGet("/Account/Login", async (HttpContext httpContext, string redirectUri = "/") =>
{
    var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
        .WithRedirectUri(redirectUri)
        .Build();

    await httpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
});

app.MapGet("/Account/Logout", async (HttpContext httpContext, string redirectUri = "/") =>
{
    var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
        .WithRedirectUri(redirectUri)
        .Build();

    await httpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(PDH.MessagingDemo.Ui.Client._Imports).Assembly);

app.Run();
