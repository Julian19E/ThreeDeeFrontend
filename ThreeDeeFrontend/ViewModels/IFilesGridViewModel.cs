using Microsoft.AspNetCore.Components;
using ThreeDeeApplication.Enums;
using ThreeDeeApplication.Models;

namespace ThreeDeeFrontend.ViewModels;

public interface IFilesGridViewModel
{
    List<FileModel> FilteredFiles { get; }
    public EventCallback FilesChanged { get; set; }
    string SelectedSearchValue { get; set; }
    Filetype FileAccessStatus { get; }
    Task ChangeStatus(Filetype newStatus);
    Task<IEnumerable<string>> UpdateFilteredFiles(string searchValue);
}