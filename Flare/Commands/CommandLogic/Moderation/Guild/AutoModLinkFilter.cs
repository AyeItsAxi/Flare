using System.Text.RegularExpressions;

namespace Flare.Commands.CommandLogic.Moderation.Guild;

public static partial class AutoModLinkFilter
{
    // im only doing this one because i dont want to do the rest
    public static async Task<bool> RunModLogic(SocketMessage message)
    {
        if (!IsViolating(message)) return false;
        await message.DeleteAsync();
        return true;
    }

    public static async Task SetValue(SocketMessage message)
    {
        if (!((SocketGuildUser)message.Author).GuildPermissions.Has(GuildPermission.ManageMessages))
        {
            await message.Channel.SendMessageAsync("You must have the 'MANAGE_MESSAGES' permission to execute this command!");
            return;
        }
        
        var rss = JObject.Parse(await File.ReadAllTextAsync($"App/Guilds/{((SocketGuildChannel)message.Channel).Guild.Id}/GuildConfiguration.flare"));
        rss["AutoModLinkFilter"] = Convert.ToBoolean(message.Content.Split(' ')[1].Replace("0", "false").Replace("1", "true"));
        await File.WriteAllTextAsync($"App/Guilds/{((SocketGuildChannel)message.Channel).Guild.Id}/GuildConfiguration.flare", rss.ToString());

        await message.Channel.SendMessageAsync("Successfully set AutoModLinkFilter to " + Convert.ToBoolean(message.Content.Split(' ')[1].Replace("0", "false").Replace("1", "true")));
    }

    private static bool IsViolating(IMessage message)
    {
        return !((SocketGuildUser)message.Author).GuildPermissions.Has(GuildPermission.EmbedLinks) && MyRegex().IsMatch(message.Content);
    }
    
    [GeneratedRegex("(http|https)://[^\\s]+")]
    private static partial Regex MyRegex();
}