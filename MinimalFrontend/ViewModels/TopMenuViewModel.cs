using MinimalFrontend.Models;

namespace MinimalFrontend.ViewModels;

public class TopMenuViewModel
{
    public List<DropDownMenuItemModel> LibraryItems { get; } = new()
    {
        new(Localization.DropDownPublicFiles, "/FilesOverview"),
        new(Localization.DropDownSharedFiles, "/"),
        new(Localization.DropDownPrivateFiles, "/"),
    };
    
    public List<DropDownMenuItemModel> FilesItems { get; } = new()
    {
        new(Localization.DropDownAddFile, "/"),
        new(Localization.DropDownRequestFile, "/")
    };
}