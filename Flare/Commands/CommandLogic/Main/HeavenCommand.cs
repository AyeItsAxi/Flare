namespace Flare.Commands.CommandLogic.Main;

public class HeavenCommand
{
    public static async Task RunCommandLogic(SocketMessage message, string avatarUrl)
    {
        await message.Channel.SendMessageAsync($"https://vacefron.nl/api/heaven?user={avatarUrl}");
    }
}