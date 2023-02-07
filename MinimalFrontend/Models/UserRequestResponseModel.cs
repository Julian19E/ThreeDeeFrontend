using MinimalFrontend.Enums;

namespace MinimalFrontend.Models;

public class UserRequestResponseModel
{
    public List<UserModel> ServiceUsers { get; } = new();
    public string ResponseMessage { get; } = "";
    public ResponseStatus ResponseStatus { get; }

    public UserRequestResponseModel(List<UserModel> users, string responseMessage, ResponseStatus responseStatus)
    {
        ServiceUsers = users;
        ResponseMessage = responseMessage;
        ResponseStatus = responseStatus;
    }
    
    public UserRequestResponseModel(){}
}