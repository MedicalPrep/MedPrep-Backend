namespace MedPrep.Api.Models;

using MedPrep.Api.Models.Common;

public class Teacher : Account
{
    public VerficationStatus Status { get; set; }

    // References
    public ICollection<License> License { get; } = new List<License>();
    public ICollection<Video> Videos { get; } = new List<Video>();
    public ICollection<CourseModule> CourseModule { get; } = new List<CourseModule>();
    public ICollection<Playlist> Playlists { get; } = new List<Playlist>();
}
