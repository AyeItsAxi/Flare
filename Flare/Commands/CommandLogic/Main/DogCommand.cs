namespace Flare.Commands.CommandLogic.Main;

public class DogCommand
{
    public static async Task RunCommandLogic(SocketMessage message)
    {
        var dogCommandMsg = await message.Channel.SendMessageAsync("Getting a random dog image for you...");
        var imageEmbed = new EmbedBuilder()
            .WithTitle("Woof")
            .WithFooter($"Random dog for {message.Author.Username}")
            .WithImageUrl(JObject.Parse(new WebClient().DownloadString("https://dog.ceo/api/breeds/image/random")).GetValue("message")?.ToString())
            .WithColor(Color.Purple)
            .Build();
        await message.Channel.SendMessageAsync(null, false, imageEmbed);
        await message.Channel.DeleteMessageAsync(message);
        await message.Channel.DeleteMessageAsync(dogCommandMsg);
    }
}