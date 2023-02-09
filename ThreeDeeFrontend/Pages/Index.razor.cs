using Microsoft.AspNetCore.Components;
using ThreeDeeApplication.Models;
using ThreeDeeInfrastructure.Repositories;

namespace ThreeDeeFrontend.Pages;

public partial class Index
{
    [Inject]
    private IFileRepository FileRepository { get; set; }
    
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private List<FileModel> _filteredFiles;

    protected override void OnParametersSet()
    {
        _filteredFiles = FileRepository.MockData;
    }

    private void OnButtonClicked(int fileId)
    {
        NavigationManager.NavigateTo($"/model/{fileId}");
    }

    private async Task OnFilteredValueChanged(List<FileModel> filtered)
    {
        _filteredFiles = filtered;
        await InvokeAsync(StateHasChanged);
    }
}