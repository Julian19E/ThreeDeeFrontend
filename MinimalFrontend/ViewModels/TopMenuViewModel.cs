using MinimalFrontend.Models;

namespace MinimalFrontend.ViewModels;

public class TopMenuViewModel
{
    public List<DropDownMenuItemModel> LibraryItems { get; } = new()
    {
        new("Öffentliche", "/"),
        new("Files", "/FilesOverview")
    };
    
    public List<DropDownMenuItemModel> FilesItems { get; } = new()
    {
        new("Main", "/"),
        new("Files", "/FilesOverview")
    };
}