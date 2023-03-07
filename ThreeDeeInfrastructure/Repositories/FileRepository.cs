using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using ThreeDeeApplication.Enums;
using ThreeDeeApplication.Models;

namespace ThreeDeeInfrastructure.Repositories;

public class FileRepository : IFileRepository
{
    private readonly HttpClient _httpClient;
    private readonly string _serviceUrl;
    public List<FileModel> MockData { get; } = new()
    {
        new FileModel{Id = 0, Name = "Banshee", Author = "Nico", Filetype = Filetype.Public},
        new FileModel{Id = 1, Name = "PrinterTest", Author = "Nico", Filetype = Filetype.Public},
        new FileModel{Id = 2, Name = "Falcon", Author = "Gabriel", Filetype = Filetype.Public},
        new FileModel{Id = 3, Name = "Lamp", Author = "Gabriel", Filetype = Filetype.Private},
        new FileModel{Id = 4, Name = "Nut", Author = "Julian", Filetype = Filetype.Private},
        new FileModel{Id = 5, Name = "GiftBoxOuter", Author = "Julian", Filetype = Filetype.Private},
        new FileModel{Id = 6, Name = "Bolt", Author = "Hannes", Filetype = Filetype.Shared},
        new FileModel{Id = 7, Name = "GiftBoxInner", Author = "Hannes", Filetype = Filetype.Shared}
    };

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