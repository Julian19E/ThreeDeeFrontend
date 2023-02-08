using Microsoft.AspNetCore.Components;

namespace ThreeDeeFrontend.Services;

public interface IThemeProviderService
{
    bool IsDarkMode { get; set; }
    float[] BackGroundColor();
    EventCallback<float[]> BackgroundColorHasChanged { get; set; }
}