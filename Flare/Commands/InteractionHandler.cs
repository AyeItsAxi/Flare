using System.Linq;

namespace Flare.Commands;

public static class InteractionHandler
{
    public static class CommandHandler
    {
        public static async Task HandleCommandReceive(SocketMessage message, string command)
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
                        await CommandLogic.Main.AvatarCommand.RunCommandLogic(message, message.MentionedUsers.First());
                        break;
                    }

                    await CommandLogic.Main.AvatarCommand.RunCommandLogic(message, message.Author);
                    break;
                case "kick":
                    if (message.MentionedUsers.Count == 0) { await message.Channel.SendMessageAsync("Please specify (@mention) a user to ban."); break; }
                    var kickReason = "Very responsible moderator, no reason provided.";
                    if (!string.IsNullOrWhiteSpace(command.Split('>')[1])) { kickReason = command.Split('>')[1]; }
                    await CommandLogic.Moderation.KickCommand.RunCommandLogic(
                        message, 
                        message.MentionedUsers.First(),
                        kickReason
                        );
                    break;
            }
        }
    }

    public class ButtonInteractionHandler
    {
        
    }
}