namespace MedPrep.Api.Models;

using System;
using MedPrep.Api.Models.Common;

public class SubtitleSource : BaseEntity, ISoftDeletable
{
    public string Source { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public bool IsDeleted { get ; set; }
    public DateTime? DeletedAt { get ; set; }
}
