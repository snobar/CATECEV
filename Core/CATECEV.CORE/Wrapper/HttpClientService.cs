using System.Net;
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

    public async Task<ResponseModel<T>> GetAsync<T>(string uri, string token = null)
    {
        var response = new HttpResponseMessage();
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            AddAuthorizationHeader(request, token);

            response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<Data<T>>(content, _options);

            return new ResponseModel<T> 
            { 
                Data = data.data,
                IsSuccess = true,
                StatusCode = response.StatusCode,
                CurrentPage = data.Meta.current_page,
                TotalRecords = data.Meta.Total,
                TotalPages = data.Meta.last_page,
            };
        }
        catch (Exception ex)
        {
            return new ResponseModel<T>
            {
                IsSuccess = false,
                Message = $"HttpClientService Exception Message: {ex.Message} \n HttpClientService Exception InnerException: {ex.InnerException}",
                ShowMessage = false,
                StatusCode = response.StatusCode
            };
        }

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
    Task<ResponseModel<T>> GetAsync<T>(string uri, string token = null);
    Task<T> PostAsync<T>(string uri, object data, string token = null);
    Task<T> PutAsync<T>(string uri, object data, string token = null);
    Task DeleteAsync(string uri, string token = null);
    Task<T> PatchAsync<T>(string uri, object data, string token = null);
}

public class ResponseModel<T>
{
    public T Data { get; set; }
    public bool IsSuccess { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public bool ShowMessage { get; set; }
    public int CurrentPage { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
}

public class Data<T>
{
    public T data { get; set; }
    public Links Links { get; set; }
    public Meta Meta { get; set; }
}

public class Links
{
    public string First { get; set; }
    public string Last { get; set; }
    public string Prev { get; set; }
    public string Next { get; set; }
}

public class Meta
{
    public int current_page { get; set; }
    public int? From { get; set; }
    public int last_page { get; set; }
    public List<PageLink> Links { get; set; }
    public string Path { get; set; }
    public int PerPage { get; set; }
    public int? To { get; set; }
    public int Total { get; set; }
}

public class PageLink
{
    public string Url { get; set; }
    public string Label { get; set; }
    public bool Active { get; set; }
}