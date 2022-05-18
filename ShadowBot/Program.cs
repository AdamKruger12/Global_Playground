using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using Microsoft.Extensions.Configuration;
using DSharpPlus.Interactivity.Extensions;
using shadowBot.Commands;
using DSharpPlus.Net;
using DSharpPlus.Lavalink;
using System.Diagnostics;

namespace shadowBot
{
  internal class Program
  {
    private CancellationTokenSource _cts { get; set; }
    private IConfigurationRoot _config;
    private DiscordClient _discord;

    static async Task Main(string[] args) => await new Program().InitBot(args);

    private async Task InitBot(string[] args)
    {
      try
      {
        Console.WriteLine("[info] Welcome to my bot!");
        _cts = new CancellationTokenSource();

        Console.WriteLine("[info] Loading config file..");
        _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json", optional: false, reloadOnChange: true)
            .Build();

        Console.WriteLine("[info] Creating discord client..");
        _discord = new DiscordClient(new DiscordConfiguration
        {
          Token = _config.GetValue<string>("discord:token"),
          TokenType = TokenType.Bot
        });


        var commands = _discord.UseCommandsNext(new CommandsNextConfiguration
        {
          StringPrefixes = new[] { _config.GetValue<string>("discord:CommandPrefix") },
        });


        Console.WriteLine("[info] Loading command modules..");
        commands.RegisterCommands<BasicCommandsModule>();
        commands.RegisterCommands<MusicCommandsModule>();
        //commented endpoint connects to global lavalink(external)
        //var endpoint = new ConnectionEndpoint
        //{
        //    Hostname = "lava.link", // From your server configuration.
        //    Port = 80 // From your server configuration
        //};
        //connects to local lavalink executable
        var endpoint = new ConnectionEndpoint
        {
          Hostname = "localhost",
          Port = 2333
        };

        var lavalinkConfig = new LavalinkConfiguration
        {
          Password = "youshallnotpass", // From your server configuration.
          RestEndpoint = endpoint,
          SocketEndpoint = endpoint
        };

        // Connect to discord's service
        Console.WriteLine("Connecting..");

        var lavalink = _discord.UseLavalink();

        await _discord.ConnectAsync();
        await lavalink.ConnectAsync(lavalinkConfig);
        //await Task.Delay(-1);


        RunAsync(args).Wait();
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine(ex.ToString());
      }
    }
    async Task RunAsync(string[] args)
    {
      //await _discord.ConnectAsync();
      Console.WriteLine("Connected!");

      // Keep the bot running until the cancellation token requests we stop
      while (!_cts.IsCancellationRequested)
        await Task.Delay(TimeSpan.FromMinutes(1));
    }
  }
}
