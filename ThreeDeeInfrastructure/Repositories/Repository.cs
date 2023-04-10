

using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using ThreeDeeInfrastructure.ResponseModels;
using ThreeDeeInfrastructure.Services;


namespace ThreeDeeInfrastructure.Repositories;

 

public class Repository<TResponse, TRequest> : IRepository<TResponse, TRequest>
    where TResponse : ResponseBase, new()
    where TRequest : class, new()
{
    protected readonly HttpClient HttpClient;
    protected readonly JsonSerializerOptions Options;
    protected readonly string Uri;

 


    public Repository(HttpClient httpClient, IEndpointService endpointService)
    {
        HttpClient = httpClient;
        Uri = endpointService.GetMappedUrl(typeof(TResponse));
        Options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
    }

 

    public async Task<IEnumerable<TResponse>> GetAll()
    {
        try
        {
            var response = await HttpClient.GetFromJsonAsync<List<TResponse>>($"{Uri}", Options);
            return response ?? Enumerable.Empty<TResponse>().ToList();
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new List<TResponse> {new() {IsResponseSuccess = false}};
        }
    }
    
    public async Task<IEnumerable<TResponse>> GetAll(string id)
    {
        try
        {
            var response = await HttpClient.GetFromJsonAsync<List<TResponse>>($"{Uri}/{id}", Options);
            return response ?? Enumerable.Empty<TResponse>().ToList();
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new List<TResponse> {new() {IsResponseSuccess = false}};
        }
    }

 

    public async Task<TResponse> Get(int id, Dictionary<string, string>? queries = default)
    {
        try
        {
            var query = ExtractQuery(queries);
            var response = await HttpClient.GetFromJsonAsync<TResponse>($"{Uri}/{id}{query}", Options);
            return response ?? new TResponse();
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new TResponse
            {
                IsResponseSuccess = false
            };
        }
    }

    
 

    public async Task<TResponse> Insert(TRequest requestModel)
    {
        try
        {
            var response = await HttpClient.PostAsJsonAsync(Uri, requestModel);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseString) ?? new TResponse();
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new TResponse
            {
                IsResponseSuccess = false
            };
        }
    }

 

    public async Task<TResponse> Update(TRequest requestModel, int id)
    {
        try
        {
            var response = await HttpClient.PutAsJsonAsync($"{Uri}/{id}", requestModel);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseString) ?? new TResponse();
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new TResponse
            {
                IsResponseSuccess = false
            };
        }
    }

 

    public async Task<bool> Delete(int id)
    {
        try
        {
            var response = await HttpClient.DeleteAsync($"{Uri}/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException e)
        {
            Debug.WriteLine(e);
            return false;
        }
    }

 

    protected string ExtractQuery(Dictionary<string, string>? queries)
    {
        if (queries == null || queries.Count == 0) return "";
        var query = queries.Aggregate("?", (current, kv) => current + $"{kv.Key}={kv.Value}&");
        return query.Remove(query.Length - 1);
    }
}
