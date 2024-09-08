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

    // third party ID
    public string ThirdPartyVideoId { get; set; } = string.Empty;

    // New properties
    // New properties with foreign keys
    public Guid? NextVideoId { get; set; } // Foreign key for the next video
    public Video? NextVideo { get; set; }

    public Guid? PrevVideoId { get; set; } // Foreign key for the previous video
    public Video? PrevVideo { get; set; }

    public Guid? CourseModuleId { get; set; } // Foreign key for CourseModule
    public CourseModule? CourseModule { get; set; }

    public Guid? PlaylistId { get; set; } // Foreign key for Playlist
    public Playlist? Playlist { get; set; }
}

