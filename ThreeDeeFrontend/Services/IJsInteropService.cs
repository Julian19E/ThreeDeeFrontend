using Microsoft.JSInterop;

namespace ThreeDeeFrontend.Services;

public interface IJsInteropService<T> where T : class
{
    Task AddFile(string path, string color, int id, DotNetObjectReference<T> dotnetObjRef);
    Task ChangeColor(string newColor, int id); 
}