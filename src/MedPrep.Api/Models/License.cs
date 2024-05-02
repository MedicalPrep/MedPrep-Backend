namespace MedPrep.Api.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MedPrep.Api.Models.Common;

public class License : IBaseEntity
{
    [Key]
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

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }

    // soft deletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
