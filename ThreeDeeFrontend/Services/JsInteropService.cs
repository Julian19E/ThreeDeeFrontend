using Microsoft.JSInterop;

namespace ThreeDeeFrontend.Services;

public class JsInteropService : IJsInteropService
{
    private readonly Lazy<Task<IJSObjectReference>> _jsApiModule;
    
    public JsInteropService(IJSRuntime jsRuntimeService)
    {
        _jsApiModule = new Lazy<Task<IJSObjectReference>>(() => jsRuntimeService
            .InvokeAsync<IJSObjectReference>("import", "./js/renderController.js").AsTask());
    }

    public async Task ChangeCanvasStyle(float[] rgbColors)
    {
        IJSObjectReference module = await _jsApiModule.Value;
        await module.InvokeVoidAsync("changeCanvasStyle", rgbColors[0], rgbColors[1], rgbColors[2]);
    }
    
    
}