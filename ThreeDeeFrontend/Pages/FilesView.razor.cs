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
    

    private List<FileModel> _filteredFiles = new();

    private Filetype Status;

    protected override void OnParametersSet()
    {
        ChooseFilePerStatus();
    }
    
    private async Task ChooseFilePerStatus()
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

    private async Task OnStatusButtonClicked(Filetype status)
    {
        Status = status;
        await ChooseFilePerStatus();
    }
    
   

    private async Task OnFilteredValueChanged(List<FileModel> filtered)
    {
        _filteredFiles = filtered;
        await InvokeAsync(StateHasChanged);
    }
}