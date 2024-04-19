namespace MedPrep.Api.Models;

using MedPrep.Api.Models.Common;

public class VideoSource : BaseEntity
{
    public string Source { get; set; } = string.Empty;

    // References
    public Guid VideoId { get; set; }
    public Video Video { get; set; } = null!;
}
