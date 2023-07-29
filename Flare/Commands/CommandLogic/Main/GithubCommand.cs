namespace Flare.Commands.CommandLogic.Main;

public class GithubCommand
{
    public static async Task RunCommandLogic(SocketMessage message, string text)
    {
        var jsonResponse = JObject.Parse(new WebClient().DownloadString($"https://api.popcat.xyz/github/{text.Replace(" ", "%20")}"));
        if (string.IsNullOrWhiteSpace(jsonResponse["name"]?.ToString()))
        {
            var failEmbed = new EmbedBuilder()
                .WithTitle($"Failed to find profile {text}!")
                .WithDescription("Make sure you specifed a valid Github username.")
                .WithColor(Color.Red)
                .Build();
            await message.Channel.SendMessageAsync(null, false, failEmbed);
            return;
        }
        
        var imageEmbed = new EmbedBuilder()
            .WithTitle($"{jsonResponse["name"]}'s Github profile.")
            .WithUrl(jsonResponse["url"]?.ToString())
            .WithDescription($"**{jsonResponse["followers"]}** followers, following **{jsonResponse["following"]}**.\n*{jsonResponse["bio"]}*\n\n**{jsonResponse["name"]}**'s profile was created at {jsonResponse["created_at"]} and has **{jsonResponse["public_repos"]}** public repositories.")
            .WithThumbnailUrl(jsonResponse["avatar"]?.ToString())
            .WithColor(Color.Purple)
            .Build();
        await message.Channel.SendMessageAsync(null, false, imageEmbed);
    }
}