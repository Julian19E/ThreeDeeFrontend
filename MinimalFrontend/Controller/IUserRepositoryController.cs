using MinimalFrontend.Models;

namespace MinimalFrontend.Controller;

public interface IUserRepositoryController
{
    Task<UserRequestResponseModel> GetAllUsers();
    Task<UserRequestResponseModel> GetAllUsersByAge(int minAge);
    Task<UserRequestResponseModel> GetUserById(int iD);
    Task<UserRequestResponseModel> Create(string name, string mail, int age);
    Task<UserRequestResponseModel> Delete(int iD);
    Task<UserRequestResponseModel> Update(int iD, string name, string mail, int age);
}