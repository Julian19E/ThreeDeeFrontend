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

    public async Task AddFile(string path, string color, int id)
    {
        IJSObjectReference module = await _jsApiModule.Value;
        await module.InvokeVoidAsync("addModel", path, color, id);
    }

    public async Task ChangeColor(string newColor, int id)
    {
        IJSObjectReference module = await _jsApiModule.Value;
        await module.InvokeVoidAsync("changeToColor", newColor, id);
    }
}