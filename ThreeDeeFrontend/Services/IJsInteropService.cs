namespace ThreeDeeFrontend.Services;

public interface IJsInteropService
{
    Task AddFile(string path, string color, int id);
    Task ChangeColor(string newColor, int id);
}