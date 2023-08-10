namespace Flare
{
    public partial class MainWindow
    {
        // Flare (Prod): 
        // Flare (Development): https://discord.com/api/oauth2/authorize?client_id=1126807945599008768&permissions=8&scope=bot
        /* MAIN: Initialization */
        public MainWindow()
        {
            InitializeComponent();
            InitializeAsyncLoaders();
        }

        private async void InitializeAsyncLoaders()
        {
            await CheckIsNewCompile();
            await ClearLog();
            await InitiateDiscordAuthflow();
        }

        private async Task CheckIsNewCompile()
        {
            const string buildCfgPath = "../../../Build.flare";
            if (!File.Exists(buildCfgPath)) return;
            var rss = JObject.Parse(await File.ReadAllTextAsync(buildCfgPath));
            var buildNumber = Convert.ToInt32(rss["FlareBuildNumber"]);
            buildNumber++;
            rss["FlareBuildNumber"] = buildNumber;
            await File.WriteAllTextAsync(buildCfgPath, rss.ToString());
            //Redundant re-parse, improves code readability however
            rss = JObject.Parse(await File.ReadAllTextAsync(buildCfgPath));
            FlareBuildInformation.Content = $"{rss["FlareBuildName"]}, Version {rss["FlareBuildVersion"]}, Build {rss["FlareBuildNumber"]}.";
        }

        private static Task ClearLog()
        {
            const string logPath = "App/DiscordLog.flare";
            if (File.Exists(logPath)) { File.Delete(logPath); } 
            return Task.CompletedTask;
        }


        /* DISCORD: Authflow */
        private async Task InitiateDiscordAuthflow()
        {
            Variables.DiscordClient = new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.All | GatewayIntents.MessageContent
            });
            Variables.ClientCommandService = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Verbose,
                CaseSensitiveCommands = false,
            });
            Variables.DiscordClient.Log += Log;
            Variables.ClientCommandService.Log += Log;
            Variables.DiscordClient.MessageReceived += MessageReceivedAsync;
            Variables.DiscordClient.InteractionCreated += InteractionCreatedAsync;
            await LoginDiscord();
        }

        private static async Task LoginDiscord() 
        {
            var rss = JObject.Parse(await File.ReadAllTextAsync("App/BotConfiguration.flare"));
            await Variables.DiscordClient.LoginAsync(TokenType.Bot, rss["BotToken"]!.ToString());
            await Variables.DiscordClient.StartAsync();
            await Task.Delay(-1);
        }



        /* DISCORD: ETC */
        private async Task Log(LogMessage log)
        {
            await File.AppendAllTextAsync("App/DiscordLog.flare", log.ToString());
            // non ui thread, use invokeasync so text actually updates when called from discord.net's native logger
            await Dispatcher.InvokeAsync(() => FlareOutput.Content = log.ToString().Replace("     ", " "));
        }



        /* ETC: Navigation */
        private void TitleBarMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;
            WindowState = WindowState.Normal;
            DragMove();
        }


        private static async Task MessageReceivedAsync(SocketMessage message)
        {
            if (message.Author.Id == Variables.DiscordClient.CurrentUser.Id || message.Author.IsBot)
                return;
            if (message.Content.StartsWith(JObject.Parse(await File.ReadAllTextAsync("App/BotConfiguration.flare"))["BotPrefix"]?.ToString() ?? "f!")) { await Commands.InteractionHandler.CommandHandler.HandleCommandReceive(message, message.Content[2..]); }
        }

        // For better functionality & a more developer-friendly approach to handling any kind of interaction, refer to:
        // https://discordnet.dev/guides/int_framework/intro.html
        private static async Task InteractionCreatedAsync(SocketInteraction interaction)
        {
            // safety-casting is the best way to prevent something being cast from being null.
            // If this check does not pass, it could not be cast to said type.
            if (interaction is SocketMessageComponent component)
            {
                // Check for the ID created in the button mentioned above.
                if (component.Data.CustomId == "pingmessage-option1")
                    await interaction.RespondAsync("Thank you for clicking my button!");
                if (component.Data.CustomId == "pingmessage-option2")
                    await interaction.RespondAsync($"Consider suicide, <@!{interaction.User.Id}>.");

                else
                    Console.WriteLine("An ID has been received that has no handler!");
            }
        }

        private void MinimizeClick(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void MaximizeClick(object sender, RoutedEventArgs e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

        private void CloseClick(object sender, RoutedEventArgs e) => Close();
    }
}
