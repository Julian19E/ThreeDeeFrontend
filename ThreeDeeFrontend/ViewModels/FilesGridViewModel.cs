using Microsoft.AspNetCore.Components;
using MudBlazor;
using ThreeDeeApplication.Enums;
using ThreeDeeApplication.Models;
using ThreeDeeInfrastructure.Repositories;
using ThreeDeeInfrastructure.ResponseModels;


namespace ThreeDeeFrontend.ViewModels;

public class FilesGridViewModel : IFilesGridViewModel
{
    public EventCallback FilesChanged { get; set; }
    public string SelectedSearchValue { get; set; } = "";
    public List<FileModel> FilteredFiles { get; private set; } = new();
    public Filetype FileAccessStatus { get; private set; }
    
    private readonly List<FileModel> _files = new();
    private IRepository<FileModel, FileModel> FileRepository { get; } 
    
    public FilesGridViewModel(IRepository<FileModel, FileModel> fileRepository)
    {
        FileRepository = fileRepository;
        ChangeStatus(FileAccessStatus);
    }

    public async Task ChangeStatus(Filetype newStatus)
    {
        FileAccessStatus = newStatus;
        _files.Clear();
        IEnumerable<FileModel> test = new List<FileModel>();
        if (newStatus == Filetype.Public)
        {
            test = await FileRepository.GetAll("public");
        }
        else
        {
            test = await FileRepository.GetAll("tst"); //TODO Get Username
        }
        
        foreach (var file in test)
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