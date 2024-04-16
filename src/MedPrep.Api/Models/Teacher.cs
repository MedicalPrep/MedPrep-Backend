namespace MedPrep.Api.Models;

using System.ComponentModel.DataAnnotations;

public class Teacher
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserType Type { get; set; }
    public VerficationStatus Status { get; set; }

    // References
    public ICollection<License> License { get; } = new List<License>();
    public ICollection<Video> Videos { get; } = new List<Video>();
}
