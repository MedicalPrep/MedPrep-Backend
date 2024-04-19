namespace MedPrep.Api.Models;

using MedPrep.Api.Models.Common;

public class CourseModule : BaseEntity, ISoftDeletable
{
    public string Currency { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Topic { get; set; } = string.Empty;
    public ModuleType Type { get; set; }

    // References
    public Guid? Collection { get; set; }
    public Playlist? Playlist { get; set; }
    public ICollection<Video> Videos { get; } = new List<Video>();

    // soft deletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
