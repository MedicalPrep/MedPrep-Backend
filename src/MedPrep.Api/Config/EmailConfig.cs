namespace MedPrep.Api.Config;

public class EmailConfig
{
    public static string Name { get; set; } = "EmailConfig";
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Password { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
}
