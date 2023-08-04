using System.Text.Json;
using System.Text.Json.Serialization;

namespace Social.Models;

public class Comment {
    [JsonPropertyName("postId")]
    public int PostId { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;

    [JsonPropertyName("body")]
    public string Body { get; set; } = null!;

    private static IHttpClientFactory ClientFactory { get; set; } = null!;

    public static void Initialize(IServiceProvider serviceProvider) {
        ClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    }

    internal static async Task<IEnumerable<Comment>?> GetCommentsAsync(int id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"posts/{id}/comments");
        var client = ClientFactory.CreateClient("JsonPlaceholderClient");

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode) {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var comments = await JsonSerializer.DeserializeAsync<IEnumerable<Comment>?>(responseStream);
            return comments;
        } else {
            return null!;
        }
    }
}