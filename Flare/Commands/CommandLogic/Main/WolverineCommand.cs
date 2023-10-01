﻿namespace Flare.Commands.CommandLogic.Main;

public static class WolverineCommand
{
    public static async Task RunCommandLogic(SocketMessage message, string avatarUrl)
    {
        await message.Channel.SendMessageAsync($"https://vacefron.nl/api/wolverine?user={avatarUrl}");
    }
}