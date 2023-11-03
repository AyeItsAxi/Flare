namespace Flare.Models;

public class ProfileConfiguration
{
    public int SelectedProfile { get; set; }
    public Profile1 Profile1 { get; set; } = new();
    public Profile2 Profile2 { get; set; } = new();
    public Profile3 Profile3 { get; set; } = new();
}

public class Profile1
{
    public string BotPrefix { get; set; }
    public string BotToken { get; set; }
    public string StatusType { get; set; }
    public string StatusContent { get; set; }
}
public class Profile2
{
    public string BotPrefix { get; set; }
    public string BotToken { get; set; }
    public string StatusType { get; set; }
    public string StatusContent { get; set; }
}
public class Profile3
{
    public string BotPrefix { get; set; }
    public string BotToken { get; set; }
    public string StatusType { get; set; }
    public string StatusContent { get; set; }
}