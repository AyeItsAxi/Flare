using System.CodeDom;

namespace Flare.Commands.CommandLogic.Moderation;
using Discord.Interactions;

public class UnmuteCommand : InteractionModuleBase<SocketInteractionContext>
{
    public static async Task RunCommandLogic(SocketMessage message, SocketUser targetUser)
    {
        try
        {
            var initiatorAsSgu = (SocketGuildUser)message.Author;
            var targetUserAsSgu = (SocketGuildUser)targetUser;
            if (initiatorAsSgu.GuildPermissions.Has(GuildPermission.ManageMessages))
            {
                await targetUserAsSgu.RemoveTimeOutAsync();
                var successEmbed = new EmbedBuilder()
                    .WithTitle($"Successfully unmuted {targetUser.Username}")
                    .WithFooter($"Unmuted by {message.Author.Username}")
                    .WithColor(Color.Green)
                    .Build();
                await message.Channel.SendMessageAsync("", false, successEmbed);
            }
            
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