using Microsoft.AspNetCore.Components;

namespace ThreeDeeInfrastructure.Services;

public class ThemeProviderService : IThemeProviderService
{
    public EventCallback<string> BackgroundColorHasChanged { get; set; }
    public string CurrentColor => _isDarkMode ? DarkBackgroundHex : LightBackgroundHex;

    private const string DarkBackgroundHex = "#373740";
    private const string LightBackgroundHex = "#ffffff";
    private bool _isDarkMode;
    public bool IsDarkMode
    {
        get => _isDarkMode;
        set
        {
            _isDarkMode = value;
            BackgroundColorHasChanged.InvokeAsync(CurrentColor);
        }
    }
}
