namespace MedPrep.Api.Controllers.Contracts;

public static class VideoUploadControllerContracts
{
    public record VideoUploadRequest(string Title, string Description, Guid CourseModuleId);
}
