namespace Flare.Commands.CommandLogic.Main;

public static class CatCommand
{
    public static async Task RunCommandLogic(SocketMessage message)
    {
        var catCommandMsg = await message.Channel.SendMessageAsync("Getting a random cat image for you...");
        await new WebClient().DownloadFileTaskAsync("https://cataas.com/cat", "App/Temp/CatCommandTemp.png");
        var imageEmbed = new EmbedBuilder()
            .WithTitle("Meow")
            .WithFooter($"Random cat for {message.Author.Username}")
            .WithColor(Color.Purple)
            .Build();
        await message.Channel.DeleteMessageAsync(message);
        await message.Channel.DeleteMessageAsync(catCommandMsg);
        await message.Channel.SendFileAsync("App/Temp/CatCommandTemp.png", "", false, imageEmbed);
        File.Delete("App/Temp/CatCommandTemp.png");
    }
}