namespace MedPrep.Api.Models;

using MedPrep.Api.Models.Common;

public class RefreshToken : IBaseEntity, ISoftDeletable
{
    public Guid Id { get; set; }
    public string Token { get; set; } = string.Empty;

    public DateTime ExpiresOn { get; set; }

    public DateTime? RevokedOn { get; set; }

    public bool IsExpired => DateTime.Now >= this.ExpiresOn;

    public bool IsRevoked => this.RevokedOn is not null && DateTime.Now >= this.RevokedOn;

    public bool IsActive => !this.IsRevoked && !this.IsExpired;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // References
    public Guid AccountId { get; set; }
    public Account Account { get; set; } = null!;
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
