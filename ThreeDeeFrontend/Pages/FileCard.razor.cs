using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor.Utilities;
using ThreeDeeApplication.Models;
using ThreeDeeFrontend.Components;
using ThreeDeeInfrastructure.Repositories;

namespace ThreeDeeFrontend.Pages;

public partial class FileCard
{
    [Parameter]
    public int Id { get; set; }
    
    [Inject] 
    public IFileRepository FileRepository { get; set; }

    private FileModel _file;
    private bool _avoidRendering;
    private bool _isColorPickerOpen;
    private bool _isInitDone;
    private MudColor _color = new("#03A9F4");
    private readonly Random _random = new();
    private double _progress;
    private ModelRenderer _modelRendererRef;

    protected override void OnParametersSet()
    {
        _file = FileRepository.MockData[Id];
    }

    private async Task ProgressHasChangedCallback(double progress)
    {
        _progress = progress;
        await InvokeAsync(StateHasChanged);
    }

    private async Task UpdateColor(MudColor color)
    {
        _color = color;
        await _modelRendererRef.ChangeColor(color.Value);
    }
}