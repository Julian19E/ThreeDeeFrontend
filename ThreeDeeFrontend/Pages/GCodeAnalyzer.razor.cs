using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ThreeDeeFrontend.Pages;

public partial class GCodeAnalyzer
{
    [Inject]
    public IJSRuntime JsRuntime { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JsRuntime.InvokeVoidAsync("onloadInit");
        }
    }
}