namespace Flare.Services
{
    internal static class Variables
    {
        public static DiscordSocketClient DiscordClient = null!;
        public static CommandService ClientCommandService = null!;
        public static string? FlareBuildVersion = "Flare Alpha, Version 0.3";
        public static ProfileConfiguration ProfileConfiguration = null!;
        public static ProfileData SelectedProfileData = null!;

        public const string Aliases = """{"CommandAliases":{"Adios":["adios","adiosmeme","adiosimage"],"Avatar":["pfp","avatar","av","profilepicture"],"Ban":["ban","banuser","banmember","banaccount"],"Biden":["biden","bidenmeme","bidentweet","bidenimage"],"CarReverse":["carreverse","carmeme","reversememe","carimage","carreverseimage","reverseimage"],"Cat":["cat","meow","kitty","kitten","cato"],"Dog":["dog","woof","inferioranimal","bark"],"Drip":["drip","drippy"],"Github":["github","githubprofile","githubstats","githubprofilestats"],"Grave":["grave","gravememe"],"Heaven":["heaven","heavenmeme"],"Help":["help","bothelp","commands","allcommands","commandlist"],"Kick":["kick","kickuser","kickmember"],"Lockdown":["lockdown","lockdownchannel"],"Lyrics":["lyrics","findlyrics","getlyrics","fetchlyrics","songlyrics"],"Mute":["mute","muteuser","mutemember"],"Ping":["ping","botping","latency","delay","lag"],"Purge":["purge","purgecommand","purgechat","purgechannel"],"SadCat":["sadcat","sadcatmeme"],"ServerConfiguration_SetAutoModLinkFilter":["serverconfiguration.setautomodlinkfilter","serverconfiguration.automodlinkfilter","serverconfiguration.linkfilter","setlinkfilter","linkfilter","filterlinks"],"Softban":["softban","softbanuser","softbanmember"],"Stats":["stats","info","botstats"],"Unban":["unban","unbanuser","unbanmember"],"Unmute":["unmute","unmuteuser","unmutemember"],"Water":["water","watermeme"],"Wide":["wide","widememe","wideimage"],"Wolverine":["wolverine","wolverinememe","wolverineimage"]}}""";
    }
}
