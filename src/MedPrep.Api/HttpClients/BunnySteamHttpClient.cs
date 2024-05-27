namespace MedPrep.Api.HttpClients;

using System.Net.Http.Json;
using System.Text.Json.Serialization;
using MedPrep.Api.Config;
using MedPrep.Api.Exceptions;
using Microsoft.Extensions.Options;
using static MedPrep.Api.HttpClients.BunnyStreamHttpClientContracts;

public sealed class BunnyStreamHttpClient(
    HttpClient client,
    IOptions<BunnyStreamSettings> bunnySettings
)
{
    private readonly HttpClient client = client;
    private readonly BunnyStreamSettings bunnySettings = bunnySettings.Value;

    public static string ClientName { get; set; } = "bunny";

    public static void Client(IServiceProvider serviceProvider, HttpClient httpClient)
    {
        var bunnyStreamSettings = serviceProvider.GetRequiredService<
            IOptions<BunnyStreamSettings>
        >();
        httpClient.DefaultRequestHeaders.Add("Authorization", bunnyStreamSettings.Value.ApiKey);
        httpClient.BaseAddress = new Uri("https://video.bunnycdn.com/");
    }

    public async Task<CreateCollectionResponse> CreateCollection(Guid userId)
    {
        var path = $"/library/{this.bunnySettings.LibraryId}/collections";
        var content = new Dictionary<string, string>() { { "name", userId.ToString() }, };
        var response = await this.client.PostAsJsonAsync(path, content);
        var result =
            await response.Content.ReadFromJsonAsync<CreateCollectionResponse>()
            ?? throw new InternalServerErrorException(
                $"Could not properly decode response from into {ClientName}{path} into {nameof(CreateCollectionResponse)} "
            );
        return result;
    }

    public async Task<CreateVideoResponse> CreateVideoAsync(string title, string collectionId)
    {
        var path = $"/library/{this.bunnySettings.LibraryId}/videos";
        var content = new Dictionary<string, dynamic>
        {
            { "title", title },
            { "collectionId", collectionId }
        };
        var response = await this.client.PostAsJsonAsync(path, content);
        var result =
            await response.Content.ReadFromJsonAsync<CreateVideoResponse>()
            ?? throw new InternalServerErrorException(
                $"Could not properly decode response from {ClientName}{path} into {nameof(CreateVideoResponse)} "
            );
        return result;
    }
}

public static class BunnyStreamHttpClientContracts
{
    public record CreateCollectionResponse
    {
        [JsonPropertyName("guid")]
        public string? CollectionGuid { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    };

    public record CreateVideoResponse
    {
        [JsonPropertyName("guid")]
        public string VideoGuid { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string? Title { get; set; }
    }
}
