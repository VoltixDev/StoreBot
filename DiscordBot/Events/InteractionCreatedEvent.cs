using Discord.Interactions;
using Discord.WebSocket;
using Serilog;

namespace DiscordBot.Events;

public partial class BotEvents
{
    [Event(nameof(DiscordSocketClient.InteractionCreated))]
    public async Task InteractionCreatedEvent(SocketInteraction interaction)
    {
        Log.Debug("Received interaction {Interaction} from {User}", interaction.Id, interaction.User.Id);
        
        var ctx = new InteractionContext(client, interaction, interaction.Channel);
        await service.ExecuteCommandAsync(ctx, provider);
    }
}