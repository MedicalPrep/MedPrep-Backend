namespace MedPrep.Api.Models;

using MedPrep.Api.Models.Common;

public class Teacher : BaseEntity, ISoftDeletable
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserType Type { get; set; }
    public VerficationStatus Status { get; set; }

    // References
    public ICollection<License> License { get; } = new List<License>();
    public ICollection<Video> Videos { get; } = new List<Video>();

    // soft deletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
