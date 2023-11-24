namespace Flare.FTSComponents;

// bitch
// ReSharper disable once InconsistentNaming
public partial class FTSWindow
{
    public FTSWindow()
    {
        InitializeComponent();
        flareBuildInfo.Content = $"Version: {FlareBuildVersion!.Split(";special")[0]}\r\nStarted at: {DateTime.Now}";
    }

    private void hyperlink_Click(object sender, RoutedEventArgs e) => Process.Start(new ProcessStartInfo("cmd", "/c start https://www.youtube.com/watch?v=aI4OmIbkJH8") { CreateNoWindow = true });

    private async void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (TokenBox.Password.Length <= 50)
        {
            AnimationHandler.FadeIn(invalidToken, 0.2);
            await Task.Delay(5000);
            AnimationHandler.FadeOut(invalidToken, 0.2);
            return;
        }
        SelectedProfileData.BotToken = TokenBox.Password;
        Variables.ProfileConfiguration.Profile1 = SelectedProfileData;
        await File.WriteAllTextAsync("App/ProfileConfiguration.flare", JsonConvert.SerializeObject(Variables.ProfileConfiguration, Formatting.Indented));
        Close();
    }

    private void OnDragMove(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed) DragMove();
    }
}