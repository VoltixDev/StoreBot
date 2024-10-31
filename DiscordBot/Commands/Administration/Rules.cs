using Discord;
using Discord.Interactions;
using Serilog;

namespace DiscordBot.Commands.Administration;

public partial class Commands
{
    [SlashCommand("rules", "Create a rules panel here.")]
    [RequireOwner]
    public async Task CreateRules()
    {
        var embeds = new List<EmbedBuilder>
        {
            new EmbedBuilder().WithTitle("1. Be Respectful").WithDescription("""
                                                                             - Treat all members with respect and courtesy.
                                                                             - No bullying, harassment, or discrimination of any kind.
                                                                             - Avoid sharing inappropriate, offensive, or NSFW content.
                                                                             """),
            new EmbedBuilder().WithTitle("2. No Swearing or Offensive Language").WithDescription("""
                - Please refrain from using profanity or offensive language.
                - Disrespectful or inflammatory language is not allowed and may result in a warning.
                """),
            new EmbedBuilder().WithTitle("3. No Arguments or Drama").WithDescription("""
                - Keep discussions civil. If you disagree with someone, be respectful.
                - Heated arguments and personal attacks are not allowed. Take any unresolved issues to private messages if necessary.
                - Avoid engaging in or escalating drama.
                """),
            new EmbedBuilder().WithTitle("4. Use Channels Appropriately").WithDescription("""
                - Keep conversations in the appropriate channels (e.g., bot commands in the designated channel).
                - Spamming or flooding channels with messages is not allowed.
                - Check channel descriptions and rules before posting to avoid disrupting the flow of conversation.
                """),
            new EmbedBuilder().WithTitle("5. No Abusing the Ticket System").WithDescription("""
                - Only create a support ticket if you genuinely need help or have a legitimate question.
                - Don’t spam, abuse, or misuse the ticket system.
                - Be patient — our support team will respond as soon as possible.
                """),
            new EmbedBuilder().WithTitle("6. No Self-Promotion or Advertising").WithDescription("""
                - Advertising personal projects, servers, or businesses is prohibited unless given explicit permission by an admin.
                - Sharing links that are irrelevant to the discussion or that are spammy is not allowed.
                """),
            new EmbedBuilder().WithTitle("7. Follow Discord’s Terms of Service").WithDescription("""
                - Adhere to [Discord’s Terms of Service](https://discord.com/terms) and [Community Guidelines](https://discord.com/guidelines).
                - Engaging in illegal activities or attempting to bypass our security measures will result in an immediate ban.
                """),
            new EmbedBuilder().WithTitle("8. Keep Personal Information Private").WithDescription("""
                - Do not share your own or others' personal information, such as addresses, phone numbers, or payment details.
                - For your safety, please keep sensitive information in private and secure channels.
                """),
            new EmbedBuilder().WithTitle("9. Listen to Staff and Moderators").WithDescription("""
                - Follow instructions from moderators and admins. Their decisions are final.
                - If you disagree with a decision, you may respectfully appeal in private, but public disputes are not allowed.
                """),
            new EmbedBuilder().WithTitle("10. Have Fun and Be Kind!").WithDescription("""
                - We’re here to build a positive and enjoyable community — help each other out, share ideas, and enjoy your time with us!
                """)
        };

        embeds = embeds.Select(x => x.WithCurrentTimestamp()
            .WithColor(Color.DarkMagenta)).ToList();
        
        await Context.Channel.SendMessageAsync(
            embeds: embeds.Select(x => x.Build()).ToArray(),
            components: new ComponentBuilder()
                .WithButton("Our terms", null, ButtonStyle.Link, Emoji.Parse("📋"), "https://github.com/VoltixDev/.github/blob/main/TERMS.md")
                .WithButton("I agree to to abide by the terms and rules.", "agree-rules", ButtonStyle.Success, Emoji.Parse("✅")).Build()
            );

        await RespondAsync(embed: new EmbedBuilder().WithCurrentTimestamp().WithColor(Color.Magenta).WithTitle("Successfully sent embeds.").Build(), ephemeral: true);
    }
}