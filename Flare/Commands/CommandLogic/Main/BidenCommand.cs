namespace Flare.Commands.CommandLogic.Main;

public static class BidenCommand
{
    public static async Task RunCommandLogic(SocketMessage message, string text)
    {
        await message.Channel.SendMessageAsync($"https://api.popcat.xyz/biden?text={System.Web.HttpUtility.UrlEncode(text)}");
    }
}