using System.Reflection;
using Discord.WebSocket;

namespace DiscordBot.Events;

public partial class BotEvents
{
    [Event(nameof(DiscordSocketClient.Ready))]
    public async Task Ready()
    {
        await service.AddModulesAsync(Assembly.GetExecutingAssembly(), provider);
        await service.RegisterCommandsToGuildAsync(1280161741325729939);
    }
}