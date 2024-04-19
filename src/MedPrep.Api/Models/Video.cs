namespace MedPrep.Api.Models;

using MedPrep.Api.Models.Common;

public class Video : BaseEntity, ISoftDeletable
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<VideoSource> VideoSources { get; } = new List<VideoSource>();
    public ICollection<SubtitleSource> SubtitleSources { get; } = new List<SubtitleSource>();
    public ICollection<User> Purchasers { get; } = new List<User>();

    // References
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;

    // soft deletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
