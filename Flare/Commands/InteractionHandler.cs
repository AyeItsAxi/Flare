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
            }
        }
    }

    public class ButtonInteractionHandler
    {
        
    }
}