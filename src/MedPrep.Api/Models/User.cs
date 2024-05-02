namespace MedPrep.Api.Models;

using MedPrep.Api.Models.Common;
using Microsoft.EntityFrameworkCore;

[Index(nameof(Email), IsUnique = true)]
public class User : Account, IBaseEntity
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    // References
    public ICollection<Playlist> PlaylistPurchases { get; set; } = new List<Playlist>();
    public ICollection<CourseModule> CourseModulePurchases { get; set; } = new List<CourseModule>();
    public ICollection<Video> VideoPurchases { get; } = new List<Video>();
}
