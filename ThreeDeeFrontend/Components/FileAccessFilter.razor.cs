using Microsoft.AspNetCore.Components;
using ThreeDeeApplication.Enums;

namespace ThreeDeeFrontend.Components;

public partial class FileAccessFilter
{
    [Parameter] 
    public EventCallback<Filetype> FiletypeHasChanged { get; set; }
    
    private Filetype _status;

    private async Task OnStatusButtonClicked(Filetype status)
    {
        _status = status;
        await FiletypeHasChanged.InvokeAsync(_status);
    }
    
}