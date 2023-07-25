using System.Globalization;

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
            
            //scuffed but works so wtvr
            TimeSpan muteDuration = duration.Substring(duration.Length - 1) switch
            {
                "m" => TimeSpan.FromMinutes(Double.Parse(duration.Split('m')[0])),
                "h" => TimeSpan.FromHours(Double.Parse(duration.Split('h')[0])),
                "d" => TimeSpan.FromDays(Double.Parse(duration.Split('d')[0])),
                "w" => TimeSpan.FromDays(Double.Parse(duration.Split('w')[0]) * 7),
                _ => TimeSpan.FromMinutes(1)
            };
            await targetUserAsSocketGuildUser.SetTimeOutAsync(muteDuration);

            var successEmbed = new EmbedBuilder()
                .WithTitle($"Successfully muted {targetUser.Username}")
                .WithFooter($"Muted by {message.Author.Username}")
                .WithColor(Color.Green)
                .Build();
            await message.Channel.SendMessageAsync("", false, successEmbed);
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