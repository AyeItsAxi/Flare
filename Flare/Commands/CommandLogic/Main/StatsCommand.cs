namespace Flare.Commands.CommandLogic.Main;

public static class StatsCommand
{
    public static async Task RunCommandLogic(SocketMessage message)
    {
        var tempMessage = await message.Channel.SendMessageAsync("Gathering system information...");
        
        //Gets total memory size
        long memSize = 0;
        foreach (var o in new ManagementObjectSearcher(new ManagementScope(), new ObjectQuery("SELECT Capacity FROM Win32_PhysicalMemory")).Get())
        {
            var obj = (ManagementObject)o;
            var mCap = Convert.ToInt64(obj["Capacity"]);
            memSize += mCap;
        }
        memSize = (memSize / 1024) / 1024;
        
        //Gets current used memory
        var gcMemoryInfo = GC.GetGCMemoryInfo();
        var installedMemory = gcMemoryInfo.MemoryLoadBytes;
        var physicalMemory = installedMemory / 1048576.0;
        
        //Gets CPU information (Model, Clock speed)
        var cpuInfo = string.Empty;
        foreach (var o in new ManagementClass("win32_processor").GetInstances())
        {
            var mo = (ManagementObject)o;
            var name = (string)mo["Name"];
            name = name.Replace("(TM)", "").Replace("(tm)", "").Replace("(R)", "").Replace("(r)", "").Replace("(C)", "").Replace("(c)", "").Replace("    ", " ").Replace("  ", " ");

            cpuInfo = name + ", " + (string)mo["Caption"] + ", " + (string)mo["SocketDesignation"];
        }

        //Gets system uptime
        var uptime = new PerformanceCounter("System", "System Up Time");
        uptime.NextValue();
        var timeSpan = TimeSpan.Parse(TimeSpan.FromSeconds(uptime.NextValue()).ToString());
        var formattedTimeSpan =
            $"{timeSpan.Days} day{(timeSpan.Days == 1 ? "" : "s")}, {timeSpan.Hours} hour{(timeSpan.Hours == 1 ? "" : "s")}, {timeSpan.Minutes} minute{(timeSpan.Minutes == 1 ? "" : "s")}, {timeSpan.Seconds} second{(timeSpan.Seconds == 1 ? "" : "s")}";


        //Gets cpu usage
        var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", Environment.MachineName);
        cpuCounter.NextValue();
        await Task.Delay(1000);
        
        
        var statsEmbed = new EmbedBuilder()
            .WithTitle("Flare Information")
            .WithDescription($"**Flare Info**\r\n{Variables.FlareBuildVersion}\r\nCommand Amount: {Enum.GetNames(typeof(ECommandEnum)).Length - 1}\r\n\r\n**Server Info**\r\nFlare running on {Environment.MachineName} ({Environment.OSVersion})\r\nServer Uptime: {formattedTimeSpan}\r\nCPU Usage: {Math.Round(cpuCounter.NextValue())}% ({cpuInfo.Split(',')[0]})\r\nRAM Usage: {Math.Round(physicalMemory):00,000}MB / {memSize:00,000}MB\r\n*(Flare is using {Math.Round((double)Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024 )}MB)*")
            .WithColor(Color.Red)
            .Build();
        await message.Channel.SendMessageAsync(null, false, statsEmbed);
        await tempMessage.DeleteAsync();
    }
}