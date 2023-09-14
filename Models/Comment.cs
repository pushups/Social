using System.Text.Json;
using System.Text.Json.Serialization;
using Social.Libraries;

namespace Social.Models;

public class Comment {
    [JsonPropertyName("postId")]
    public int PostId { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    
    private string? _Email;
    [JsonPropertyName("email")]
    public string? Email {
        set {
            _Email = value;
            if (value is not null) {
                GravatarHash = value;
            }
        }

        get {
            return _Email;
        }
    }

    private string _GravatarHash = null!;
    public string GravatarHash {
        set {
            if(Email is null) {
                _GravatarHash = null!;
                return;
            }
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(Email);
            byte[] hashBytes = System.Security.Cryptography.MD5.HashData(inputBytes);

            _GravatarHash = Convert.ToHexString(hashBytes);
        }

        get {
            return _GravatarHash;
        }
    }

    [JsonPropertyName("body")]
    public string Body { get; set; } = null!;

    internal static async Task<IEnumerable<Comment>?> GetCommentsAsync(int id)
    {
        return await JsonPlaceholderClient.Get<IEnumerable<Comment>?>($"posts/{id}/comments");
    }
}