using Discord.Interactions;
using Discord.WebSocket;

namespace DiscordBot.Events;

public partial class BotEvents(IServiceProvider provider, DiscordSocketClient client, InteractionService service);