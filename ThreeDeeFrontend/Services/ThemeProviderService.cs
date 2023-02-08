using Microsoft.AspNetCore.Components;

namespace ThreeDeeFrontend.Services;

public class ThemeProviderService : IThemeProviderService
{
    public bool IsDarkMode
    {
        get => _isDarkMode;
        set
        {
            _isDarkMode = value;
            var switchToColor = BackGroundColor();
            BackgroundColorHasChanged.InvokeAsync(switchToColor);
        }
    }
    
    public EventCallback<float[]> BackgroundColorHasChanged { get; set; }

    private const string DarkBackgroundHex = "373740";
    private const string LightBackgroundHex = "ffffff";
    private readonly float[] _darkBackgroundRgb;
    private readonly float[] _lightBackgroundRgb;
    private bool _isDarkMode;

    public ThemeProviderService()
    {
        _darkBackgroundRgb = CreateConvertedColors(DarkBackgroundHex);
        _lightBackgroundRgb = CreateConvertedColors(LightBackgroundHex);
    }

    public float[] BackGroundColor()
    {
        return _isDarkMode ? _darkBackgroundRgb : _lightBackgroundRgb;
    }
    
    private float[] CreateConvertedColors(string source)
    {
        float[] d = { 0F, 0F, 0F };
        if (source.Length == 6)
        {
            for (int i = 0; i < 3; i++)
            {
                string hexEightBit = source.Substring(i * 2, 2);
                float decValue = Convert.ToInt32(hexEightBit, 16);
                d[i] = decValue / 255;
            }
        }

        return d;
    }
}