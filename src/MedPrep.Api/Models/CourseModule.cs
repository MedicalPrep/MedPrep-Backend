namespace MedPrep.Api.Models;

using System.ComponentModel.DataAnnotations;
using MedPrep.Api.Models.Common;

public class CourseModule : IBaseEntity, ISoftDeletable
{
    [Key]
    public Guid Id { get; set; }

    public string Currency { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Topic { get; set; } = string.Empty;
    public ModuleType Type { get; set; }

    // References
    public Guid? Collection { get; set; }
    public Playlist? Playlist { get; set; }
    public ICollection<Video> Videos { get; } = new List<Video>();
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
    public ICollection<User> Purchasers { get; } = new List<User>();

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    // soft deletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
