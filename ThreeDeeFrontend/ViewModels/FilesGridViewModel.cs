using Microsoft.AspNetCore.Components;
using ThreeDeeApplication.Enums;
using ThreeDeeApplication.Models;
using ThreeDeeInfrastructure.Repositories;

namespace ThreeDeeFrontend.ViewModels;

public class FilesGridViewModel : IFilesGridViewModel
{
    public EventCallback FilesChanged { get; set; }
    public string SelectedSearchValue { get; set; } = "";
    public List<FileModel> FilteredFiles { get; private set; } = new();
    public Filetype FileAccessStatus { get; private set; }
    
    private readonly List<FileModel> _files = new();
    private readonly IFileRepository _fileRepository;

    public FilesGridViewModel(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
        ChangeStatus(FileAccessStatus);
    }

    public async Task ChangeStatus(Filetype newStatus)
    {
        FileAccessStatus = newStatus;
        _files.Clear();
        foreach (var file in _fileRepository.MockData.Where(file => file.Filetype == newStatus))
        {
            _files.Add(file);
        }

        FilteredFiles = new List<FileModel>(_files);
    }
    
    public async Task<IEnumerable<string>> UpdateFilteredFiles(string searchValue)
    {
        IEnumerable<string> filtered;
        if (string.IsNullOrEmpty(searchValue))
        {
            FilteredFiles = new List<FileModel>(_files);
            filtered = new List<string>();
        }
        else
        {
            FilteredFiles = new List<FileModel>(_files)
                .Where(x => x.Name
                    .Contains(searchValue, StringComparison.InvariantCultureIgnoreCase) && x.Filetype == FileAccessStatus)
                .ToList();
            filtered = FilteredFiles.Select(x => x.Name);
        }
        await FilesChanged.InvokeAsync();
        return filtered;
    }
}