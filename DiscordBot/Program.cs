using Discord;
using Discord.Interactions;
using Discord.Net;
using Discord.Webhook;
using Discord.WebSocket;
using DiscordBot.Events;
using DiscordBot.Services;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateBootstrapLogger();

builder.Services.AddSerilog();

#region Hosted Services

builder.Services.AddHostedService<BotService>();

#endregion

#region Custom-Made Services

builder.Services.AddSingleton<BotEvents>();

#endregion

#region Discord.Net Services

builder.Services.AddSingleton(new DiscordSocketConfig
{
    LogLevel = LogSeverity.Debug,
    GatewayIntents = GatewayIntents.All,
    AlwaysDownloadUsers = true
}).AddSingleton<DiscordSocketClient>();

builder.Services.AddSingleton(new InteractionServiceConfig
{
    LogLevel = LogSeverity.Debug
}).AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()));

builder.Services.AddSingleton(
    new DiscordWebhookClient(builder.Configuration.GetRequiredSection("Webhooks").GetValue<string>("UserLogs")));

#endregion

var host = builder.Build();
host.Run();