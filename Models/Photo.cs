using System.Text.Json;
using System.Text.Json.Serialization;
using Social.Libraries;

namespace Social.Models;

public class Photo {
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("albumId")]
    public int AlbumId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("url")]
    public string Url { get; set; } = null!;

    [JsonPropertyName("thumbnailUrl")]
    public string ThumbnailUrl { get; set; } = null!;

    internal static async Task<IEnumerable<Photo>?> GetPhotosAsync(int albumId)
    {
        return await JsonPlaceholderClient.Get<IEnumerable<Photo>?>($"albums/{albumId}/photos");
    }

    internal static async Task<Photo?> GetPhotoAsync(int id)
    {
        return await JsonPlaceholderClient.Get<Photo?>($"photos/{id}");
    }
}