namespace Flare.Commands.CommandLogic.Main;

public class AdiosCommand
{
    public static async Task RunCommandLogic(SocketMessage message, string avatarUrl)
    {
        await message.Channel.SendMessageAsync($"https://vacefron.nl/api/adios?user={avatarUrl}");
    }
}