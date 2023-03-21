
using Microsoft.AspNetCore.Components;
using ThreeDeeApplication.Models;

namespace ThreeDeeFrontend.Components;

public partial class FileGrid
{
    [Inject]
    private NavigationManager NavigationManager { get; set; }
    
    [Parameter]
    public List<FileModel> Files { get; set; }
    
    private void OnButtonClicked(int fileId)
    {
        NavigationManager.NavigateTo($"/model/{fileId}");
    }
}