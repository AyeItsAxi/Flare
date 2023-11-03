//not declared in globals.cs cause ambiguous reference color between discord.color and whatever of these 3 libraries

using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            ApplicationLoading.Visibility = Visibility.Visible;
            InitializeAsyncLoaders();
        }

        private async void InitializeAsyncLoaders()
        {
            await CheckIsNewInstance();
            await CheckIsNewCompile();
            await ClearLog();
            await InitiateDiscordAuthflow();
        }

        private async Task CheckIsNewInstance()
        {
            if (Directory.Exists("App")) return;
            Directory.CreateDirectory("App");
            Directory.CreateDirectory("App/Guilds");
            Directory.CreateDirectory("App/Guilds/Channels");
            await File.WriteAllTextAsync("App/BotConfiguration.flare", "{\n  \"BotPrefix\": \"f!\",\n  \"BotToken\": \"\",\n  \"StatusType\": \"CUSTOM\",\n  \"StatusContent\": \"\",\n  \"CommandAliases\": {\n    \"Adios\": [\n      \"adios\",\n      \"adiosmeme\",\n      \"adiosimage\"\n    ],\n    \"Avatar\": [\n      \"pfp\",\n      \"avatar\",\n      \"av\",\n      \"profilepicture\"\n    ],\n    \"Ban\": [\n      \"ban\",\n      \"banuser\",\n      \"banmember\",\n      \"banaccount\"\n    ],\n    \"Biden\": [\n      \"biden\",\n      \"bidenmeme\",\n      \"bidentweet\",\n      \"bidenimage\"\n    ],\n    \"CarReverse\": [\n      \"carreverse\",\n      \"carmeme\",\n      \"reversememe\",\n      \"carimage\",\n      \"carreverseimage\",\n      \"reverseimage\"\n    ],\n    \"Cat\": [\n      \"cat\",\n      \"meow\",\n      \"kitty\",\n      \"kitten\",\n      \"cato\"\n    ],\n    \"Dog\": [\n      \"dog\",\n      \"woof\",\n      \"inferioranimal\",\n      \"bark\"\n    ],\n    \"Drip\": [\n      \"drip\",\n      \"drippy\"\n    ],\n    \"Github\": [\n      \"github\",\n      \"githubprofile\",\n      \"githubstats\",\n      \"githubprofilestats\"\n    ],\n    \"Grave\": [\n      \"grave\",\n      \"gravememe\"\n    ],\n    \"Heaven\": [\n      \"heaven\",\n      \"heavenmeme\"\n    ],\n    \"Help\": [\n      \"help\",\n      \"bothelp\",\n      \"commands\",\n      \"allcommands\",\n      \"commandlist\"\n    ],\n    \"Kick\": [\n      \"kick\",\n      \"kickuser\",\n      \"kickmember\"\n    ],\n    \"Lockdown\": [\n      \"lockdown\",\n      \"lockdownchannel\"\n    ],\n    \"Lyrics\": [\n      \"lyrics\",\n      \"findlyrics\",\n      \"getlyrics\",\n      \"fetchlyrics\",\n      \"songlyrics\"\n    ],\n    \"Mute\": [\n      \"mute\",\n      \"muteuser\",\n      \"mutemember\"\n    ],\n    \"Ping\": [\n      \"ping\",\n      \"botping\",\n      \"latency\",\n      \"delay\",\n      \"lag\"\n    ],\n    \"Purge\": [\n      \"purge\",\n      \"purgecommand\",\n      \"purgechat\",\n      \"purgechannel\"\n    ],\n    \"SadCat\": [\n      \"sadcat\",\n      \"sadcatmeme\"\n    ],\n    \"ServerConfiguration_SetAutoModLinkFilter\": [\n      \"serverconfiguration.setautomodlinkfilter\",\n      \"serverconfiguration.automodlinkfilter\",\n      \"serverconfiguration.linkfilter\",\n      \"setlinkfilter\",\n      \"linkfilter\",\n      \"filterlinks\"\n    ],\n    \"Softban\": [\n      \"softban\",\n      \"softbanuser\",\n      \"softbanmember\"\n    ],\n    \"Stats\": [\n      \"stats\",\n      \"info\",\n      \"botstats\"\n    ],\n    \"Unban\": [\n      \"unban\",\n      \"unbanuser\",\n      \"unbanmember\"\n    ],\n    \"Unmute\": [\n      \"unmute\",\n      \"unmuteuser\",\n      \"unmutemember\"\n    ],\n    \"Water\": [\n      \"water\",\n      \"watermeme\"\n    ],\n    \"Wide\": [\n      \"wide\",\n      \"widememe\",\n      \"wideimage\"\n    ],\n    \"Wolverine\": [\n      \"wolverine\",\n      \"wolverinememe\",\n      \"wolverineimage\"\n    ]\n  }\n}");
            json = JsonConvert.DeserializeObject<BotConfiguration.Root>(await File.ReadAllTextAsync("App/BotConfiguration.flare"))!;
            //PfConfig = JsonConvert.DeserializeObject<ProfileConfiguration>(await File.ReadAllTextAsync("App/ProfileConfiguration.flare"))!;
            await RunFlareFirstTimeSetup();
        }

        private async Task CheckIsNewCompile()
        {
            const string buildCfgPath = "../../../Build.flare";
            if (!File.Exists("App/Build.flare") && !File.Exists(buildCfgPath)) return;
            var rss = JObject.Parse(await File.ReadAllTextAsync(buildCfgPath));
            var buildNumber = Convert.ToInt32(rss["FlareBuildNumber"]);
            buildNumber++;
            rss["FlareBuildNumber"] = buildNumber;
            await File.WriteAllTextAsync(buildCfgPath, rss.ToString());
            //Redundant re-parse, improves code readability however
            rss = JObject.Parse(await File.ReadAllTextAsync(buildCfgPath));
            FlareBuildVersion = $"Flare {rss["FlareBuildVersion"]} - major={rss["FlareBuildName"]!.ToString().ToLower().Split(' ')[1]};build={rss["FlareBuildNumber"]};special={rss["Special"]};type={rss["Type"]}";
            FlareBuildInformation.Content = $"{rss["FlareBuildName"]}, Version {rss["FlareBuildVersion"]}, Build {rss["FlareBuildNumber"]}.";
            if (!File.Exists(buildCfgPath)) return;
            File.Copy(buildCfgPath, "App/Build.flare", true);
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
            json = JsonConvert.DeserializeObject<BotConfiguration.Root>(await File.ReadAllTextAsync("App/BotConfiguration.flare"))!;
            DiscordClient = new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.All | GatewayIntents.MessageContent
            });
            ClientCommandService = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Debug,
                CaseSensitiveCommands = false,
            });
            DiscordClient.Log += Log;
            ClientCommandService.Log += Log;
            DiscordClient.MessageReceived += MessageReceivedAsync;
            DiscordClient.Ready += async () => await Application.Current.Dispatcher.InvokeAsync(OnClientReady);
            await LoginDiscord();
        }

        private async Task LoginDiscord()
        {
            _bIsPendingRestart = false;
            json = JsonConvert.DeserializeObject<BotConfiguration.Root>(await File.ReadAllTextAsync("App/BotConfiguration.flare"))!;
            await DiscordClient.LoginAsync(TokenType.Bot, json.BotToken);
            await DiscordClient.StartAsync();
            while (!_bIsPendingRestart)
            {
                await Task.Delay(-1);
            }
        }



        /* DISCORD: ETC */
        private async Task Log(LogMessage log)
        {
            await File.AppendAllTextAsync("App/DiscordLog.flare", log.ToString());
            // scuffed way of doing it because Discord.Net doesnt have any native way of executing code on error
            if (log.ToString().Contains("Disconnected")) await Dispatcher.InvokeAsync(OnClientDisconnected);
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
            if (message.Author.Id == DiscordClient.CurrentUser.Id || message.Author.IsBot)
                return;
            await Commands.InteractionHandler.CommandHandler.MessagePrefilter(message);
        }
        
        private static EStatusType GetStatusType()
        {
            if (Enum.TryParse(json.StatusType, out EStatusType statusType))
            {
                return statusType;
            }
            throw new ArgumentException("Invalid status type", nameof(json));
        }
        
        private static BitmapImage BitmapConvertConvert(System.Drawing.Image src)
        {
            var ms = new MemoryStream();
            src.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            var image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        
        #pragma warning disable SYSLIB0014

        private async Task OnClientReady()
        {
            foreach (var guild in DiscordClient.Guilds)
            {
                if (File.Exists($"App/Guilds/{guild.Id.ToString()}/GuildConfiguration.flare")) continue;
                Directory.CreateDirectory($"App/Guilds/{guild.Id.ToString()}");
                await File.WriteAllTextAsync($"App/Guilds/{guild.Id.ToString()}/GuildConfiguration.flare", JsonConvert.SerializeObject(new GuildConfiguration
                {
                    AutoModLinkFilter = false
                }));
            }
            await UpdateUiComponents();
        }
        
        private async Task OnClientDisconnected()//Exception exception)
        {
            if (json.BotToken.Length > 50) return; // connection errors should only ever be caused by an invalid token. if the token is valid, then the error is on discord's end.
            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("Flare Control Panel")
                .AddText("Something has goofed.")
                .AddText("Please go through the First Time Setup once more.")
                .Show();
            await DiscordClient.LogoutAsync();
            await RunFlareFirstTimeSetup();
            _bIsPendingRestart = true;
            await Task.Delay(500);
            await LoginDiscord();
        }
        
        private async Task UpdateUiComponents()
        {
            Bitmap bitmap;
            using (var ms = new MemoryStream(await new WebClient().DownloadDataTaskAsync(DiscordClient.CurrentUser.GetAvatarUrl())))
            {
                var image = System.Drawing.Image.FromStream(ms);
                bitmap = new Bitmap(image);
            }
            AvatarImageBrush.ImageSource = BitmapConvertConvert(bitmap);
            var statusType = GetStatusType();
            object selectedDdb = statusType switch
            {
                EStatusType.CUSTOM => CustomStatus,
                EStatusType.PLAYING => PlayingStatus,
                EStatusType.WATCHING => WatchingStatus,
                EStatusType.STREAMING => StreamingStatus,
                EStatusType.COMPETING => CompetingStatus,
                EStatusType.LISTENING => ListeningStatus,
                _ => CustomStatus
            };
            var statusTypeFromJson = json.StatusType switch
            {
                "PLAYING" => "Playing ",
                "STREAMING" => "Streaming ",
                "LISTENING" => "Listening to ",
                "WATCHING" => "Watching ",
                "COMPETING" => "Competing in ",
                "CUSTOM" => "",
                _ => ""
            };
            StatusSelect.SelectedItem = selectedDdb;
            BotUserName.Text = DiscordClient.CurrentUser.Username;
            var statusStr = json.StatusContent;
            StatusTypePreview.Text = statusTypeFromJson;
            StatusPreview.Text = statusStr;
            StatusText.Text = statusStr;
            TokenBox.Text = json.BotToken;
            var activityType = statusType switch
            {
                EStatusType.PLAYING => ActivityType.Playing,
                EStatusType.STREAMING => ActivityType.Streaming,
                EStatusType.LISTENING => ActivityType.Listening,
                EStatusType.WATCHING => ActivityType.Watching,
                EStatusType.COMPETING => ActivityType.Competing,
                EStatusType.CUSTOM => ActivityType.CustomStatus,
                _ => ActivityType.CustomStatus
            };
            await UpdateBotStatus(StatusText.Text, activityType);
            BotStatusEllipse.Fill = (System.Windows.Media.Brush)new BrushConverter().ConvertFromString("#30a65c")!;
            AnimationHandler.FadeOut(ApplicationLoading, 0.3);
            await Task.Delay(300);
            ApplicationLoading.Visibility = Visibility.Hidden;
        }
        
        #pragma warning restore SYSLIB0014

        private void MinimizeClick(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void MaximizeClick(object sender, RoutedEventArgs e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

        private void CloseClick(object sender, RoutedEventArgs e) => Environment.Exit(1738);

        private int _iLoadCrashPrevention;

        private EStatusType _statusType;

        private bool _bIsPendingRestart;

        private void StatusSelect_OnSelected(object sender, RoutedEventArgs e)
        {
            if (_iLoadCrashPrevention == 0) {_iLoadCrashPrevention++; return;}
            var selectedItem = StatusSelect.SelectedIndex switch
            {
                0 => "Playing ",
                1 => "Streaming ",
                2 => "Listening to ",
                3 => "Watching ",
                4 => "Competing in ",
                5 => "",
                _ => ""
            };
            var enumMember = StatusSelect.SelectedIndex switch
            {
                0 => EStatusType.PLAYING,
                1 => EStatusType.STREAMING,
                2 => EStatusType.LISTENING,
                3 => EStatusType.WATCHING,
                4 => EStatusType.COMPETING,
                5 => EStatusType.CUSTOM,
                _ => EStatusType.CUSTOM
            };
            _statusType = enumMember;
            StatusTypePreview.Text = selectedItem;
        }

        private async void OnTokenSave(object sender, RoutedEventArgs e)
        {
            json.BotToken = TokenBox.Password;
            await File.WriteAllTextAsync("App/BotConfiguration.flare", JsonConvert.SerializeObject(json));
            AnimationHandler.FadeIn(ApplicationLoading, 0.3);
            await DiscordClient.LogoutAsync();
            _bIsPendingRestart = true;
            await Task.Delay(500);
            await LoginDiscord();
        }

        private async void OnStatusSave(object sender, RoutedEventArgs e)
        {
            json.StatusType = Enum.GetName(_statusType.GetType(), _statusType);
            json.StatusContent = StatusText.Text;
            var activityType = _statusType switch
            {
                EStatusType.PLAYING => ActivityType.Playing,
                EStatusType.STREAMING => ActivityType.Streaming,
                EStatusType.LISTENING => ActivityType.Listening,
                EStatusType.WATCHING => ActivityType.Watching,
                EStatusType.COMPETING => ActivityType.Competing,
                EStatusType.CUSTOM => ActivityType.CustomStatus,
                _ => ActivityType.CustomStatus
            };
            await UpdateBotStatus(StatusText.Text, activityType);
            await File.WriteAllTextAsync("App/BotConfiguration.flare", JsonConvert.SerializeObject(json));
        }

        private Task RunFlareFirstTimeSetup()
        {
            Visibility = Visibility.Hidden;
            var ftsWin = new FTSComponents.FTSWindow();
            ftsWin.ShowDialog();
            Visibility = Visibility.Visible;
            return Task.CompletedTask;
        }

        private static async Task UpdateBotStatus(string content, ActivityType activityType) => await DiscordClient.SetActivityAsync(new Game(content, activityType));

        private void OnStatusTextKeyDown(object sender, RoutedEventArgs e) => StatusPreview.Text = StatusText.Text;

        private void OnConfigurationSave(object sender, RoutedEventArgs e)
        {
            if (StatusText.Text != json.StatusContent || StatusSelect.SelectedIndex != (int)GetStatusType())
            {
                OnStatusSave(sender, e);
            }
        }
    }
}
