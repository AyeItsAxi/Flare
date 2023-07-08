namespace Flare.Commands.CommandLogic;

public static class PingCommand
{
    public static async Task RunCommandLogic(SocketMessage message)
    {
        var pingMessage = await message.Channel.SendMessageAsync("Pong!");
        await pingMessage.ModifyAsync(m => m.Content = $"Pong! `{Variables.DiscordClient.Latency}ms`");
    }
}