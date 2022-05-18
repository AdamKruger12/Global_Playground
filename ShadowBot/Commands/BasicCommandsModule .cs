using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shadowBot.Commands
{
  public class BasicCommandsModule : BaseCommandModule
  {
    [Command("Awaken")]
    [Description("Are you brave enough to awaken the demon within?")]
    public async Task Awaken(CommandContext ctx)
    {
      /* Trigger the Typing... in discord */
      await ctx.TriggerTypingAsync();
      Console.WriteLine("Awaken executed");
      /* Send the message "I'm Alive!" to the channel the message was recieved from */
      await ctx.RespondAsync("Who dares awaken me!?");
    }


    [Command("admin")]
    [Description("Simple command to test interaction!")]
    public async Task Interact(CommandContext ctx)
    {
      /* Trigger the Typing... in discord */
      await ctx.TriggerTypingAsync();
      Console.WriteLine("Interact executed");
      await ctx.RespondAsync("Current admin is Snowflame... All hail the all mighty!");
    }
     
    //[Command("help")]
    //[Description("Command to display a list of available commands with descriptions.")]
    //public async Task Help(CommandContext ctx) 
    //{
    //  string help = new CommandDictionary().commandDictionary.ToString();

    //  await ctx.TriggerTypingAsync();
    //  await ctx.RespondAsync(help);
    //}

  }
}
