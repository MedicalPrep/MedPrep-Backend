namespace MedPrep.Api.Models;

using System.ComponentModel.DataAnnotations;
using MedPrep.Api.Models.Common;

public class Playlist : IBaseEntity, ISoftDeletable
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public decimal Price { get; set; }

    // References
    public ICollection<CourseModule> Modules { get; set; } = new List<CourseModule>();
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    // soft deletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
