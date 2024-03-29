﻿namespace Flare.Commands.CommandLogic.Moderation;

public abstract class UnbanCommand : InteractionModuleBase<SocketInteractionContext>
{
    public static async Task RunCommandLogic(SocketMessage message, ulong userId)
    {
        var isUserBanned = false;
        try
        {
            var memberGuildUser = (SocketGuildUser)message.Author;
            var targetUser = DiscordClient.GetUserAsync(userId).Result;
            var b = ((SocketGuildChannel)message.Channel).Guild.GetBansAsync(targetUser, Direction.Around);
            //this is so scuffed oh my god
            await foreach (var ban in b)
            {
                if (ban.Any(restban => restban.User.Id == userId))
                {
                    isUserBanned = true;
                }
            }

            if (!isUserBanned)
            {
                await message.Channel.SendMessageAsync("Specified user is not banned in this server!");
                return;
            }
            
            if (!memberGuildUser.GuildPermissions.Has(GuildPermission.ManageMessages))
            {
                var responseEmbed = new EmbedBuilder()
                    .WithTitle("Missing Permissions!")
                    .WithDescription("You must have the \"MANAGE_MESSAGES\" permission in order to unban people")
                    .WithColor(Color.Red)
                    .Build();
                await message.Channel.SendMessageAsync("", false, responseEmbed);
                return;
            }

            await memberGuildUser.Guild.RemoveBanAsync(targetUser);

            var successEmbed = new EmbedBuilder()
                .WithTitle($"Successfully unbanned {targetUser.Username}")
                .WithFooter($"Unbanned by {message.Author.Username}")
                .WithColor(Color.Green)
                .Build();
            await message.Channel.SendMessageAsync("", false, successEmbed);
        }
        catch (Exception ex)
        {
            await message.Channel.SendMessageAsync("Operation failed! " + ex);
        }
    }
}