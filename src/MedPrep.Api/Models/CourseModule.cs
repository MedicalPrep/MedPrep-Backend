namespace MedPrep.Api.Models;

public class CourseModule
{
    public Guid Id { get; set; }
    public string Currency { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Topic { get; set; } = string.Empty;
    public ModuleType Type { get; set; }

    // References
    public Guid? Collection { get; set; }
    public Playlist? Playlist { get; set; }
    public ICollection<Video> Videos { get; } = new List<Video>();
}
