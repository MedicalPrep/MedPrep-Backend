namespace MedPrep.Api.Services.Contracts;

public class IVideoServiceContracts
{
    public record VideoUploadCommand
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public Guid TeacherId { get; set; }

        public Guid CourseModuleId { get; set; }
    }

    public record VideoUploadResult
    {
        public string UploadEndpoint { get; set; } = string.Empty;
        public string ThirdPartyVideoCollectionId { get; set; } = string.Empty;
        public string ThirdPartyVideoId { get; set; } = string.Empty;
        public string ThirdPartyLibraryId { get; set; } = string.Empty;
        public string AuthorizationSignature { get; set; } = string.Empty;
        public long AuthorizationExpiration { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
