namespace Flare.Commands.CommandLogic.Main;

public static class WaterCommand
{
    public static async Task RunCommandLogic(SocketMessage message, string text)
    {
        await message.Channel.SendMessageAsync($"https://vacefron.nl/api/water?text={text.Replace(" ", "%20")}");
    }
}