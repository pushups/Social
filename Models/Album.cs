using System.Text.Json.Serialization;
using Social.Libraries;

namespace Social.Models;

public class Album {
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("userId")]
    public int UserId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    public IEnumerable<Photo>? Photos { get; set; }

    internal static async Task<IEnumerable<Album>?> GetAlbumsAsync(int userId)
    {
        return await JsonPlaceholderClient.Get<IEnumerable<Album>?>($"users/{userId}/albums");
    }

    internal static async Task<Album?> GetAlbumAsync(int id)
    {
        return await JsonPlaceholderClient.Get<Album?>($"albums/{id}");
    }
}