namespace MedPrep.Api.Models;

using MedPrep.Api.Models.Common;

public class SubtitleSource : BaseEntity
{
    public string Source { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
}
