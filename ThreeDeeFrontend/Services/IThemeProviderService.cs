using Microsoft.AspNetCore.Components;

namespace ThreeDeeFrontend.Services;

public interface IThemeProviderService
{
    bool IsDarkMode { get; set; }
    EventCallback<string> BackgroundColorHasChanged { get; set; }
    string CurrentColor { get; }
}