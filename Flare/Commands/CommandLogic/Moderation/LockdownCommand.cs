namespace Flare.Commands.CommandLogic.Moderation;

public static class LockdownCommand
{
    public static async Task RunCommandLogic(SocketMessage message)
    {
        try
        {
            var memberGuildUser = (SocketGuildUser)message.Author;
            var channel = (SocketGuildChannel)message.Channel;
            var guild = channel.Guild;
            var channelPerms = channel.GetPermissionOverwrite(guild.EveryoneRole);
            if (memberGuildUser.GuildPermissions.Has(GuildPermission.ManageMessages))
            {
                bool isLocking;
                if (channelPerms is { SendMessages: PermValue.Allow })
                {
                    await channel.AddPermissionOverwriteAsync(guild.EveryoneRole,
                        new OverwritePermissions(sendMessages: PermValue.Deny));
                    isLocking = true;
                }
                else
                {
                    await channel.AddPermissionOverwriteAsync(guild.EveryoneRole,
                        new OverwritePermissions(sendMessages: PermValue.Allow));
                    isLocking = false;
                }
                
                var successEmbed = new EmbedBuilder()
                    .WithTitle(isLocking ? "Successfully locked channel!" : "Successfully unlocked channel!")
                    .WithColor(isLocking ? Color.Red : Color.Green)
                    .Build();
                await message.Channel.SendMessageAsync("", false, successEmbed);
                return;
            }

            var responseEmbed = new EmbedBuilder()
                .WithTitle("Missing Permissions!")
                .WithDescription("You must have the \"MANAGE_MESSAGES\" permission in order to purge messages.")
                .WithColor(Color.Red)
                .Build();
            await message.Channel.SendMessageAsync("", false, responseEmbed);
        }
        catch (Exception ex)
        {
            var failEmbed = new EmbedBuilder()
                .WithTitle($"Failed to lockdown channel!")
                .WithDescription($"Exception: {ex}")
                .WithFooter($"Failed at {DateTime.Now}")
                .WithColor(Color.Red)
                .Build();
            await message.Channel.SendMessageAsync("", false, failEmbed);
        }
    }
}