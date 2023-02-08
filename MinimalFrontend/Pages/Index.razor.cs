using Microsoft.AspNetCore.Components;
using MinimalFrontend.Services;

namespace MinimalFrontend.Pages;

public partial class Index
{
    [Inject]
    IThemeProviderService ThemeProviderService { get; set; }
}