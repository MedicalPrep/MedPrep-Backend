namespace MedPrep.Api.Models;

using System.ComponentModel.DataAnnotations;

public class SubtitleSource
{
    [Key]
    public Guid Id { get; set; }
    public string Source { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
}
