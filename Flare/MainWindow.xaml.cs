using System;
using System.IO;
using System.Windows;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Flare
{
    public partial class MainWindow : Window
    {
        /* MAIN: Initialization */
        public MainWindow()
        {
            InitializeComponent();
            InitializeAsyncLoaders();
        }

        private async void InitializeAsyncLoaders()
        {
            await CheckIsNewCompile();
        }

        private async Task<Task> CheckIsNewCompile()
        {
            string BuildCFGPath = "../../../Build.flare";
            if (File.Exists(BuildCFGPath))
            {
                JObject rss = JObject.Parse(await File.ReadAllTextAsync(BuildCFGPath));
                int BuildNumber = Convert.ToInt32(rss["FlareBuildNumber"]);
                BuildNumber++;
                rss["FlareBuildNumber"] = BuildNumber;
                await File.WriteAllTextAsync(BuildCFGPath, rss.ToString());
                //Redundant re-parse, improves code readability however
                rss = JObject.Parse(await File.ReadAllTextAsync(BuildCFGPath));
                FlareBuildInformation.Content = $"{rss["FlareBuildName"]}, Version {rss["FlareBuildVersion"]}, Build {rss["FlareBuildNumber"]}.";
            }
            return Task.CompletedTask;
        }


        /* ETC: Navigation */
        private void TitleBarMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;
            this.WindowState = WindowState.Normal;
            this.DragMove();
        }

        private void MinimizeClick(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;

        private void MaximizeClick(object sender, RoutedEventArgs e) => this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

        private void CloseClick(object sender, RoutedEventArgs e) => this.Close();
    }
}
