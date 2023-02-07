using System.Net.Mime;
using System.Text;
using System.Text.Json;
using MinimalFrontend.Enums;
using MinimalFrontend.Models;

namespace MinimalFrontend.Controller;



public class UserRepositoryController : IUserRepositoryController
{
    private readonly HttpClient _httpClient;
    private readonly string _usersEndpoint;

    public UserRepositoryController(HttpClient httpClient, string userEndpoint)
    {
        _httpClient = httpClient;
        _usersEndpoint = userEndpoint;
    }

    public async Task<UserRequestResponseModel> GetAllUsers()
    {
        using var httpResponseMessage = await _httpClient.GetAsync(_usersEndpoint);
        return await CreateResponseModel(httpResponseMessage, UserRepositoryActions.ReadMultiple);
    }
    
    public async Task<UserRequestResponseModel> GetAllUsersByAge(int minAge)
    {
        using var httpResponseMessage = await _httpClient.GetAsync($"{_usersEndpoint}/age/{minAge}");
        return await CreateResponseModel(httpResponseMessage, UserRepositoryActions.ReadMultiple);
    }
    
    public async Task<UserRequestResponseModel> GetUserById(int iD)
    {
        using var httpResponseMessage = await _httpClient.GetAsync($"{_usersEndpoint}/{iD}");
        return await CreateResponseModel(httpResponseMessage, UserRepositoryActions.ReadSingle);
    }
    
    public async Task<UserRequestResponseModel> Create(string name, string mail, int age)
    {
        var json = ConvertToJson(0, name, mail, age);
        using var httpResponseMessage = await _httpClient.PostAsync(_usersEndpoint, json);
        return await CreateResponseModel(httpResponseMessage, UserRepositoryActions.Created);
    }

    public async Task<UserRequestResponseModel> Delete(int iD)
    {
        using var httpResponseMessage = await _httpClient.DeleteAsync($"{_usersEndpoint}/{iD}");
        return await CreateResponseModel(httpResponseMessage, UserRepositoryActions.Deleted);
    }

    public async Task<UserRequestResponseModel> Update(int iD, string name, string mail, int age)
    {
        var json = ConvertToJson(iD, name, mail, age);
        using var httpResponseMessage = await _httpClient.PutAsync(_usersEndpoint, json);
        return await CreateResponseModel(httpResponseMessage, UserRepositoryActions.Updated);
    }

    private StringContent ConvertToJson(int iD, string name, string mail, int age)
    {
        var user = new UserModel
        {
            Id = iD,
            Age = age,
            UserName = name,
            Mail = mail
        };
        var todoItemJson = new StringContent(
            JsonSerializer.Serialize(user),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
        return todoItemJson;
    }

    private async Task<UserRequestResponseModel> CreateResponseModel(HttpResponseMessage httpResponseMessage, UserRepositoryActions action)
    {
        List<UserModel> users = new();
        string errorResponse = "";
        var responseStatus = ResponseStatus.Failed;
        if (httpResponseMessage.IsSuccessStatusCode && action == UserRepositoryActions.ReadMultiple)
        {
            var content = await httpResponseMessage.Content.ReadFromJsonAsync<UserModel[]>();
            users.AddRange(content ?? Array.Empty<UserModel>());
        }
        else if (httpResponseMessage.IsSuccessStatusCode && action == UserRepositoryActions.ReadSingle)
        {
            var content = await httpResponseMessage.Content.ReadFromJsonAsync<UserModel>();
            users.Add(content);
        }

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            responseStatus = ResponseStatus.OK;
        }
        else
        {
            errorResponse = await httpResponseMessage.Content.ReadAsStringAsync();
        }

        var message = httpResponseMessage.IsSuccessStatusCode ? 
            $"User successfully {action}." : 
            $"User couldn't be {action}. Message: {errorResponse}.";
        
        
        return new UserRequestResponseModel(users, message, responseStatus);
    }
}