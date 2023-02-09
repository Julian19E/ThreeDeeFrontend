using Microsoft.AspNetCore.Components;
using MudBlazor.Utilities;
using ThreeDeeFrontend.Models;

namespace ThreeDeeFrontend.Components;

public partial class FileCardPreview
{
    [Parameter] 
    public FileModel File { get; set; }
    
    [Parameter] 
    public EventCallback<int> ButtonClicked { get; set; }
    
    private Random _rand = new();
}