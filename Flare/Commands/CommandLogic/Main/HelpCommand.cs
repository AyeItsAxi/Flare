namespace Flare.Commands.CommandLogic.Main;

public static class HelpCommand
{
    public static async Task RunCommandLogic(SocketMessage message)
    {
        ECommandEnum? requestedCommand = null;
        if (message.Content.Length > 7)
        {
            var json = JObject.Parse(await File.ReadAllTextAsync("App/BotConfiguration.flare"));
            var commandAliases = json["CommandAliases"];

            foreach (var jToken in commandAliases!)
            {
                var command = (JProperty)jToken;
                var aliases = (JArray)command.Value;
                if (aliases.Any(alias => (string)alias! == message.Content.ToLower().Split(' ')[1]))
                {
                    requestedCommand = (ECommandEnum)Enum.Parse(typeof(ECommandEnum), command.Name);
                    break;
                }

                requestedCommand = ECommandEnum.None;
            }
        }
        
        Embed helpEmbed;
        var obj = JObject.Parse(await File.ReadAllTextAsync("App/BotConfiguration.flare"));
        var builder = new StringBuilder();
        switch (requestedCommand)
        {
            case ECommandEnum.Adios:
                var adiosArray = (JArray)obj["CommandAliases"]?["Adios"]!;
                foreach (var item in adiosArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Adios Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Generates an image of the adios meme with your or another user's profile picture. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl("https://vacefron.nl/api/adios?user=https://cdn.discordapp.com/avatars/1108867178662469692/fc8561326089d2c6176d7442fdfa0e88.webp?size=1024&width=0&height=230")
                    .Build();
                break;
                    
            case ECommandEnum.Avatar:
                var avatarArray = (JArray)obj["CommandAliases"]?["Avatar"]!;
                foreach (var item in avatarArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Avatar Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Gets your or another user's profile picture and sends it in chat. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Ban:
                var banArray = (JArray)obj["CommandAliases"]?["Ban"]!;
                foreach (var item in banArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Ban Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Bans a user. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Biden:
                var bidenArray = (JArray)obj["CommandAliases"]?["Biden"]!;
                foreach (var item in bidenArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Biden Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Generates an image of a Joe Biden tweet with the inputted text. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl($"https://api.popcat.xyz/biden?text={System.Web.HttpUtility.UrlEncode("Example text")}")
                    .Build();
                break;
                    
            case ECommandEnum.CarReverse:
                var carReverseArray = (JArray)obj["CommandAliases"]?["CarReverse"]!;
                foreach (var item in carReverseArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Car Reverse Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Generates an image of the car reverse meme with the inputted text. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl("https://vacefron.nl/api/carreverse?text=Example text")
                    .Build();
                break;
                    
            case ECommandEnum.Cat:
                var catArray = (JArray)obj["CommandAliases"]?["Cat"]!;
                foreach (var item in catArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Cat Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Sends a random cat image. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Dog:
                var dogArray = (JArray)obj["CommandAliases"]?["Dog"]!;
                foreach (var item in dogArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Dog Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Sends a random dog image. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Drip:
                var dripArray = (JArray)obj["CommandAliases"]?["Drip"]!;
                foreach (var item in dripArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Drip Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Generates an image of the drip meme with your or another user's profile picture. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl("https://vacefron.nl/api/drip?user=https://cdn.discordapp.com/avatars/1108867178662469692/fc8561326089d2c6176d7442fdfa0e88.webp?size=1024&width=0&height=230")
                    .Build();
                break;
                    
            case ECommandEnum.Github:
                var githubArray = (JArray)obj["CommandAliases"]?["Github"]!;
                foreach (var item in githubArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Github Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Gets the stats of a Github user's profile. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Grave:
                var graveArray = (JArray)obj["CommandAliases"]?["Grave"]!;
                foreach (var item in graveArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Grave Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Generates an image of your or another user's profile picture on a grave. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl("https://vacefron.nl/api/grave?user=https://cdn.discordapp.com/avatars/1108867178662469692/fc8561326089d2c6176d7442fdfa0e88.webp?size=1024&width=0&height=230")
                    .Build();
                break;
                    
            case ECommandEnum.Heaven:
                var heavenArray = (JArray)obj["CommandAliases"]?["Heaven"]!;
                foreach (var item in heavenArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Heaven Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Generates an image of the heaven meme with your or another user's profile picture. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl("https://vacefron.nl/api/heaven?user=https://cdn.discordapp.com/avatars/1108867178662469692/fc8561326089d2c6176d7442fdfa0e88.webp?size=1024&width=0&height=230")
                    .Build();
                break;
            
            case ECommandEnum.Help:
                var helpArray = (JArray)obj["CommandAliases"]?["Help"]!;
                foreach (var item in helpArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Help Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Sends a list of all commands and their aliases when specified. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Kick:
                var kickArray = (JArray)obj["CommandAliases"]?["Kick"]!;
                foreach (var item in kickArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Kick Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Kicks a user from the server. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Lyrics:
                var lyricsArray = (JArray)obj["CommandAliases"]?["Lyrics"]!;
                foreach (var item in lyricsArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Lyrics Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Gets the lyrics of a song. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Mute:
                var muteArray = (JArray)obj["CommandAliases"]?["Mute"]!;
                foreach (var item in muteArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Mute Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Mutes a user for a specified duration up to 4 weeks. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Ping:
                var pingArray = (JArray)obj["CommandAliases"]?["Ping"]!;
                foreach (var item in pingArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Ping Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Gets the delay between Discord and the bot. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Purge:
                var purgeArray = (JArray)obj["CommandAliases"]?["Purge"]!;
                foreach (var item in purgeArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Purge Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Deletes a specified number of messages from the channel the command was sent in. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.SadCat:
                var sadCatArray = (JArray)obj["CommandAliases"]?["SadCat"]!;
                foreach (var item in sadCatArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Sad Cat Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Generates an image of the sad cat birthday meme with the generated text. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl($"https://api.popcat.xyz/sadcat?text={System.Web.HttpUtility.UrlEncode("Example text")}")
                    .Build();
                break;
                    
            case ECommandEnum.Softban:
                var softbanArray = (JArray)obj["CommandAliases"]?["Softban"]!;
                foreach (var item in softbanArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Softban Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Bans a user and then instantly unbans them to kick them from the server and delete their messages. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
            
            case ECommandEnum.Stats:
                var statsArray = (JArray)obj["CommandAliases"]?["Stats"]!;
                foreach (var item in statsArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Stats Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Gets current stats about the server that Flare is running on and what version Flare is running. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Unban:
                var unbanArray = (JArray)obj["CommandAliases"]?["Unban"]!;
                foreach (var item in unbanArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Unban Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Unbans a user from a server based on the user's ID. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Unmute:
                var unmuteArray = (JArray)obj["CommandAliases"]?["Unmute"]!;
                foreach (var item in unmuteArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Unmute Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Unmutes a user. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Water:
                var waterArray = (JArray)obj["CommandAliases"]?["Water"]!;
                foreach (var item in waterArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Adios Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Generates an image of the desert water sign meme with inputted text to go on the sign. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl("https://vacefron.nl/api/water?text=Example%20text")
                    .Build();
                break;
                    
            case ECommandEnum.Wide:
                var wideArray = (JArray)obj["CommandAliases"]?["Wide"]!;
                foreach (var item in wideArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Wide Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Makes an image wide. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl($"https://vacefron.nl/api/wide?image=https://cdn.discordapp.com/avatars/1108867178662469692/fc8561326089d2c6176d7442fdfa0e88.webp?size=1024&width=0&height=230")
                    .Build();
                break;
                    
            case ECommandEnum.Wolverine:
                var wolverineArray = (JArray)obj["CommandAliases"]?["Wolverine"]!;
                foreach (var item in wolverineArray)
                {
                    builder.AppendLine(item.ToString());
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Wolverine Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Generates an image of the wolverine photo meme with your or another user's profile picture. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl($"https://vacefron.nl/api/wolverine?user=https://cdn.discordapp.com/avatars/1108867178662469692/fc8561326089d2c6176d7442fdfa0e88.webp?size=1024&width=0&height=230")
                    .Build();
                break;
                    
            case ECommandEnum.None:
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Command not recognized!")
                    .WithColor(Color.Red)
                    .WithDescription("Command was not recognized as a valid command.")
                    .WithFooter("Tip: use f!help CommandName to view what it does and aliases")
                    .Build();
                break;
            default:
                var sb = new StringBuilder();
                foreach (var item in Enum.GetValues(typeof(ECommandEnum)))
                {
                    sb.AppendLine(item.ToString());
                }
                sb.Replace("None", ""); // remove "none" from the list, as it is not a command, just a part of the enum for fallback cases
                helpEmbed = new EmbedBuilder()
                    .WithTitle("List of Flare commands")
                    .WithColor(Color.LightOrange)
                    .WithDescription(sb.ToString())
                    .WithFooter("Tip: use f!help commandname to view what it does and aliases")
                    .Build();
                break;
        }
        
        
        await message.Channel.SendMessageAsync("", false, helpEmbed);
    }
}