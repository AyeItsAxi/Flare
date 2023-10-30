namespace Flare.Services
{
    internal static class Variables
    {
        public static DiscordSocketClient DiscordClient = null!;
        public static CommandService ClientCommandService = null!;
        public static string? FlareBuildVersion = "Flare Alpha, Version 0.2";
        public static BotConfiguration.Root json = null!;
    }
}
