using Newtonsoft.Json;

namespace Flare.Commands;

public static class InteractionHandler
{
    public static class CommandHandler
    {
        private static ECommandEnum InterpretCommand(IMessage message)
        {
            var json = JObject.Parse(File.ReadAllText("App/BotConfiguration.flare"));
            var commandAliases = json["CommandAliases"];

            foreach (var jToken in commandAliases!)
            {
                var command = (JProperty)jToken;
                var aliases = (JArray)command.Value;
                if (aliases.Any(alias => (string)alias! == message.Content.ToLower().Split('!')[1].Split(' ')[0]))
                {
                    return (ECommandEnum)Enum.Parse(typeof(ECommandEnum), command.Name);
                }
            }
            
            
            MessageBox.Show(message.Content.ToLower().Split('!')[1].Split(' ')[0].Replace('.', '_'));

            return ECommandEnum.None;
        }
        public static async Task HandleCommandReceive(SocketMessage message, string command)
        {
            try
            {
                var avatarUrl = message.MentionedUsers.Count != 0 ? message.MentionedUsers.First().GetAvatarUrl(ImageFormat.WebP, 4096) : message.Author.GetAvatarUrl(ImageFormat.WebP, 4096);
                switch (InterpretCommand(message))
                {
                    case ECommandEnum.Adios:
                        await CommandLogic.Main.AdiosCommand.RunCommandLogic(message, avatarUrl);
                        break;
                    
                    case ECommandEnum.Avatar:
                        if (message.MentionedUsers.Count != 0)
                        {
                            await CommandLogic.Main.AvatarCommand.RunCommandLogic(message,
                                message.MentionedUsers.First());
                            break;
                        }

                        await CommandLogic.Main.AvatarCommand.RunCommandLogic(message, message.Author);
                        break;
                    
                    case ECommandEnum.Ban:
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
                    
                    case ECommandEnum.Biden:
                        if (message.Content.Length > 8 && !string.IsNullOrWhiteSpace(message.Content.Split(' ')[1]))
                        {
                            await CommandLogic.Main.BidenCommand.RunCommandLogic(message, message.Content[(message.Content.IndexOf(' ') + 1)..]);
                            break;
                        }

                        await message.Channel.SendMessageAsync("Please attach text to process the image.");
                        break;
                    
                    case ECommandEnum.CarReverse:
                        if (message.Content.Length > 13 && !string.IsNullOrWhiteSpace(message.Content.Split(' ')[1]))
                        {
                            await CommandLogic.Main.CarReverseCommand.RunCommandLogic(message, message.Content[(message.Content.IndexOf(' ') + 1)..]);
                            break;
                        }

                        await message.Channel.SendMessageAsync("Please attach text to the command to process the image.");
                        break;
                    
                    case ECommandEnum.Cat:
                        await CommandLogic.Main.CatCommand.RunCommandLogic(message);
                        break;
                    
                    case ECommandEnum.Dog:
                        await CommandLogic.Main.DogCommand.RunCommandLogic(message);
                        break;
                    
                    case ECommandEnum.Drip:
                        await CommandLogic.Main.DripCommand.RunCommandLogic(message, avatarUrl);
                        break;
                    
                    case ECommandEnum.Github:
                        if (message.Content.Length > 9 && !string.IsNullOrWhiteSpace(message.Content.Split(' ')[1]))
                        {
                            await CommandLogic.Main.GithubCommand.RunCommandLogic(message, message.Content[(message.Content.IndexOf(' ') + 1)..]);
                            break;
                        }

                        await message.Channel.SendMessageAsync("Please specify a Github username to get stats for.");
                        break;
                    
                    case ECommandEnum.Grave:
                        await CommandLogic.Main.GraveCommand.RunCommandLogic(message, avatarUrl);
                        break;
                    
                    case ECommandEnum.Heaven:
                        await CommandLogic.Main.HeavenCommand.RunCommandLogic(message, avatarUrl);
                        break;
                    
                    case ECommandEnum.Help:
                        await CommandLogic.Main.HelpCommand.RunCommandLogic(message);
                        break;
                    
                    case ECommandEnum.Kick:
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
                    
                    case ECommandEnum.Lockdown:
                        await CommandLogic.Moderation.LockdownCommand.RunCommandLogic(message);
                        break;
                    
                    case ECommandEnum.Lyrics:
                        if (message.Content.Length > 9 && !string.IsNullOrWhiteSpace(message.Content.Split(' ')[1]))
                        {
                            await CommandLogic.Main.LyricsCommand.RunCommandLogic(message, message.Content[(message.Content.IndexOf(' ') + 1)..]);
                            break;
                        }

                        await message.Channel.SendMessageAsync("Please specify a song to find the lyrics of.");
                        break;
                    
                    case ECommandEnum.Mute:
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
                    
                    case ECommandEnum.Ping:
                        await CommandLogic.Main.PingCommand.RunCommandLogic(message);
                        //DONOTSHIP: testing: var sgc = (SocketGuildChannel)message.Channel;
                        //await sgc.AddPermissionOverwriteAsync(sgc.Guild.EveryoneRole, OverwritePermissions.DenyAll(message.Channel));
                        break;
                    
                    case ECommandEnum.Purge:
                        if (command.Length > 6 && !string.IsNullOrWhiteSpace(message.Content.Split(' ')[1]))
                        {
                            await CommandLogic.Moderation.PurgeCommand.RunCommandLogic(message, int.Parse(message.Content.Split(' ')[1]));
                            break;
                        }

                        await CommandLogic.Moderation.PurgeCommand.RunCommandLogic(message);
                        break;
                    
                    case ECommandEnum.SadCat:
                        if (message.Content.Length > 9 && !string.IsNullOrWhiteSpace(message.Content.Split(' ')[1]))
                        {
                            await CommandLogic.Main.SadCatCommand.RunCommandLogic(message, message.Content[(message.Content.IndexOf(' ') + 1)..]);
                            break;
                        }

                        await message.Channel.SendMessageAsync("Please attach text to process the image.");
                        break;
                    
                    case ECommandEnum.ServerConfiguration_SetAutoModLinkFilter:
                        await CommandLogic.Moderation.Guild.AutoModLinkFilter.SetValue(message);
                        break;
                    
                    case ECommandEnum.Softban:
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
                    
                    case ECommandEnum.Stats:
                        await CommandLogic.Main.StatsCommand.RunCommandLogic(message);
                        break;
                    
                    case ECommandEnum.Unban:
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
                    
                    case ECommandEnum.Unmute:
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
                    
                    case ECommandEnum.Water:
                        if (message.Content.Length > 8 && !string.IsNullOrWhiteSpace(message.Content.Split(' ')[1]))
                        {
                            await CommandLogic.Main.WaterCommand.RunCommandLogic(message, message.Content[(message.Content.IndexOf(' ') + 1)..]);
                            break;
                        }

                        await message.Channel.SendMessageAsync("Please attach text to the command to process the image.");
                        break;
                    
                    case ECommandEnum.Wide:
                        if (message.Attachments.Count == 0)
                        {
                            await message.Channel.SendMessageAsync(
                                "Please send an image with the command to process the image.");
                            break;
                        }
                        
                        await CommandLogic.Main.WideCommand.RunCommandLogic(message, message.Attachments.First().Url);
                        break;
                    
                    case ECommandEnum.Wolverine:
                        await CommandLogic.Main.WolverineCommand.RunCommandLogic(message, avatarUrl);
                        break;
                    
                    case ECommandEnum.None:
                        await message.Channel.SendMessageAsync(
                            $"Command \"{command}\" not recognized. Please use f!help to view all commands.");
                        break;
                    
                    default:
                        await message.Channel.SendMessageAsync(
                            $"Command \"{command}\" not recognized. Please use f!help to view all commands.");
                        break;
                }
            }
            catch (Exception ex)
            {
                var failEmbed = new EmbedBuilder()
                    .WithTitle("Failed to execute command!")
                    .WithDescription($"Exception: {ex}")
                    .WithFooter($"Failed at {DateTime.Now}")
                    .WithColor(Color.Red)
                    .Build();
                await message.Channel.SendMessageAsync("", false, failEmbed);
            }
        }

        public static async Task MessagePrefilter(SocketMessage message)
        {
            var passesLinkPrefilter = true;
            var serverConfiguration = JsonConvert.DeserializeObject<GuildConfiguration>(await File.ReadAllTextAsync($"App/Guilds/{((SocketGuildChannel)message.Channel).Guild.Id.ToString()}/GuildConfiguration.flare"))!;
            // literally only runs if i add this stupid console.writeline
            if (serverConfiguration.AutoModLinkFilter == true) Console.WriteLine("Link prefilter patch"); passesLinkPrefilter = !await CommandLogic.Moderation.Guild.AutoModLinkFilter.RunModLogic(message);
            if (passesLinkPrefilter && message.Content.StartsWith(JObject.Parse(await File.ReadAllTextAsync("App/BotConfiguration.flare"))["BotPrefix"]?.ToString() ?? "f!")) await HandleCommandReceive(message, message.Content[2..]);
            //scuffed but works so yeah
        }
    }

    public class ButtonInteractionHandler
    {
        
    }
}