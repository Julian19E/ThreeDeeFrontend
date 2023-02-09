using Microsoft.AspNetCore.Components;

namespace ThreeDeeInfrastructure.Services;

public interface IThemeProviderService
{
    bool IsDarkMode { get; set; }
    EventCallback<string> BackgroundColorHasChanged { get; set; }
    string CurrentColor { get; }
}