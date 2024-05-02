namespace MedPrep.Api.Config;

public class RedirectionConfig
{
    public static string Name { get; } = "RedirectionConfig";
    public string ConfirmationUrl { get; set; } = string.Empty;
}
