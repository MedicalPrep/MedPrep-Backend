namespace MedPrep.Api.Services.Contracts;

using MedPrep.Api.Controllers.Contracts;

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
    public record VideoRequestResponse
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid? NextVideo { get; set; }
        public Guid? PrevVideo { get; set; }
        public List<string> VideoSource { get; set; } = [];
        public List<string> SubtitleSource { get; set; } = [];
        public CourseModuleDto? CourseModule { get; set; }
        public PlaylistDto? Playlist { get; set; }
    }

    public class CourseModuleDto
    {
        public string Name { get; set; } = string.Empty;
        public Guid? Id { get; set; }
    }

    public class PlaylistDto
    {
        public string Name { get; set; } = string.Empty;
        public Guid? Id { get; set; }
    }

    public record VideoPlayResponse
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid? NextVideo { get; set; }
        public Guid? PrevVideo { get; set; }
        public string ThumbnailUrl { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public List<string> SubtitleSource { get; set; } = [];
        public PlaylistDto? Playlist { get; set; }
    }
}
