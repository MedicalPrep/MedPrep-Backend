namespace MedPrep.Api.Models;

using MedPrep.Api.Models.Common;
using Microsoft.EntityFrameworkCore;

[Index(nameof(Email), IsUnique = true)]
public class User : Account
{
    // References
    public ICollection<Playlist> PlaylistPurchases { get; set; } = [];
    public ICollection<CourseModule> CourseModulePurchases { get; set; } = [];
    public ICollection<Video> VideoPurchases { get; } = [];
}
