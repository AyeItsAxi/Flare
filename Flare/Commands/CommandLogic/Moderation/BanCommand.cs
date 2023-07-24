using Discord.Interactions;

namespace Flare.Commands.CommandLogic.Moderation;

public class BanCommand : InteractionModuleBase<SocketInteractionContext>
{
    public static async Task RunCommandLogic(SocketMessage message, SocketUser targetUser, string reason)
    {
        try
        {
            var memberGuildUser = (SocketGuildUser)message.Author;
            var targetUserAsSocketGuildUser = (SocketGuildUser)targetUser;
            if (!memberGuildUser.GuildPermissions.Has(GuildPermission.ManageMessages))
            {
                var responseEmbed = new EmbedBuilder()
                    .WithTitle("Missing Permissions!")
                    .WithDescription("You must have the \"MANAGE_MESSAGES\" permission in order to ban people")
                    .WithColor(Color.Red)
                    .Build();
                await message.Channel.SendMessageAsync("", false, responseEmbed);
                return;
            }

            if (targetUserAsSocketGuildUser.GuildPermissions.Has(GuildPermission.ManageMessages))
            {
                var permissionsTooHighEmbed = new EmbedBuilder()
                    .WithTitle("You do not have permission to ban that member!")
                    .WithDescription("The target user has the \"MANAGE_MESSAGES\" permission and cannot be banned.")
                    .WithColor(Color.Red)
                    .Build();
                await message.Channel.SendMessageAsync("", false, permissionsTooHighEmbed);
                return;
            }
            await targetUserAsSocketGuildUser.BanAsync(7, reason);
            var successEmbed = new EmbedBuilder()
                .WithTitle($"Successfully banned {targetUser.Username}")
                .WithFooter($"Banned by {message.Author.Username}")
                .WithColor(Color.Green)
                .Build();
            await message.Channel.SendMessageAsync("", false, successEmbed);
        }
        catch (Exception ex)
        {
            await message.Channel.SendMessageAsync("Please make sure to specify (@mention) a valid user. " + ex);
        }
    }
}