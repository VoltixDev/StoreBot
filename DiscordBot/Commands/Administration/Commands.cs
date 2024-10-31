using Discord.Interactions;
using JetBrains.Annotations;

namespace DiscordBot.Commands.Administration;

[PublicAPI]
[Group("administration", "Administration commands")]
public partial class Commands : InteractionModuleBase;