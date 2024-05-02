namespace MedPrep.Api.Models.Common;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public abstract class Account : IdentityUser<Guid>, IBaseEntity, ISoftDeletable
{
    public AccountType AccountType { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }

    // soft deletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}

public enum AccountType
{
    Teacher,
    User,
}
