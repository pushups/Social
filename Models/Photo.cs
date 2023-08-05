using System.Text.Json;
using System.Text.Json.Serialization;

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
    

    private static IHttpClientFactory ClientFactory { get; set; } = null!;

    public static void Initialize(IServiceProvider serviceProvider) {
        ClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    }

    internal static async Task<IEnumerable<Photo>?> GetPhotosAsync(int albumId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"albums/{albumId}/photos");
        var client = ClientFactory.CreateClient("JsonPlaceholderClient");

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode) {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var photos = await JsonSerializer.DeserializeAsync<IEnumerable<Photo>?>(responseStream);
            return photos;
        } else {
            return null!;
        }
    }
}