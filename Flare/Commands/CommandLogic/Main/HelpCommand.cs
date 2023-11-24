namespace Flare.Commands.CommandLogic.Main;

public static class HelpCommand
{
    public static async Task RunCommandLogic(SocketMessage message)
    {
        ECommandEnum? requestedCommand = null;
        if (message.Content.Length > 7) requestedCommand = InteractionHandler.CommandHandler.InterpretCommand(message.Content.Split(' ')[1]);
        
        var aliasJson = JsonConvert.DeserializeObject<CommandAliases>(Aliases)!;
        Embed helpEmbed;
        var builder = new StringBuilder();
        switch (requestedCommand)
        {
            case ECommandEnum.Adios:
                var adiosArray = aliasJson.Adios;
                foreach (var item in adiosArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Adios Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Generates an image of the adios meme with your or another user's profile picture. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl("https://vacefron.nl/api/adios?user=https://cdn.discordapp.com/avatars/1108867178662469692/fc8561326089d2c6176d7442fdfa0e88.webp?size=1024&width=0&height=230")
                    .Build();
                break;
                    
            case ECommandEnum.Avatar:
                var avatarArray = aliasJson.Avatar;
                foreach (var item in avatarArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Avatar Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Gets your or another user's profile picture and sends it in chat. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Ban:
                var banArray = aliasJson.Ban;
                foreach (var item in banArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Ban Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Bans a user. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Biden:
                var bidenArray = aliasJson.Biden;
                foreach (var item in bidenArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Biden Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Generates an image of a Joe Biden tweet with the inputted text. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl($"https://api.popcat.xyz/biden?text={System.Web.HttpUtility.UrlEncode("Example text")}")
                    .Build();
                break;
                    
            case ECommandEnum.CarReverse:
                var carReverseArray = aliasJson.CarReverse;
                foreach (var item in carReverseArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Car Reverse Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Generates an image of the car reverse meme with the inputted text. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl("https://vacefron.nl/api/carreverse?text=Example text")
                    .Build();
                break;
                    
            case ECommandEnum.Cat:
                var catArray = aliasJson.Cat;
                foreach (var item in catArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Cat Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Sends a random cat image. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Dog:
                var dogArray = aliasJson.Dog;
                foreach (var item in dogArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Dog Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Sends a random dog image. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Drip:
                var dripArray = aliasJson.Drip;
                foreach (var item in dripArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Drip Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Generates an image of the drip meme with your or another user's profile picture. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl("https://vacefron.nl/api/drip?user=https://cdn.discordapp.com/avatars/1108867178662469692/fc8561326089d2c6176d7442fdfa0e88.webp?size=1024&width=0&height=230")
                    .Build();
                break;
                    
            case ECommandEnum.Github:
                var githubArray = aliasJson.Github;
                foreach (var item in githubArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Github Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Gets the stats of a Github user's profile. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Grave:
                var graveArray = aliasJson.Grave;
                foreach (var item in graveArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Grave Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Generates an image of your or another user's profile picture on a grave. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl("https://vacefron.nl/api/grave?user=https://cdn.discordapp.com/avatars/1108867178662469692/fc8561326089d2c6176d7442fdfa0e88.webp?size=1024&width=0&height=230")
                    .Build();
                break;
                    
            case ECommandEnum.Heaven:
                var heavenArray = aliasJson.Heaven;
                foreach (var item in heavenArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Heaven Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Generates an image of the heaven meme with your or another user's profile picture. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl("https://vacefron.nl/api/heaven?user=https://cdn.discordapp.com/avatars/1108867178662469692/fc8561326089d2c6176d7442fdfa0e88.webp?size=1024&width=0&height=230")
                    .Build();
                break;
            
            case ECommandEnum.Help:
                var helpArray = aliasJson.Help;
                foreach (var item in helpArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Help Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Sends a list of all commands and their aliases when specified. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Kick:
                var kickArray = aliasJson.Kick;
                foreach (var item in kickArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Kick Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Kicks a user from the server. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Lyrics:
                var lyricsArray = aliasJson.Lyrics;
                foreach (var item in lyricsArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Lyrics Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Gets the lyrics of a song. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Mute:
                var muteArray = aliasJson.Mute;
                foreach (var item in muteArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Mute Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Mutes a user for a specified duration up to 4 weeks. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Ping:
                var pingArray = aliasJson.Ping;
                foreach (var item in pingArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Ping Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Gets the delay between Discord and the bot. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Purge:
                var purgeArray = aliasJson.Purge;
                foreach (var item in purgeArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Purge Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Deletes a specified number of messages from the channel the command was sent in. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.SadCat:
                var sadCatArray = aliasJson.SadCat;
                foreach (var item in sadCatArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Sad Cat Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Generates an image of the sad cat birthday meme with the generated text. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl($"https://api.popcat.xyz/sadcat?text={System.Web.HttpUtility.UrlEncode("Example text")}")
                    .Build();
                break;
                    
            case ECommandEnum.Softban:
                var softbanArray = aliasJson.Softban;
                foreach (var item in softbanArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Softban Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Bans a user and then instantly unbans them to kick them from the server and delete their messages. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
            
            case ECommandEnum.Stats:
                var statsArray = aliasJson.Stats;
                foreach (var item in statsArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Stats Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Gets current stats about the server that Flare is running on and what version Flare is running. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Unban:
                var unbanArray = aliasJson.Unban;
                foreach (var item in unbanArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Unban Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Unbans a user from a server based on the user's ID. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Unmute:
                var unmuteArray = aliasJson.Unmute;
                foreach (var item in unmuteArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Unmute Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Unmutes a user. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
                    
            case ECommandEnum.Water:
                var waterArray = aliasJson.Water;
                foreach (var item in waterArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Adios Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Generates an image of the desert water sign meme with inputted text to go on the sign. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl("https://vacefron.nl/api/water?text=Example%20text")
                    .Build();
                break;
                    
            case ECommandEnum.Wide:
                var wideArray = aliasJson.Wide;
                foreach (var item in wideArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Wide Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Makes an image wide. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .WithImageUrl($"https://vacefron.nl/api/wide?image=https://cdn.discordapp.com/avatars/1108867178662469692/fc8561326089d2c6176d7442fdfa0e88.webp?size=1024&width=0&height=230")
                    .Build();
                break;
                    
            case ECommandEnum.Wolverine:
                var wolverineArray = aliasJson.Wolverine;
                foreach (var item in wolverineArray)
                {
                    builder.AppendLine(item);
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
            
            case ECommandEnum.Lockdown:
                var lockdownArray = aliasJson.Lockdown;
                foreach (var item in lockdownArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("Lockdown Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Disables the everyone role's ability to type in the channel the command is ran in. {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
                    .Build();
                break;
            
            case ECommandEnum.ServerConfiguration_SetAutoModLinkFilter:
                var serverConfigurationSetAutoModLinkFilterArray = aliasJson.ServerConfiguration_SetAutoModLinkFilter;
                foreach (var item in serverConfigurationSetAutoModLinkFilterArray)
                {
                    builder.AppendLine(item);
                }
                helpEmbed = new EmbedBuilder()
                    .WithTitle("ServerConfiguration.SetAutoModLinkFilter Command")
                    .WithColor(Color.LightOrange)
                    .WithDescription($"Sets the server policy for the automatic link filter deleting policy {Environment.NewLine + Environment.NewLine}Aliases: {builder}")
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