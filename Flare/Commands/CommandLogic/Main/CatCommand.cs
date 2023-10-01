namespace Flare.Commands.CommandLogic.Main;

public static class CatCommand
{
    #pragma warning disable SYSLIB0014
    public static async Task RunCommandLogic(SocketMessage message)
    {
        var catCommandMsg = await message.Channel.SendMessageAsync("Getting a random cat image for you...");
        var imageEmbed = new EmbedBuilder()
            .WithTitle("Meow")
            .WithImageUrl((string)JArray.Parse(new WebClient().DownloadString("https://api.thecatapi.com/v1/images/search"))[0]["url"]!)
            .WithFooter($"Random cat for {message.Author.Username}")
            .WithColor(Color.Purple)
            .Build();
        await message.Channel.SendMessageAsync(null, false, imageEmbed);
        await message.Channel.DeleteMessageAsync(message);
        await message.Channel.DeleteMessageAsync(catCommandMsg);
    }
    #pragma warning restore SYSLIB0014
}