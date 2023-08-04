using System.Text.Json;
using System.Text.Json.Serialization;

namespace Social.Models;

public class Comment {
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("postId")]
    public int PostId { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    
    [JsonPropertyName("body")]
    public string? Body { get; set; }
}