using System.Text.Json;
using System.Text.Json.Serialization;
using Social.Libraries;

namespace Social.Models;

public class User {

    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = null!;

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

    private string? _GravatarHash;
    public string? GravatarHash {
        set {
            if(Email is null) {
                _GravatarHash = null;
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

    [JsonPropertyName("company")]
    public Company Company { get; set; } = null!;

    [JsonPropertyName("address")]
    public Address Address { get; set; } = null!;

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = null!;

    [JsonPropertyName("website")]
    public string Website { get; set; } = null!;

    internal static async Task<IEnumerable<User>?> GetUsersAsync()
    {
        return await JsonPlaceholderClient.Get<IEnumerable<User>?>("users");
    }

    internal static async Task<IEnumerable<User>?> GetUsersAsync(IEnumerable<int> userIds)
    {
        return await JsonPlaceholderClient.Get<IEnumerable<User>?>($"users?{string.Join("&", userIds.Select(id => $"id={id}"))}");
    }

    internal static async Task<User> GetUserAsync(int id)
    {
        return await JsonPlaceholderClient.Get<User>($"users/{id}");
    }
}

public class Address
{
    [JsonPropertyName("street")]
    public string Street { get; set; } = null!;

    [JsonPropertyName("suite")]
    public string Suite { get; set; } = null!;

    [JsonPropertyName("city")]
    public string City { get; set; } = null!;

    [JsonPropertyName("zipcode")]
    public string Zipcode { get; set; } = null!;
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