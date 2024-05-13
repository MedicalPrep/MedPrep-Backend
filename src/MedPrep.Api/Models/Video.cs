namespace MedPrep.Api.Models;

using System.ComponentModel.DataAnnotations;
using MedPrep.Api.Models.Common;

public class Video : IBaseEntity, ISoftDeletable
{
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<VideoSource> VideoSources { get; } = new List<VideoSource>();
    public ICollection<SubtitleSource> SubtitleSources { get; } = new List<SubtitleSource>();
    public ICollection<User> Purchasers { get; } = new List<User>();

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    // soft deletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    // References
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
}
