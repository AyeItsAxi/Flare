namespace Flare.Commands.CommandLogic.Main;

public class GraveCommand
{
    public static async Task RunCommandLogic(SocketMessage message, string avatarUrl)
    {
        await message.Channel.SendMessageAsync($"https://vacefron.nl/api/grave?user={avatarUrl}");
    }
}