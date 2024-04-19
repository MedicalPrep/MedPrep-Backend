namespace MedPrep.Api.Models;

using MedPrep.Api.Models.Common;

public class License : BaseEntity
{
    public string Country { get; set; } = string.Empty;
    public string ProvinceOrState { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string LicenseName { get; set; } = string.Empty;
    public string AccreditationType { get; set; } = string.Empty;
    public string LicenseSource { get; set; } = string.Empty;

    // References
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;

    // soft deletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
