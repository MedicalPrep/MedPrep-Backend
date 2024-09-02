namespace MedPrep.Api.Models;

using System.ComponentModel.DataAnnotations;
using MedPrep.Api.Models.Common;

public class Video : IBaseEntity, ISoftDeletable
{
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<VideoSource> VideoSources { get; } = [];
    public ICollection<SubtitleSource> SubtitleSources { get; } = [];
    public ICollection<User> Purchasers { get; } = [];

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    // soft deletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    // References
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
}
