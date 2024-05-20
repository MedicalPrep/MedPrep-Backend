namespace MedPrep.Api.Controllers.Contracts;

public static class VideoUploadControllerContracts
{
    public record VideoUploadRequest(
        IList<IFormFile> Video,
        string Title,
        string Description,
        Guid CourseModuleId
    );
}
