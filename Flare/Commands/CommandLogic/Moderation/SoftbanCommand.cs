namespace Flare.Commands.CommandLogic.Moderation;

public abstract class SoftbanCommand : InteractionModuleBase<SocketInteractionContext>
{
    public static async Task RunCommandLogic(SocketMessage message, SocketUser targetUser, string reason)
    {
        try
        {
            var tempMsg = await message.Channel.SendMessageAsync("Working on it...");
            var memberGuildUser = (SocketGuildUser)message.Author;
            var guild = ((SocketGuildChannel)message.Channel).Guild;
            var guildMemberList = await guild.GetUsersAsync().FlattenAsync();
            var igu = guildMemberList.FirstOrDefault(user => user.Id == targetUser.Id);
            
            if (!memberGuildUser.GuildPermissions.Has(GuildPermission.ManageMessages))
            {
                var responseEmbed = new EmbedBuilder()
                    .WithTitle("Missing Permissions!")
                    .WithDescription("You must have the \"MANAGE_MESSAGES\" permission in order to softban people")
                    .WithColor(Color.Red)
                    .Build();
                await message.Channel.SendMessageAsync("", false, responseEmbed);
                await tempMsg.DeleteAsync();
                return;
            }

            if (igu!.GuildPermissions.Has(GuildPermission.ManageMessages))
            {
                var permissionsTooHighEmbed = new EmbedBuilder()
                    .WithTitle("You do not have permission to softban that member!")
                    .WithDescription("The target user has the \"MANAGE_MESSAGES\" permission and cannot be softbanned.")
                    .WithColor(Color.Red)
                    .Build();
                await message.Channel.SendMessageAsync("", false, permissionsTooHighEmbed);
                await tempMsg.DeleteAsync();
                return;
            }
            await igu.BanAsync(7, reason);
            await Task.Delay(300);
            await guild.RemoveBanAsync(targetUser);
            var successEmbed = new EmbedBuilder()
                .WithTitle($"Successfully softbanned {targetUser.Username}")
                .WithFooter($"Softbanned by {message.Author.Username}")
                .WithColor(Color.Green)
                .Build();
            await message.Channel.SendMessageAsync("", false, successEmbed);
            await tempMsg.DeleteAsync();
        }
        catch (Exception ex)
        {
            var failEmbed = new EmbedBuilder()
                .WithTitle($"Failed to unmute {targetUser.Username}!")
                .WithDescription($"Exception: {ex}")
                .WithFooter($"Failed at {DateTime.Now}")
                .WithColor(Color.Red)
                .Build();
            await message.Channel.SendMessageAsync("", false, failEmbed);
        }
    }
}