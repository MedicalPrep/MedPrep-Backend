namespace MedPrep.Api.Models;

using System.ComponentModel.DataAnnotations;
using MedPrep.Api.Models.Common;

public class VideoSource : IBaseEntity, ISoftDeletable
{
    [Key]
    public Guid Id { get; set; }
    public string Source { get; set; } = string.Empty;
    public VideoSourceType SourceType { get; set; }
    public string ThirdPartyVideoId { get; set; } = string.Empty;

    // References
    public Guid VideoId { get; set; }
    public Video Video { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
