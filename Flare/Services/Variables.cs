﻿using System.Windows.Controls;

namespace Flare.Services
{
    internal static class Variables
    {
        public static DiscordSocketClient DiscordClient = null!;
        public static CommandService ClientCommandService = null!;
        public static Models.ECommandEnum CommandContext = Models.ECommandEnum.Ping;
    }
}
