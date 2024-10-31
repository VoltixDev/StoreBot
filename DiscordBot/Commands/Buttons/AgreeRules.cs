using Discord;
using Discord.Interactions;
using JetBrains.Annotations;

namespace DiscordBot.Commands.Buttons;

[PublicAPI]
public partial class Buttons
{
    [ComponentInteraction("agree-rules")]
    public async Task AgreeRules()
    {
        if (Context.User is not IGuildUser user)
        {
            await RespondAsync(embed: new EmbedBuilder()
                .WithColor(Color.Red)
                .WithTitle("Couldn't parse to an IGuildUser.")
                .WithDescription("Please contact Voltix Development to report the issue.")
                .WithCurrentTimestamp()
                .Build(), ephemeral: true
            );

            return;
        }
        
        if (user.RoleIds.Any(x => x == 1280161741325729940))
        {
            await RespondAsync(embed: new EmbedBuilder()
                .WithColor(Color.Red)
                .WithTitle("You are already a member!")
                .WithCurrentTimestamp()
                .Build(), ephemeral: true);

            return;
        }
        
        await webhookClient.SendMessageAsync(embeds:
        [
            new EmbedBuilder()
                .WithColor(Color.Green)
                .WithTitle($"{Context.User.Username} has agreed to the rules.")
                .WithCurrentTimestamp()
                .Build()
        ]);

        await user.AddRoleAsync(1280161741325729940);
    }
}