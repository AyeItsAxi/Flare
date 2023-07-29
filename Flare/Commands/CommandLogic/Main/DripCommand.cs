﻿namespace Flare.Commands.CommandLogic.Main;

public class DripCommand
{
    public static async Task RunCommandLogic(SocketMessage message, string avatarUrl)
    {
        await message.Channel.SendMessageAsync($"https://vacefron.nl/api/drip?user={avatarUrl}");
    }
}