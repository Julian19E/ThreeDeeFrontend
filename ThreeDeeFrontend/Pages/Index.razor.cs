using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ThreeDeeApplication.Enums;
using ThreeDeeApplication.Models;
using ThreeDeeFrontend.ViewModels;
using ThreeDeeInfrastructure.Repositories;

namespace ThreeDeeFrontend.Pages;

public partial class Index
{
    [Inject]
    public IFilesGridViewModel Vm { get; set; }

    protected override async Task OnAfterRenderAsync(bool isFirstRender)
    {
        if (isFirstRender)
        {
            Vm.FilesChanged = EventCallback.Factory
                .Create(this, async () => await InvokeAsync(StateHasChanged));
        }
    }
}