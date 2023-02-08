namespace ThreeDeeFrontend.Models;

public class DropDownMenuItemModel
{
    public string Name { get; }
    public string Path { get; }
    
    public DropDownMenuItemModel(string name, string path)
    {
        Name = name;
        Path = path;
    }
}