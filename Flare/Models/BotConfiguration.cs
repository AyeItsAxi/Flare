namespace Flare.Models;

// ReSharper disable once ClassNeverInstantiated.Global
public class BotConfiguration
{
    #nullable disable
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CommandAliases
    {
        public List<string> Adios { get; } = new();
        public List<string> Avatar { get; } = new();
        public List<string> Ban { get; } = new();
        public List<string> Biden { get; } = new();
        public List<string> CarReverse { get; } = new();
        public List<string> Cat { get; } = new();
        public List<string> Dog { get; } = new();
        public List<string> Drip { get; } = new();
        public List<string> Github { get; } = new();
        public List<string> Grave { get; } = new();
        public List<string> Heaven { get; } = new();
        public List<string> Help { get; } = new();
        public List<string> Kick { get; } = new();
        public List<string> Lockdown { get; } = new();
        public List<string> Lyrics { get; } = new();
        public List<string> Mute { get; } = new();
        public List<string> Ping { get; } = new();
        public List<string> Purge { get; } = new();
        public List<string> SadCat { get; } = new();
        // disable inconsistentnaming because its how the command is processed in InterpretCommand
        // ReSharper disable once InconsistentNaming
        public List<string> ServerConfiguration_SetAutoModLinkFilter { get; } = new();
        public List<string> Softban { get; } = new();
        public List<string> Stats { get; } = new();
        public List<string> Unban { get; } = new();
        public List<string> Unmute { get; } = new();
        public List<string> Water { get; } = new();
        public List<string> Wide { get; } = new();
        public List<string> Wolverine { get; } = new();
    }

    public class Root
    {
        // TODO: Prefix changing
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string BotPrefix { get; set; }
        public string BotToken { get; set; }
        public string StatusType { get; set; }
        public string StatusContent { get; set; }
        public CommandAliases CommandAliases { get; }
    }
#nullable enable

}