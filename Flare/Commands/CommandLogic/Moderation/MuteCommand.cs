namespace Flare.Commands.CommandLogic.Moderation;
using Discord.Interactions;
using Color = Discord.Color;

public class MuteCommand : InteractionModuleBase<SocketInteractionContext>
{
    public static async Task RunCommandLogic(SocketMessage message, SocketUser targetUser, string duration)
    {
        try
        {
            var memberGuildUser = (SocketGuildUser)message.Author;
            var targetUserAsSocketGuildUser = (SocketGuildUser)targetUser;
            if (!memberGuildUser.GuildPermissions.Has(GuildPermission.ManageMessages))
            {
                var responseEmbed = new EmbedBuilder()
                    .WithTitle("Missing Permissions!")
                    .WithDescription("You must have the \"MANAGE_MESSAGES\" permission in order to mute people")
                    .WithColor(Color.Red)
                    .Build();
                await message.Channel.SendMessageAsync("", false, responseEmbed);
                return;
            }

            if (targetUserAsSocketGuildUser.GuildPermissions.Has(GuildPermission.ManageMessages))
            {
                var permissionsTooHighEmbed = new EmbedBuilder()
                    .WithTitle("You do not have permission to mute that member!")
                    .WithDescription("The target user has the \"MANAGE_MESSAGES\" permission and cannot be muted.")
                    .WithColor(Color.Red)
                    .Build();
                await message.Channel.SendMessageAsync("", false, permissionsTooHighEmbed);
                return;
            }

            await targetUserAsSocketGuildUser.SetTimeOutAsync(TimeSpan.Parse(duration));

            var successEmbed = new EmbedBuilder()
                .WithTitle($"Successfully muted {targetUser.Username}")
                .WithFooter($"Muted by {message.Author.Username}")
                .WithColor(Color.Green)
                .Build();
            await message.Channel.SendMessageAsync("", false, successEmbed);
        }
        catch
        {
            await message.Channel.SendMessageAsync("Please make sure to specify (@mention) a valid user to mute and a duration. Mutes should look like `f!mute @User 5m`.");
        }
    }
}