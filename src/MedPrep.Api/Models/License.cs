namespace MedPrep.Api.Models;

public class License
{
    public Guid Id { get; set; }
    public string Country { get; set; } = string.Empty;
    public string ProvinceOrState { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string LicenseName { get; set; } = string.Empty;
    public string AccreditationType { get; set; } = string.Empty;
    public string LicenseSource { get; set; } = string.Empty;

    // References
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
}
