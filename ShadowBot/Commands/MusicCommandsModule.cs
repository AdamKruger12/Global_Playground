using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Lavalink;
using System.Linq;
using System;
using System.Collections.Generic;

namespace shadowBot.Commands
{
  class MusicCommandsModule : BaseCommandModule
  {
    private CommandContext ctx;
    private LavalinkGuildConnection conn;
    private List<LavalinkTrack> trackList = new List<LavalinkTrack>();
    private bool isPlaying = false;

    [Command("play")]
    [Description("Plays selected music from input")]
    public async Task Play(CommandContext ctx, [RemainingText] string search)
    {

      var node = await ConnectAsync(ctx);
      if (node == null)
        return;

      //audio base64 string will be loading into here...
      LavalinkLoadResult loadResult = new LavalinkLoadResult();
      LavalinkLoadResult loadResultfromSearch = new LavalinkLoadResult();
      Console.WriteLine("Reading input from user...");
      if (search == null) { await conn.ResumeAsync(); return; }
      if (search.Contains("http"))//what if the text is a link?
        loadResult = await node.Rest.GetTracksAsync(new Uri(search));
      else
        loadResultfromSearch = await node.Rest.GetTracksAsync(search);

      if (loadResult.LoadResultType == LavalinkLoadResultType.LoadFailed
          || loadResult.LoadResultType == LavalinkLoadResultType.NoMatches)
      {
        Console.WriteLine($"Track search failed for {search}.");
        await ctx.RespondAsync($"Track search failed for {search}.");
        return;
      }

      Console.WriteLine($"Processing audio queue...");
      //TODO: check if i cant just queue the music in this loop(i.e. skip the whole listing process. 

      if (loadResultfromSearch.Tracks != null)
      {
        var track = loadResultfromSearch.Tracks.ToList().FirstOrDefault();
        trackList.Add(track);
      }
      else
      {

        foreach (var track in loadResult.Tracks)
        {
          trackList.Add(track);
        }
      }

      if (!isPlaying)
      {

        Console.WriteLine("Starting playlist...");
        await ctx.Client.UpdateStatusAsync(activity: new DiscordActivity($"{trackList[0].Title}", ActivityType.Playing));
        await conn.PlayAsync(trackList[0]);
        isPlaying = true;
        await ctx.TriggerTypingAsync();
        await ctx.RespondAsync($"Now playing {trackList[0].Title}!");
        trackList.RemoveAt(0);
        this.ctx = ctx;
        conn.PlaybackFinished += Conn_PlaybackFinished;//create event listener that executes upon ending of a song.
      }
      else
      {
        await ctx.RespondAsync($"Added song to the playlist");
      }

    }

    //not sure i like this, will probably redo
    private async Task Conn_PlaybackFinished(LavalinkGuildConnection sender, DSharpPlus.Lavalink.EventArgs.TrackFinishEventArgs e)
    {
      //check if items in list... if yes then process the first item.
      if (trackList.Count > 0)
      {
        await ctx.Client.UpdateStatusAsync(activity: new DiscordActivity($"{trackList[0].Title}", ActivityType.Playing));
        await conn.PlayAsync(trackList[0]);
        await ctx.RespondAsync($"Now playing {trackList[0].Title}!");
        trackList.RemoveAt(0);
      }
      return;
    }

    [Command("list")]
    public async Task List(CommandContext ctx) 
    {
      string response = string.Empty;
      if (trackList.Count == 0) 
      {
        await ctx.RespondAsync($"Tracklist is empty. type #play with song link or song name to play");
        return;
      }
      response = $"Queued songs : ";
      foreach (var item in trackList) 
      {
        response  += $"\n\r{item.Title}";
      }

      await ctx.RespondAsync(response);

    }

    [Command("skip")]
    public async Task Skip(CommandContext ctx)
    {
      if (conn == null)
        await ctx.RespondAsync("No tracks in queue");

      await conn.StopAsync();
    }


    [Command("pause")]
    public async Task Pause(CommandContext ctx)
    {
      if (conn == null)
      {
        await ctx.RespondAsync("Lavalink is not connected.");
        return;
      }

      if (conn.CurrentState.CurrentTrack == null)
      {
        await ctx.RespondAsync("There are no tracks loaded.");
        return;
      }

      await conn.PauseAsync();
    }

    [Command("unpause")]
    public async Task Unpause(CommandContext ctx)
    {
      await conn.ResumeAsync();
    }

    [Command("resume")]
    public async Task Resume(CommandContext ctx)
    {
      await conn.ResumeAsync();
    }

    [Command("join")]
    public async Task Join(CommandContext ctx, DiscordChannel discordChanel)
    {
      var node = await ConnectAsync(ctx);

      if (discordChanel.Type != ChannelType.Voice)
      {
        await ctx.RespondAsync("Not a valid voice channel.");
        return;
      }

      await node.ConnectAsync(discordChanel);
      await ctx.RespondAsync($"Joined {discordChanel.Name}!");
    }

    [Command("leave")]
    public async Task Leave(CommandContext ctx, DiscordChannel discordChanel)
    {

      if (discordChanel.Type != ChannelType.Voice)
      {
        await ctx.RespondAsync("Not a valid voice channel.");
        return;
      }

      if (conn == null)
      {
        await ctx.RespondAsync("Bot is not connected.");
        return;
      }
      await ctx.Client.UpdateStatusAsync(activity: new DiscordActivity($"Awaiting command...", ActivityType.Custom));
      await conn.DisconnectAsync();
      await ctx.RespondAsync($"Left {discordChanel.Name}!");
    }


    private async Task<LavalinkNodeConnection> ConnectAsync(CommandContext ctx)
    {
      //always check if the bot isn't already connected.
      if (conn != null)
        return ctx.Client.GetLavalink().ConnectedNodes.Values.First();

      //is the command coming from outside a voice channel?
      if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
      {
        await ctx.RespondAsync("You are not in a voice channel.");
        return null;
      }

      //obtain discord channel information if empty

      var lava = ctx.Client.GetLavalink();
      var node = lava.ConnectedNodes.Values.First();
      conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

      //handle failed connection
      if (conn == null)
      {
        //one last ditch effort to connect...
        var nextConn = await node.ConnectAsync(ctx.Member.VoiceState.Channel);
        if (nextConn == null)
        {
          await ctx.RespondAsync("No connection could be made to voice channel.");
          return null;
        }
      }

      conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

      return node;
    }


  }
}
