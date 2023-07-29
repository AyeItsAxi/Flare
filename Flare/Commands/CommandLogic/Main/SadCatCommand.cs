namespace Flare.Commands.CommandLogic.Main;

public class SadCatCommand
{
    public static async Task RunCommandLogic(SocketMessage message, string text)
    {
        await message.Channel.SendMessageAsync($"https://api.popcat.xyz/sadcat?text={System.Web.HttpUtility.UrlEncode(text)}");
    }
}