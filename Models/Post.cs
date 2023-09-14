using System.Text.Json;
using System.Text.Json.Serialization;
using Social.Libraries;

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
    
    internal static async Task<IEnumerable<Post>?> GetPostsAsync()
    {
        return await JsonPlaceholderClient.Get<IEnumerable<Post>?>("posts");
    }

    internal static async Task<Post?> GetPostAsync(int id)
    {
        return await JsonPlaceholderClient.Get<Post?>($"posts/{id}");
    }

    internal static async Task<IEnumerable<Post>?> GetPostsByUserAsync(int userId)
    {
        return await JsonPlaceholderClient.Get<IEnumerable<Post>?>($"users/{userId}/posts");
    }
}