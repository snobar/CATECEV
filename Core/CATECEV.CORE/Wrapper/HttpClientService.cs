using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class HttpClientService : IHttpClientService
{
    private readonly HttpClient _httpClient;
    private JsonSerializerOptions _options;

    public HttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<T> GetAsync<T>(string uri, string token = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        AddAuthorizationHeader(request, token);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(content, _options);
    }

    public async Task<T> PostAsync<T>(string uri, object data, string token = null)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = content
        };
        AddAuthorizationHeader(request, token);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent, _options);
    }

    public async Task<T> PutAsync<T>(string uri, object data, string token = null)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Put, uri)
        {
            Content = content
        };
        AddAuthorizationHeader(request, token);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent, _options);
    }

    public async Task DeleteAsync(string uri, string token = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        AddAuthorizationHeader(request, token);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

    public async Task<T> PatchAsync<T>(string uri, object data, string token = null)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri)
        {
            Content = content
        };
        AddAuthorizationHeader(request, token);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent, _options);
    }

    private void AddAuthorizationHeader(HttpRequestMessage request, string token)
    {
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}

public interface IHttpClientService
{
    Task<T> GetAsync<T>(string uri, string token = null);
    Task<T> PostAsync<T>(string uri, object data, string token = null);
    Task<T> PutAsync<T>(string uri, object data, string token = null);
    Task DeleteAsync(string uri, string token = null);
    Task<T> PatchAsync<T>(string uri, object data, string token = null);
}