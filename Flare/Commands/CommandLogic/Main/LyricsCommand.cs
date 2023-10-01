namespace Flare.Commands.CommandLogic.Main;

public static class LyricsCommand
{
    #pragma warning disable SYSLIB0014
    //this works 1/20th of the time but its free so im not complaining
    public static async Task RunCommandLogic(SocketMessage message, string songName)
    {
       var loadingMsg = await message.Channel.SendMessageAsync($"{new Emoji("\uD83D\uDD0D")} Getting lyrics for this song. Buckle up, this could take a little bit.");

        var jsonResponse = JObject.Parse(new WebClient().DownloadString($"https://api.popcat.xyz/lyrics?song={System.Web.HttpUtility.UrlEncode(songName.Replace(" ", "+"))}"));
        
        if (string.IsNullOrWhiteSpace(jsonResponse["lyrics"]?.ToString()))
        {
            var failEmbed = new EmbedBuilder()
                .WithTitle($"Failed to find the lyrics!")
                .WithDescription("Maybe try it again and phrase it differently?")
                .WithColor(Color.Red)
                .Build();
            await message.Channel.SendMessageAsync(null, false, failEmbed);
            await loadingMsg.DeleteAsync();
            return;
        }
        var imageEmbed = new EmbedBuilder()
            .WithTitle($"Lyrics for {jsonResponse["title"]} by {jsonResponse["artist"]}")
            .WithThumbnailUrl(jsonResponse["image"]?.ToString())
            .WithDescription(jsonResponse["lyrics"]?.ToString())
            .WithFooter($"Lyrics and image from Genius")
            .WithColor(Color.Purple)
            .Build();
        await message.Channel.SendMessageAsync(null, false, imageEmbed);
        await loadingMsg.DeleteAsync();
    }
    #pragma warning restore SYSLIB0014
}