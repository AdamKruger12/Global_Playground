using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shadowBot
{
  public class CommandDictionary
  {
    public Dictionary<string, string> commandDictionary = new Dictionary<string, string>() 
    { 
      { "#play","Plays a single song from link or title or will play a playlist from a link."},
      { "#skip","Skips current playing song."},
      { "#pause","Pauses the current song."},
      { "#unpause","Continues paused song."},
      { "#resume","Continues paused song."},
      { "#list","See the current songs in the queue."},
      { "#join","Joins the voice channel of the requester."},
      { "#leave","Causes bot to leave the channel."},
      { "#admin","Displays the creator/owner of this bot."},
      { "#help","help brings up this message..."},
    };
  }
}
