namespace MedPrep.Api.Services;

using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MedPrep.Api.Config;
using MedPrep.Api.Exceptions;
using MedPrep.Api.HttpClients;
using MedPrep.Api.Models;
using MedPrep.Api.Repositories.Common;
using MedPrep.Api.Services.Common;
using Microsoft.Extensions.Options;
using static MedPrep.Api.Services.Contracts.IVideoServiceContracts;

public class BunnyVideoService(
    ITeacherRepository teacherRepository,
    IVideoRepository videoRepository,
    IUnitOfWork unitOfWork,
    BunnyStreamHttpClient bunnyHttpClient,
    IOptions<BunnyStreamSettings> bunnySettings
) : IVideoService
{
    private readonly ITeacherRepository teacherRepository = teacherRepository;
    private readonly IVideoRepository videoRepository = videoRepository;
    private readonly BunnyStreamHttpClient bunnyHttpClient = bunnyHttpClient;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly BunnyStreamSettings bunnySettings = bunnySettings.Value;

    public async Task<VideoUploadResult> UploadVideo(VideoUploadCommand command)
    {

        var teacher =
            await this.teacherRepository.GetbyIdAsync(command.TeacherId)
            ?? throw new InternalServerErrorException(
                $"Could not find teacher with ID: {command.TeacherId}"
            );

        if (string.IsNullOrWhiteSpace(teacher.ThirdPartyVideoCollectionId))
        {
            // Create collection in Bunny stream for a teacher if that teacher has no
            // videos stored with the application
            var response = await this.bunnyHttpClient.CreateCollectionAsync(command.TeacherId);
            teacher.ThirdPartyVideoCollectionId =
                response.CollectionGuid
                ?? throw new InternalServerErrorException(
                    $"Reponse property of {nameof(response.CollectionGuid)} is null when creating collection"
                );
            await this.unitOfWork.SaveChangesAsync();
        }

        this.unitOfWork.BeginTransaction();

        try
        {
            // Creating video on Bunny Stream
            var response = await this.bunnyHttpClient.CreateVideoAsync(
                command.Title,
                teacher.ThirdPartyVideoCollectionId
            );

            // Storing Video information in Database
            var video = new Video() { Title = command.Title, Description = command.Description, };
            var videoSource = new VideoSource()
            {
                SourceType = VideoSourceType.BunnyStream,
                ThirdPartyVideoId = response.VideoGuid
            };

            video.VideoSources.Add(videoSource);
            teacher.Videos.Add(video);

            // Creating and Signing Token for user to upload to 3rd party Video Storage
            var presignedSignatureExpiration = DateTimeOffset
                .UtcNow.AddDays(1)
                .ToUnixTimeMilliseconds();

            var videoPresignedSignature =
                $"{this.bunnySettings.LibraryId}{this.bunnySettings.ApiKey}{presignedSignatureExpiration}{videoSource.ThirdPartyVideoId}";
            var encryptedVideoPresignedSignature = string.Empty;
            var presignedSignatureBytes = Encoding.UTF8.GetBytes(videoPresignedSignature);
            var encryptedBytes = SHA256.HashData(presignedSignatureBytes);

            foreach (var data in encryptedBytes)
            {
                encryptedVideoPresignedSignature += data.ToString("x2", new CultureInfo("en-US"));
            }

            await this.unitOfWork.SaveChangesAsync();

            this.unitOfWork.CommitTransaction();

            return new VideoUploadResult()
            {
                Title = command.Title,
                ThirdPartyLibraryId = this.bunnySettings.LibraryId,
                ThirdPartyVideoCollectionId = teacher.ThirdPartyVideoCollectionId,
                ThirdPartyVideoId = videoSource.ThirdPartyVideoId,
                AuthorizationSignature = encryptedVideoPresignedSignature,
                AuthorizationExpiration = presignedSignatureExpiration,
                UploadEndpoint = this.bunnySettings.UploadEndpoint
            };
        }
        catch (Exception)
        {
            this.unitOfWork.RollbackTransaction();
            throw;
        }
    }

    public async Task<VideoRequestResponse> FetchVideoInfo(Guid videoId)
    {
        //fetch the video Id from the database (video respositiory)
        var video = await this.videoRepository.GetByIdAsync(videoId) ?? throw new NotFoundException("Video Not Found");

        var nextVideo = video.NextVideo;
        var prevVideo = video.PrevVideo;
        var courseModule = video.CourseModule;

        // Fetch video play data from bunny stream
        var playData = await this.bunnyHttpClient.GetVideoPlayDataAsync(video.ThirdPartyVideoId);

        // Map to video response Data to Object
        var videoResponse = new VideoRequestResponse
        {
            Title = video.Title,
            Description = video.Description,
            NextVideo = nextVideo?.Id,
            PrevVideo = prevVideo?.Id,
            VideoSource = playData.VideoSources.Select(vs => $"video-{vs.SourceId}-{vs.Quality}").ToList(),
            SubtitleSource = playData.Subtitles.Select(sub => $"subtitle-{sub.SourceId}-{sub.Language}").ToList(),
            CourseModule = courseModule != null ? new CourseModuleDto
            {
                Name = courseModule.Topic,
                Id = courseModule.Id

            } : null!,
            Playlist = video.Playlist != null ? new PlaylistDto
            {
                Name = video.Playlist.Name,
                Id = video.Playlist.Id
            } : null!


        };

        return videoResponse;
    }
}
