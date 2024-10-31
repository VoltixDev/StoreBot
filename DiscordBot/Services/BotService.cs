using Discord;
using Discord.WebSocket;
using DiscordBot.Events;

namespace DiscordBot.Services;

public class BotService(
    ILogger<BotService> logger,
    IConfiguration configuration,
    BotEvents events,
    DiscordSocketClient client
    ) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        EventAttribute.RegisterEvents(events, client);
        
        // Should be stored in .NET user secrets for Development, or environment variables for Production.
        await client.LoginAsync(TokenType.Bot, configuration.GetValue<string>("Token"));
        await client.StartAsync();
    }
}