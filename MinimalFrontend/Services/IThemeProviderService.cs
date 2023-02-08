using Microsoft.AspNetCore.Components;

namespace MinimalFrontend.Services;

public interface IThemeProviderService
{
    bool IsDarkMode { get; set; }
    float[] BackGroundColor();
    EventCallback<float[]> BackgroundColorHasChanged { get; set; }
}