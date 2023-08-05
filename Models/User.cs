using System.Text.Json;
using System.Text.Json.Serialization;

namespace Social.Models;

public class User {

    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = null!;

    private string _Email;
    [JsonPropertyName("email")]
    public string Email {
        set {
            _Email = value;
            GravatarHash = value;
        }

        get {
            return _Email;
        }
    }

    private string _GravatarHash = null!;
    public string GravatarHash {
        set {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(Email);
            byte[] hashBytes = System.Security.Cryptography.MD5.HashData(inputBytes);

            _GravatarHash = Convert.ToHexString(hashBytes);
        }

        get {
            return _GravatarHash;
        }
    }

    [JsonPropertyName("company")]
    public Company Company { get; set; } = null!;

    private static IHttpClientFactory ClientFactory { get; set; } = null!;

    public static void Initialize(IServiceProvider serviceProvider) {
        ClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    }

    internal static async Task<IEnumerable<User>?> GetUsersAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "users");
        var client = ClientFactory.CreateClient("JsonPlaceholderClient");

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode) {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var users = await JsonSerializer.DeserializeAsync<IEnumerable<User>>(responseStream);
            return users;
        } else {
            return Array.Empty<User>();
        }
    }

    internal static async Task<IEnumerable<User>?> GetUsersAsync(IEnumerable<int> userIds)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"users?{string.Join("&", userIds.Select(id => $"id={id}"))}");
        var client = ClientFactory.CreateClient("JsonPlaceholderClient");

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode) {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var users = await JsonSerializer.DeserializeAsync<IEnumerable<User>>(responseStream);
            return users;
        } else {
            return Array.Empty<User>();
        }
    }

    internal static async Task<User> GetUserAsync(int id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"users/{id}");
        var client = ClientFactory.CreateClient("JsonPlaceholderClient");

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode) {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<User>(responseStream);
            return user!;
        } else {
            return null!;
        }
    }
}

public class Company
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("catchPhrase")]
    public string CatchPhrase { get; set; } = null!;

    [JsonPropertyName("bs")]
    public string Bs { get; set; } = null!;
}