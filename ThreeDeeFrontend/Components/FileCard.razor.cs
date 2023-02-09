using Microsoft.AspNetCore.Components;
using ThreeDeeFrontend.Models;

namespace ThreeDeeFrontend.Components;

public partial class FileCard
{
    [Parameter] 
    public FileModel File { get; set; }
    
    [Parameter] 
    public bool IsPreview { get; set; }

    private bool _isPreview;
    private string _colorValue = "#abcdef";
    
    private ModelRenderer _modelRendererRef;

    protected override void OnParametersSet()
    {
        _isPreview = IsPreview;
        base.OnParametersSet();
    }

    private void Switch()
    {
        _isPreview = !_isPreview;
        StateHasChanged();
    }
}