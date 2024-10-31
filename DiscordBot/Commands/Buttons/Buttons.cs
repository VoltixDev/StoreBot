using Discord.Interactions;
using Discord.Webhook;
using Discord.WebSocket;

namespace DiscordBot.Commands.Buttons;

public partial class Buttons(DiscordSocketClient client, DiscordWebhookClient webhookClient, IHostEnvironment environment) : InteractionModuleBase;