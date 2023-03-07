using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ThreeDeeApplication.Enums;
using ThreeDeeApplication.Models;
using ThreeDeeInfrastructure.Repositories;

namespace ThreeDeeFrontend.Pages;

public partial class PrivateFile
{
    [Inject]
    private IFileRepository FileRepository { get; set; }
    
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private List<FileModel> _filteredPrivateFiles = new();

    protected override void OnParametersSet()
     {
         foreach (FileModel model in FileRepository.MockData)
        {
            if (model.Filetype == Filetype.Private)
            {
                _filteredPrivateFiles.Add(model);
            }
        }
     }

    private void OnButtonClicked(int fileId)
    {
        NavigationManager.NavigateTo($"/model/{fileId}");
    }

    private async Task OnFilteredValueChanged(List<FileModel> filtered)
    {
        _filteredPrivateFiles = filtered;
        await InvokeAsync(StateHasChanged);
    }
}