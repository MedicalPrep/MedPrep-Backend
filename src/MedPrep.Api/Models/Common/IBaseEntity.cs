namespace MedPrep.Api.Models.Common;

using System.ComponentModel.DataAnnotations;

public interface IBaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}
