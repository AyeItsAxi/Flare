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
            InitializeAsyncLoaders();
        }

        private async void InitializeAsyncLoaders()
        {
            await CheckIsNewInstance();
            await CheckIsNewCompile();
            await ClearLog();
            await InitiateDiscordAuthflow();
        }

        private static Task CheckIsNewInstance()
        {
            if (!Directory.Exists("App")) return Task.CompletedTask;
            Directory.CreateDirectory("App");
            Directory.CreateDirectory("App/Guilds");
            Directory.CreateDirectory("App/Guilds/Channels");

            return Task.CompletedTask;
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
            FlareBuildVersion = $"Flare 0.1 - major={rss["FlareBuildName"]!.ToString().ToLower().Split(' ')[1]};build={rss["FlareBuildNumber"]};special=false;type=base";
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
            DiscordClient.Ready += async () => await Application.Current.Dispatcher.InvokeAsync(UpdateUiComponents);
            await LoginDiscord();
        }

        private async Task LoginDiscord()
        {
            _bIsPendingRestart = false;
            var rss = JObject.Parse(await File.ReadAllTextAsync("App/BotConfiguration.flare"));
            await DiscordClient.LoginAsync(TokenType.Bot, rss["BotToken"]!.ToString());
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
            if (message.Author.Id == DiscordClient.CurrentUser.Id || message.Author.IsBot)
                return;
            if (message.Content.StartsWith(JObject.Parse(await File.ReadAllTextAsync("App/BotConfiguration.flare"))["BotPrefix"]?.ToString() ?? "f!")) { await Commands.InteractionHandler.CommandHandler.HandleCommandReceive(message, message.Content[2..]); }
        }
        
        private static EStatusType GetStatusType()
        {
            var json = JObject.Parse(File.ReadAllText("App/BotConfiguration.flare"))["StatusType"]!.ToString();
            if (Enum.TryParse(json, out EStatusType statusType))
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
            var statusTypeFromJson = JObject.Parse(await File.ReadAllTextAsync("App/BotConfiguration.flare"))["StatusType"]!.ToString() switch
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
            var statusStr = JObject.Parse(await File.ReadAllTextAsync("App/BotConfiguration.flare"))["StatusContent"]!.ToString();
            StatusTypePreview.Text = statusTypeFromJson;
            StatusPreview.Text = statusStr;
            StatusText.Text = statusStr;
            TokenBox.Text = JObject.Parse(await File.ReadAllTextAsync("App/BotConfiguration.flare"))["BotToken"]!.ToString();
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
        }
        
        #pragma warning restore SYSLIB0014

        private void MinimizeClick(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void MaximizeClick(object sender, RoutedEventArgs e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

        private void CloseClick(object sender, RoutedEventArgs e) => Close();

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
            var json = JObject.Parse(await File.ReadAllTextAsync("App/BotConfiguration.flare"));
            json["BotToken"] = TokenBox.Password;
            await File.WriteAllTextAsync("App/BotConfiguration.flare", json.ToString());
            await DiscordClient.LogoutAsync();
            _bIsPendingRestart = true;
            await Task.Delay(500);
            await LoginDiscord();
        }

        private async void OnStatusSave(object sender, RoutedEventArgs e)
        {
            var json = JObject.Parse(await File.ReadAllTextAsync("App/BotConfiguration.flare"));
            json["StatusType"] = Enum.GetName(_statusType.GetType(), _statusType);
            json["StatusContent"] = StatusText.Text;
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
            await File.WriteAllTextAsync("App/BotConfiguration.flare", json.ToString());
        }

        private static async Task UpdateBotStatus(string content, ActivityType activityType)
        {
            await DiscordClient.SetActivityAsync(new Game(content, activityType));
        }

        private void OnStatusTextKeyDown(object sender, RoutedEventArgs e) => StatusPreview.Text = StatusText.Text;
    }
}
