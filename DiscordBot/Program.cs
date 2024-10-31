using DiscordBot.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<BotService>();

var host = builder.Build();
host.Run();