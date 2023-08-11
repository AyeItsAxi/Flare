using System.Linq;

namespace Flare.Commands.CommandLogic.Moderation;
using Discord.Interactions;
using Color = Color;

public class MuteCommand : InteractionModuleBase<SocketInteractionContext>
{
    public static async Task RunCommandLogic(SocketMessage message, SocketUser targetUser, string duration)
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
                    .WithDescription("You must have the \"MANAGE_MESSAGES\" permission in order to mute people")
                    .WithColor(Color.Red)
                    .Build();
                await message.Channel.SendMessageAsync("", false, responseEmbed);
                await tempMsg.DeleteAsync();
                return;
            }

            if (igu!.GuildPermissions.Has(GuildPermission.ManageMessages))
            {
                var permissionsTooHighEmbed = new EmbedBuilder()
                    .WithTitle("You do not have permission to mute that member!")
                    .WithDescription("The target user has the \"MANAGE_MESSAGES\" permission and cannot be muted.")
                    .WithColor(Color.Red)
                    .Build();
                await message.Channel.SendMessageAsync("", false, permissionsTooHighEmbed);
                await tempMsg.DeleteAsync();
                return;
            }
            
            //scuffed but works so wtvr
            var muteDuration = duration[^1..] switch
            {
                "m" => TimeSpan.FromMinutes(double.Parse(duration.Split('m')[0])),
                "h" => TimeSpan.FromHours(double.Parse(duration.Split('h')[0])),
                "d" => TimeSpan.FromDays(double.Parse(duration.Split('d')[0])),
                "w" => TimeSpan.FromDays(double.Parse(duration.Split('w')[0]) * 7),
                _ => TimeSpan.FromMinutes(1)
            };
            await igu.SetTimeOutAsync(muteDuration);

            var successEmbed = new EmbedBuilder()
                .WithTitle($"Successfully muted {targetUser.Username}")
                .WithFooter($"Muted by {message.Author.Username}")
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