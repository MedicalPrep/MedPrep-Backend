namespace MedPrep.Api.Config;

public class BunnyStreamSettings
{
    public static string Name { get; set; } = "BunnyStreamSettings";
    public string ApiKey { get; set; } = string.Empty;
    public string LibraryId { get; set; } = string.Empty;
    public string UploadEndpoint { get; set; } = string.Empty;
}
