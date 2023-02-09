using Microsoft.AspNetCore.Components;
using ThreeDeeFrontend.Controller;
using ThreeDeeFrontend.Models;
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
    
    private void OnButtonClicked(int fileId)
    {
        Console.Write(fileId);
        NavigationManager.NavigateTo($"/model/{fileId}");
    }
}