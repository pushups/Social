using System.Text.Json;
using System.Text.Json.Serialization;

namespace Social.Libraries;

public class JsonPlaceholderClient {

    private static IHttpClientFactory ClientFactory { get; set; } = null!;
    public static void Initialize(IServiceProvider serviceProvider) {
        ClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    }

    public static async Task<T?> Get<T>(string path)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, path);
        var client = ClientFactory.CreateClient("JsonPlaceholderClient");

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode) {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<T>(responseStream);
            return result;
        } else {
            return default;
        }
    }

}