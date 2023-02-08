using MinimalFrontend.Models;

namespace MinimalFrontend.Controller;

public interface IFileRepository
{
    Task<List<FileModel>> GetAll();
    Task<FileModel> Get(int id);
    Task<bool> Create(FileModel file);
    Task<bool> Update(FileModel file);
    Task<bool> Delete(int id);
}