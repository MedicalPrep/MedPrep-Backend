namespace MedPrep.Api.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MedPrep.Api.Models.Common;

public class SubtitleSource : IBaseEntity, ISoftDeletable
{
    [Key]
    public Guid Id { get; set; }

    public string Source { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;

    // References
    public Guid VideoId { get; set; }
    public Video Video { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
