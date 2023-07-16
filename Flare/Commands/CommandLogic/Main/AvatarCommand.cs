namespace Flare.Commands.CommandLogic.Main;

public static class AvatarCommand
{
    public static async Task RunCommandLogic(SocketMessage message, SocketUser targetUser)
    {
        try
        {
            Embed avatarEmbed = new EmbedBuilder()
                .WithTitle($"{targetUser.Username}'s avatar")
                .WithImageUrl(targetUser.GetAvatarUrl(ImageFormat.WebP, 4096))
                .WithColor(Color.LightOrange)
                .Build();
            await message.Channel.SendMessageAsync("", false, avatarEmbed);
        }
        catch
        {
            await message.Channel.SendMessageAsync(
                "Please make sure to specify a valid user or leave it empty to get your avatar.");
        }
    }
}