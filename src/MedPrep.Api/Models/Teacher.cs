namespace MedPrep.Api.Models;

using MedPrep.Api.Models.Common;

public class Teacher : Account
{
    public string ThirdPartyVideoCollectionId { get; set; } = string.Empty;
    public VerficationStatus Status { get; set; }

    // References
    public ICollection<License> License { get; } = [];
    public ICollection<Video> Videos { get; } = [];
    public ICollection<CourseModule> CourseModule { get; } = [];
    public ICollection<Playlist> Playlists { get; } = [];
}
