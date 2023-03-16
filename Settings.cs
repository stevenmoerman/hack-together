namespace HackTogether;
public class Settings
{
    public string? ClientId { get; set; }
    public string[]? Scopes { get; set; }
    public string? To { get; set; }

    public static Settings LoadAppSettings()
    {
        // Load settings
        IConfiguration config = new ConfigurationBuilder()
            // appsettings.json is required.
            .AddJsonFile("appsettings.json", optional: false)
            // appsettings.Development.json" is optional, values override appsettings.json.
            .AddJsonFile($"appsettings.Development.json", optional: true)
            // User secrets are optional, values override both JSON files.
            .Build();

        return config.GetRequiredSection("graph").Get<Settings>() ??
            throw new Exception("Could not load app settings.");
    }
}