namespace MedPrep.Api.Config;

public class EmailConfig
{
    public static string Name { get; set; } = "EmailConfig";
    public required string Host { get; set; } = string.Empty;
    public required string Username { get; set; } = string.Empty;
    public required int Port { get; set; }
    public required string Password { get; set; } = string.Empty;
    public required string From { get; set; } = string.Empty;
}
