namespace MedPrep.Api.Models;

public class Playlist
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public decimal Price { get; set; }

    // References
    public ICollection<CourseModule> Modules { get; set; } = new List<CourseModule>();
}
