using Lib.Core.Rebus;
using PDH.MessagingDemo.ServiceA;
using PDH.MessagingDemo.Shared;
using Rebus.Routing.TypeBased;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOneWayMessaging(options =>
{
    options.Provider = MessagingProvider.RabbitMq;
    options.QueueName = "message-queue";
    options.ConnectionString = builder.Configuration["RabbitMq:ConnectionString"]!;

    options.Routing = r => r.TypeBased()
        .Map<ChatMessage>("message-queue");

    options.Subscriptions = new() { typeof(ChatMessage) };

    options.Options = o => o.SetNumberOfWorkers(1);
});

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("/message", SendMessage.Execute);

app.Run();