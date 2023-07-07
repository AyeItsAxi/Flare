namespace Flare.Commands;

public static class InteractionHandler
{
    public static class CommandHandler
    {
        public static async Task HandleCommandReceive(SocketMessage message, string command)
        {
            switch (command.ToLower())
            {
                case "ping":
                    await CommandLogic.PingCommand.RunCommandLogic(message);
                    break;
            }
        }
    }

    public class ButtonInteractionHandler
    {
        
    }
}