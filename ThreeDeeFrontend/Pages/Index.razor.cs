using Microsoft.AspNetCore.Components;
using ThreeDeeFrontend.Controller;
using ThreeDeeFrontend.Models;
using ThreeDeeFrontend.Repositories;
using ThreeDeeFrontend.Services;

namespace ThreeDeeFrontend.Pages;

public partial class Index
{
    [Inject]
    private IThemeProviderService ThemeProviderService { get; set; }
    
    [Inject]
    private IFileRepository FileRepository { get; set; }
    
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private List<FileModel> _filteredFiles;

    protected override void OnParametersSet()
    {
        _filteredFiles = FileRepository.Files;
    }

    private void OnButtonClicked(int fileId)
    {
        Console.Write(fileId);
        NavigationManager.NavigateTo($"/model/{fileId}");
    }

    private async Task OnFilteredValueChanged(List<FileModel> filtered)
    {
        _filteredFiles = filtered;
        await InvokeAsync(StateHasChanged);
    }
}