using Lib.Core.Rebus;
using PDH.MessagingDemo.Shared;
using PDH.MessagingDemo.Web;
using PDH.MessagingDemo.Web.Client;
using PDH.MessagingDemo.Web.Components;
using Rebus.Routing.TypeBased;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddHttpClient<Service>(options =>
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

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(PDH.MessagingDemo.Web.Client._Imports).Assembly);

app.Run();
