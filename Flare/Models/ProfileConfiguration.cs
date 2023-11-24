namespace Flare.Models;

public class ProfileConfiguration
{
    public int SelectedProfile { get; set; }
    public ProfileData Profile1 { get; set; } = new();
    public ProfileData Profile2 { get; set; } = new();
    public ProfileData Profile3 { get; set; } = new();
}

public class ProfileData
{
    public string? BotPrefix { get; set; }
    public string? BotToken { get; set; }
    public string? StatusType { get; set; }
    public string? StatusContent { get; set; }
    public string? AccountName { get; set; }
}