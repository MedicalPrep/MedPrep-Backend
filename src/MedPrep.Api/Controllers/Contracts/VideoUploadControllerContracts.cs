namespace MedPrep.Api.Controllers.Contracts;

public static class VideoUploadControllerContracts
{
    public record VideoUploadRequest(string Title, string Description, Guid CourseModuleId);

    public record VideoUploadResponse
    {
        public string UploadEndpoint { get; set; } = string.Empty;
        public string ThirdPartyVideoCollectionId { get; set; } = string.Empty;
        public string ThirdPartyVideoId { get; set; } = string.Empty;
        public string ThirdPartyLibraryId { get; set; } = string.Empty;
        public string AuthorizationSignature { get; set; } = string.Empty;
        public long AuthorizationExpiration { get; set; }
        public string Title { get; set; } = string.Empty;
    };
}
