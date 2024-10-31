using Discord;
using Discord.WebSocket;
using JetBrains.Annotations;
using Serilog;
using Serilog.Events;

namespace DiscordBot.Events;

[PublicAPI]
public partial class BotEvents
{
    [Event(nameof(DiscordSocketClient.Log))]
    public Task LogEvent(LogMessage msg)
    {
        var severity = msg.Severity switch
        {
            LogSeverity.Critical => LogEventLevel.Fatal,
            LogSeverity.Error => LogEventLevel.Error,
            LogSeverity.Warning => LogEventLevel.Warning,
            LogSeverity.Info => LogEventLevel.Information,
            LogSeverity.Verbose => LogEventLevel.Debug,
            LogSeverity.Debug => LogEventLevel.Verbose,
            _ => LogEventLevel.Information
        };
        
        Log.Write(severity, msg.Exception, "{Message}", msg.Message);

        return Task.CompletedTask;
    }
}