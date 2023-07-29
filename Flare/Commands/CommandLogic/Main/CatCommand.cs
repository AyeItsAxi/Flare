using System.Collections.Generic;
using System.Linq;

namespace Flare.Commands.CommandLogic.Main;

public static class CatCommand
{
    public static async Task RunCommandLogic(SocketMessage message)
    {
        var catCommandMsg = await message.Channel.SendMessageAsync("Getting a random cat image for you...");
        //this is so scuffed bruh
        //protip: dont put [{ content }], its so annoying
        //i know im stupid and theres a better way to do it but im out of time and have to go
        var jsonResponse = new WebClient().DownloadString("https://api.thecatapi.com/v1/images/search");
        jsonResponse = jsonResponse.Substring(jsonResponse.Length - (jsonResponse.Length - 1));
        jsonResponse = jsonResponse.TrimEnd(']');
        MessageBox.Show(jsonResponse);
        var imageEmbed = new EmbedBuilder()
            .WithTitle("Meow")
            .WithImageUrl(JObject.Parse(jsonResponse)["url"].ToString())
            .WithFooter($"Random cat for {message.Author.Username}")
            .WithColor(Color.Purple)
            .Build();
        await message.Channel.SendMessageAsync(null, false, imageEmbed);
        await message.Channel.DeleteMessageAsync(message);
        await message.Channel.DeleteMessageAsync(catCommandMsg);
    }
}