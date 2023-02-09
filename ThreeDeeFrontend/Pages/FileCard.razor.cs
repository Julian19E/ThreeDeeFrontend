using Microsoft.AspNetCore.Components;
using MudBlazor.Utilities;
using ThreeDeeFrontend.Components;
using ThreeDeeFrontend.Controller;
using ThreeDeeFrontend.Models;

namespace ThreeDeeFrontend.Pages;

public partial class FileCard
{
    [Inject] 
    public IFileRepository FileRepository { get; set; }

    [Parameter]
    public int Id { get; set; }

    private FileModel _file;
    private bool _avoidRendering;
    private bool _isColorPickerOpen;
    private MudColor _color = new("#03A9F4");
    private ModelRenderer _modelRendererRef;
    private Random _random = new();

    protected override void OnParametersSet()
    {
        _file = FileRepository.Files[Id];
    }

    private async Task UpdateColor(MudColor color)
    {
        _color = color;
        await _modelRendererRef.ChangeColor(color.Value);
    }
}