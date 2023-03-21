using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ThreeDeeApplication.Enums;
using ThreeDeeApplication.Models;
using ThreeDeeInfrastructure.Repositories;

namespace ThreeDeeFrontend.Pages;


public partial class FilesView
{
    [Inject]
    private IFileRepository FileRepository { get; set; }
    
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private List<FileModel> _filteredFiles = new();

    private Filetype Status;

    protected override void OnParametersSet()
    {
        ChooseFilePerStatus();
    }

    private void ChooseFilePerStatus()
    {
        _filteredFiles.Clear();
        foreach (FileModel model in FileRepository.MockData)
        {
            if (model.Filetype == Status)
            {
                _filteredFiles.Add(model);
            }
        }
    }

    private void OnStatusButtonClicked(Filetype status)
    {
        Status = status;
        ChooseFilePerStatus();
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