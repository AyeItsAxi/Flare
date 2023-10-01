namespace Flare.Commands.CommandLogic.Main;

public static class GraveCommand
{
    public static async Task RunCommandLogic(SocketMessage message, string avatarUrl)
    {
        await message.Channel.SendMessageAsync($"https://vacefron.nl/api/grave?user={avatarUrl}");
    }
}