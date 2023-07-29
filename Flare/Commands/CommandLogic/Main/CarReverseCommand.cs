namespace Flare.Commands.CommandLogic.Main;

public class CarReverseCommand
{
    public static async Task RunCommandLogic(SocketMessage message, string text)
    {
        await message.Channel.SendMessageAsync($"https://vacefron.nl/api/carreverse?text={text}");
    }
}