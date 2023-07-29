using System.Linq;

namespace Flare.Commands;

public static class InteractionHandler
{
    public static class CommandHandler
    {
        public static async Task HandleCommandReceive(SocketMessage message, string command)
        {
            try
            {
                switch (command.ToLower().Split(' ')[0])
                {
                    case "ping":
                        await CommandLogic.Main.PingCommand.RunCommandLogic(message);
                        break;

                    case "cat":
                        await CommandLogic.Main.CatCommand.RunCommandLogic(message);
                        break;

                    case "avatar":
                        if (message.MentionedUsers.Count != 0)
                        {
                            await CommandLogic.Main.AvatarCommand.RunCommandLogic(message,
                                message.MentionedUsers.First());
                            break;
                        }

                        await CommandLogic.Main.AvatarCommand.RunCommandLogic(message, message.Author);
                        break;
                    
                    case "dog":
                        await CommandLogic.Main.DogCommand.RunCommandLogic(message);
                        break;
                    
                    case "kick":
                        if (message.MentionedUsers.Count == 0)
                        {
                            await message.Channel.SendMessageAsync("Please specify (@mention) a user to kick.");
                            break;
                        }

                        var kickReason = "Very responsible moderator, no reason provided.";
                        if (!string.IsNullOrWhiteSpace(command.Split('>')[1]))
                        {
                            kickReason = command.Split('>')[1];
                        }

                        await CommandLogic.Moderation.KickCommand.RunCommandLogic(
                            message,
                            message.MentionedUsers.First(),
                            kickReason
                        );
                        break;
                    
                    case "ban":
                        if (message.MentionedUsers.Count == 0)
                        {
                            await message.Channel.SendMessageAsync("Please specify (@mention) a user to ban.");
                            break;
                        }

                        var banReason = "Very responsible moderator, no reason provided.";
                        if (!string.IsNullOrWhiteSpace(command.Split('>')[1]))
                        {
                            banReason = command.Split('>')[1];
                        }

                        await CommandLogic.Moderation.BanCommand.RunCommandLogic(
                            message,
                            message.MentionedUsers.First(),
                            banReason
                        );
                        break;
                    
                    case "mute":
                        if (message.MentionedUsers.Count == 0)
                        {
                            await message.Channel.SendMessageAsync("Please specify (@mention) a user to mute.");
                            break;
                        }

                        var muteDuration = "5m";
                        if (!string.IsNullOrWhiteSpace(command.Split('>')[1]))
                        {
                            muteDuration = command.Split('>')[1];
                        }

                        await CommandLogic.Moderation.MuteCommand.RunCommandLogic(
                            message,
                            message.MentionedUsers.First(),
                            muteDuration
                        );
                        break;
                    
                    case "unban":
                        if (message.Content.Length > 7 && string.IsNullOrWhiteSpace(message.Content.Split(' ')[1]))
                        {
                            await message.Channel.SendMessageAsync("Please specify the ID of a user to unban.");
                            break;
                        }

                        if (ulong.TryParse(message.Content.Split(' ')[1], out var userId))
                        {
                            await CommandLogic.Moderation.UnbanCommand.RunCommandLogic(
                                message,
                                userId
                            );
                            break;
                        }

                        await message.Channel.SendMessageAsync(
                            "Please specify the ID of a user to unban. Correct formatting is `f!unban 756164525035749529`.");
                        break;
                    
                    case "unmute":
                        if (message.MentionedUsers.Count == 0)
                        {
                            await message.Channel.SendMessageAsync("Please specify (@mention) a user to unmute.");
                            break;
                        }

                        await CommandLogic.Moderation.UnmuteCommand.RunCommandLogic(
                            message,
                            message.MentionedUsers.First()
                        );
                        break;
                    
                    case "purge":
                        if (command.Length > 6 && !string.IsNullOrWhiteSpace(message.Content.Split(' ')[1]))
                        {
                            await CommandLogic.Moderation.PurgeCommand.RunCommandLogic(message, int.Parse(message.Content.Split(' ')[1]));
                            break;
                        }

                        await CommandLogic.Moderation.PurgeCommand.RunCommandLogic(message);
                        break;
                    case "softban":
                        if (message.MentionedUsers.Count == 0)
                        {
                            await message.Channel.SendMessageAsync("Please specify (@mention) a user to softban.");
                            break;
                        }

                        var softbanReason = "Very responsible moderator, no reason provided.";
                        if (!string.IsNullOrWhiteSpace(command.Split('>')[1]))
                        {
                            softbanReason = command.Split('>')[1];
                        }

                        await CommandLogic.Moderation.SoftbanCommand.RunCommandLogic(
                            message,
                            message.MentionedUsers.First(),
                            softbanReason
                        );
                        break;
                }
            }
            catch (Exception ex)
            {
                var failEmbed = new EmbedBuilder()
                    .WithTitle($"Failed to execute command!")
                    .WithDescription($"Exception: {ex}")
                    .WithFooter($"Failed at {DateTime.Now}")
                    .WithColor(Color.Red)
                    .Build();
                await message.Channel.SendMessageAsync("", false, failEmbed);
            }
        }
    }

    public class ButtonInteractionHandler
    {
        
    }
}