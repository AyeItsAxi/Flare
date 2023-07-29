using Discord.Interactions;

namespace Flare.Commands.CommandLogic.Moderation;

public class PurgeCommand : InteractionModuleBase<SocketInteractionContext>
{
    public static async Task RunCommandLogic(SocketMessage message, int limit = 10)
    {
        try
        {
            var memberGuildUser = (SocketGuildUser)message.Author;
            if (!memberGuildUser.GuildPermissions.Has(GuildPermission.ManageMessages))
            {
                var responseEmbed = new EmbedBuilder()
                    .WithTitle("Missing Permissions!")
                    .WithDescription("You must have the \"MANAGE_MESSAGES\" permission in order to purge messages.")
                    .WithColor(Color.Red)
                    .Build();
                await message.Channel.SendMessageAsync("", false, responseEmbed);
                return;
            }
            
            var messages = await message.Channel.GetMessagesAsync(limit).FlattenAsync();
            await ((ITextChannel)message.Channel).DeleteMessagesAsync(messages);
        }
        catch (ArgumentOutOfRangeException aoore)
        {
            var failEmbed = new EmbedBuilder()
                .WithTitle($"Messages must be younger than two weeks old!")
                .WithDescription($"{aoore.StackTrace}")
                .WithFooter($"Failed at {DateTime.Now}")
                .WithColor(Color.Red)
                .Build();
            await message.Channel.SendMessageAsync("", false, failEmbed);
        }
    }
}