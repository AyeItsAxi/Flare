namespace Flare.Commands.CommandLogic.Main;

public class WideCommand
{
    public static async Task RunCommandLogic(SocketMessage message, string imageUrl)
    {
        await message.Channel.SendMessageAsync($"https://vacefron.nl/api/wide?image={imageUrl}");
    }
}