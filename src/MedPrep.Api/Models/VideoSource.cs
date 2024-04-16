namespace MedPrep.Api.Models;

using System.ComponentModel.DataAnnotations;

public class VideoSource
{
    [Key]
    public Guid Id { get; set; }
    public string Source { get; set; } = string.Empty;

    // References
    public Guid VideoId { get; set; }
    public Video Video { get; set; } = null!;
}
