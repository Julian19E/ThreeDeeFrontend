using Microsoft.AspNetCore.Components;
using ThreeDeeInfrastructure.Repositories;

namespace ThreeDeeFrontend.Components;

public partial class GCodeTable
{
    [Inject]
    private IGCodeSettingsRepository GCodeSettingsRepository { get; set; }
    
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private void OnButtonClicked()
    {
        int random = new Random().Next(0, 3);
        string[] fileNames = {"Banshee", "Bolt", "PrinterTest"};
        NavigationManager.NavigateTo($"/analyzer/{fileNames[random]}");
    }
}