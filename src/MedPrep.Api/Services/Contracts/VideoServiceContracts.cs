namespace MedPrep.Api.Services.Contracts;

public class IVideoServiceContracts
{
    public record VideoUploadCommand
    {
        public IList<IFormFile> Video { get; set; } = new List<IFormFile>();
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid CourseModuleId { get; set; }
    }
}
