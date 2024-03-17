using Lib.Core.Rebus;
using Microsoft.EntityFrameworkCore;
using PDH.MessagingDemo.ServiceA.Endpoints;
using PDH.MessagingDemo.ServiceA.Persistence;
using Rebus.Routing.TypeBased;
using ChatMessage = PDH.MessagingDemo.Shared.ChatMessage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ChatContext>(options =>
{
    options.UseSqlServer(builder.Configuration["Db:ConnectionString"]!);
});

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

app.MapEndpoints();

app.Run();