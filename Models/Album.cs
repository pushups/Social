using System.Text.Json;
using System.Text.Json.Serialization;

namespace Social.Models;

public class Album {
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("userId")]
    public int UserId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    public IEnumerable<Photo>? Photos { get; set; }

    private static IHttpClientFactory ClientFactory { get; set; } = null!;

    public static void Initialize(IServiceProvider serviceProvider) {
        ClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    }

    internal static async Task<IEnumerable<Album>?> GetAlbumsAsync(int userId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"users/{userId}/albums");
        var client = ClientFactory.CreateClient("JsonPlaceholderClient");

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode) {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var albums = await JsonSerializer.DeserializeAsync<IEnumerable<Album>?>(responseStream);
            return albums;
        } else {
            return null!;
        }
    }
}