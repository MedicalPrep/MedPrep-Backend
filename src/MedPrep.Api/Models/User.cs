namespace MedPrep.Api.Models;

using MedPrep.Api.Models.Common;
using Microsoft.EntityFrameworkCore;

[Index(nameof(Email), IsUnique = true)]
public class User : BaseEntity, ISoftDeletable
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    // References
    public ICollection<Video> VideoPurchases { get; } = new List<Video>();

    // soft deletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
