using ThreeDeeApplication.Models;

namespace ThreeDeeFrontend.ViewModels;

public class TopMenuViewModel
{
    public List<DropDownMenuItemModel> LibraryItems { get; } = new()
    {
        new(Localization.DropDownPublicFiles, "/"),
        new(Localization.DropDownSharedFiles, "/"),
        new(Localization.DropDownPrivateFiles, "/"),
    };
    
    public List<DropDownMenuItemModel> FilesItems { get; } = new()
    {
        new(Localization.DropDownAddFile, "/"),
        new(Localization.DropDownRequestFile, "/")
    };
}