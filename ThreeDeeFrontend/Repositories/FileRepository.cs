using System.Text.Json;
using ThreeDeeFrontend.Controller;
using ThreeDeeFrontend.Models;

namespace ThreeDeeFrontend.Repositories;

public class FileRepository : IFileRepository
{
    private readonly HttpClient _httpClient;
    private readonly string _serviceUrl;

    public FileRepository(HttpClient httpClient, string serviceUrl)
    {
        _httpClient = httpClient;
        _serviceUrl = serviceUrl;
    }

    public async Task<List<FileModel>> GetAll()
    {
        var httpResponseMessage = await _httpClient.GetStreamAsync($"{_serviceUrl}/files");
        return await JsonSerializer.DeserializeAsync<List<FileModel>>(httpResponseMessage) ?? new List<FileModel>();
    }
    
    public async Task<FileModel> Get(int id)
    {
        var httpResponseMessage = await _httpClient.GetStreamAsync($"{_serviceUrl}/files/{id}");
        return await JsonSerializer.DeserializeAsync<FileModel>(httpResponseMessage) ?? new FileModel();
    }
    
    public async Task<bool> Create(FileModel file)
    {
        var httpResponseMessage = await _httpClient.PostAsJsonAsync($"{_serviceUrl}/files", file);
        return httpResponseMessage.IsSuccessStatusCode;
    }
    
    public async Task<bool> Update(FileModel file)
    {
        var httpResponseMessage = await _httpClient.PutAsJsonAsync($"{_serviceUrl}/files", file);
        return httpResponseMessage.IsSuccessStatusCode;
    }
    
    public async Task<bool> Delete(int id)
    {
        var httpResponseMessage = await _httpClient.DeleteAsync($"{_serviceUrl}/files/{id}");
        return httpResponseMessage.IsSuccessStatusCode;
    }
}