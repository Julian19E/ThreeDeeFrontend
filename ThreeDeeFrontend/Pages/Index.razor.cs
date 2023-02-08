using Microsoft.AspNetCore.Components;
using ThreeDeeFrontend.Services;

namespace ThreeDeeFrontend.Pages;

public partial class Index
{
    [Inject]
    IThemeProviderService ThemeProviderService { get; set; }
}