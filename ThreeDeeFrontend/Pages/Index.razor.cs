using Microsoft.AspNetCore.Components;
using ThreeDeeFrontend.Models;
using ThreeDeeFrontend.Services;

namespace ThreeDeeFrontend.Pages;

public partial class Index
{
    [Inject]
    private IThemeProviderService ThemeProviderService { get; set; }
    private static readonly string _description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

    private readonly List<FileModel> _files = new()
    {
        //new FileModel{Name = "Benchy", Author = "Nico", Description = _description, RenderCoordinates = new Vector3(1.129834808276718F, -10.246199352518826F, 51.0725531041833F)},
        new FileModel{Id = 0, Name = "Benchy", Author = "Nico", Description = _description},
        new FileModel{Id = 1, Name = "PrinterTest", Author = "Nico", Description = _description},
        new FileModel{Id = 2, Name = "Falcon", Author = "Gabriel", Description = _description},
        new FileModel{Id = 3, Name = "Lamp", Author = "Gabriel", Description = _description},
        new FileModel{Id = 4, Name = "Nut", Author = "Julian", Description = _description},
        new FileModel{Id = 5, Name = "GiftBoxOuter", Author = "Julian", Description = _description},
        new FileModel{Id = 6, Name = "Bolt", Author = "Hannes", Description = _description},
        new FileModel{Id = 7, Name = "GiftBoxInner", Author = "Hannes", Description = _description}
    };
}