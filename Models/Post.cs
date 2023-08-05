using System.Text.Json;
using System.Text.Json.Serialization;

namespace Social.Models;

public class Post {

    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("userId")]
    public int UserId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("body")]
    public string Body { get; set; } = null!;

    private static IHttpClientFactory ClientFactory { get; set; } = null!;

    public static void Initialize(IServiceProvider serviceProvider) {
        ClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    }
    
    internal static async Task<IEnumerable<Post>?> GetPostsAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "posts");
        var client = ClientFactory.CreateClient("JsonPlaceholderClient");

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode) {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var posts = await JsonSerializer.DeserializeAsync<IEnumerable<Post>>(responseStream);
            return posts;
        } else {
            return Array.Empty<Post>();
        }
    }

    internal static async Task<Post?> GetPostAsync(int id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"posts/{id}");
        var client = ClientFactory.CreateClient("JsonPlaceholderClient");

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode) {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var post = await JsonSerializer.DeserializeAsync<Post>(responseStream);
            return post;
        } else {
            return null;
        }
    }

    internal static async Task<IEnumerable<Post>?> GetPostsByUserAsync(int userId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"users/{userId}/posts");
        var client = ClientFactory.CreateClient("JsonPlaceholderClient");

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode) {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var posts = await JsonSerializer.DeserializeAsync<IEnumerable<Post>?>(responseStream);
            return posts;
        } else {
            return null;
        }       
    }
}