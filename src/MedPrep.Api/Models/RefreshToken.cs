namespace MedPrep.Api.Models;

using MedPrep.Api.Models.Common;

public class RefreshToken : IBaseEntity, ISoftDeletable
{
    public Guid Id { get; set; }
    public string Token { get; set; } = string.Empty;

    public DateTimeOffset ExpiresOn { get; set; }

    public DateTimeOffset? RevokedOn { get; set; }

    public bool IsExpired => DateTime.Now >= this.ExpiresOn;

    public bool IsRevoked =>
        this.RevokedOn is not null && DateTimeOffset.UtcNow >= this.RevokedOn?.ToUniversalTime();

    public bool IsActive => !this.IsRevoked && !this.IsExpired;

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    // References
    public Guid AccountId { get; set; }
    public Account Account { get; set; } = null!;
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
