namespace MedPrep.Api.Models.Common;

using Microsoft.AspNetCore.Identity;

public abstract class Account : IdentityUser<Guid>, IBaseEntity, ISoftDeletable
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public AccountType AccountType { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    // soft deletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
}

public enum AccountType
{
    Teacher,
    User,
}
