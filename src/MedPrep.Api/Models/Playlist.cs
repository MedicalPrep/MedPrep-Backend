namespace MedPrep.Api.Models;

using MedPrep.Api.Models.Common;

public class Playlist : BaseEntity, ISoftDeletable
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public decimal Price { get; set; }

    // References
    public ICollection<CourseModule> Modules { get; set; } = new List<CourseModule>();

    // soft deletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
