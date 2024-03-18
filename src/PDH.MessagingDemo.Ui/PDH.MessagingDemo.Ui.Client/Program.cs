using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PDH.MessagingDemo.Ui;
using PDH.MessagingDemo.Ui.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddHttpClient<ChatService>(options =>
{
    options.BaseAddress = new Uri("https://localhost:5000");
});
builder.Services.AddSingleton(typeof(EventDispatcher<>));
builder.Services.AddSingleton<UserStore>();
builder.Services.AddTransient<MessageReciever>();
await builder.Build().RunAsync();
