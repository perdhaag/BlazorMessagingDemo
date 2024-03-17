using Lib.Core.Rebus;
using PDH.MessagingDemo.ServiceB;
using PDH.MessagingDemo.Shared;
using Rebus.Routing.TypeBased;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMessaging(options =>
{
    options.Provider = MessagingProvider.RabbitMq;
    options.QueueName = "message-queue";
    options.ConnectionString = builder.Configuration["RabbitMq:ConnectionString"]!;

    options.Routing = r => r.TypeBased()
        .Map<ChatMessage>("message-queue");

    options.Subscriptions = new() { typeof(ChatMessage) };
    options.Handlers = new() { typeof(MessageHandler) };

    options.Options = o => o.SetNumberOfWorkers(3);
});

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();
