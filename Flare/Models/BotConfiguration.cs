using System.Collections.Generic;

namespace Flare.Models;

public class BotConfiguration
{
    #nullable disable
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CommandAliases
    {
        public List<string> Adios { get; set; }
        public List<string> Avatar { get; set; }
        public List<string> Ban { get; set; }
        public List<string> Biden { get; set; }
        public List<string> CarReverse { get; set; }
        public List<string> Cat { get; set; }
        public List<string> Dog { get; set; }
        public List<string> Drip { get; set; }
        public List<string> Github { get; set; }
        public List<string> Grave { get; set; }
        public List<string> Heaven { get; set; }
        public List<string> Help { get; set; }
        public List<string> Kick { get; set; }
        public List<string> Lockdown { get; set; }
        public List<string> Lyrics { get; set; }
        public List<string> Mute { get; set; }
        public List<string> Ping { get; set; }
        public List<string> Purge { get; set; }
        public List<string> SadCat { get; set; }
        public List<string> ServerConfiguration_SetAutoModLinkFilter { get; set; }
        public List<string> Softban { get; set; }
        public List<string> Stats { get; set; }
        public List<string> Unban { get; set; }
        public List<string> Unmute { get; set; }
        public List<string> Water { get; set; }
        public List<string> Wide { get; set; }
        public List<string> Wolverine { get; set; }
    }

    public class Root
    {
        public string BotPrefix { get; set; }
        public string BotToken { get; set; }
        public string StatusType { get; set; }
        public string StatusContent { get; set; }
        public CommandAliases CommandAliases { get; set; }
    }
#nullable enable

}